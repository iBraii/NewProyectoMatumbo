using UnityEngine;
using UnityEngine.InputSystem;

public class LightMec : MonoBehaviour
{
    bool canCharge;
    public float energy;
    [SerializeField] float maxEnergy;
    GameObject lantern;
    bool isON = false;

    private PlayerInput playerInput;
    private InputAction attackAction;
    private void Awake()
    {
        energy = maxEnergy;
        lantern = GameObject.Find("Lantern");
        playerInput = GetComponent<PlayerInput>();
        attackAction = playerInput.actions["Attack"];
        if (lantern == null)
        {
            Debug.LogWarning("No se encontró Lantern !!");
            return;
        }
    }
    private void Start()
    {
        lantern.SetActive(false);
    }
    void ChargeLight()
    {
        if(canCharge)
            energy += 1 * Time.deltaTime;
    }
    void ChargeLimits()
    {
        if (energy >= maxEnergy)
            energy = maxEnergy;
        if (energy <= 0)
        {
            energy = 0;
            isON = false;
            lantern.SetActive(false);
        }    
    }
    void PerformLightAction()
    {
        if(attackAction.triggered && isON == false)
        {
            isON = true;
            lantern.SetActive(true);
        }
        else if (attackAction.triggered && isON)
        {
            isON = false;
            lantern.SetActive(false);
        }
    }
    void OnPerformed()
    {
        if(isON)
        {
            energy -= 1 * Time.deltaTime;
        }
    }
    private void Update()
    {
        ChargeLight();
        ChargeLimits();
        PerformLightAction();
        OnPerformed();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("LightCharge"))
            canCharge = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("LightCharge"))
            canCharge = false;
    }
}
