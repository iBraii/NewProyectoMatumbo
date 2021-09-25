using UnityEngine;
using UnityEngine.InputSystem;

public class LightMec : MonoBehaviour
{
    bool canCharge;
    public GameObject lantern;
    bool isON = false;

    private PlayerInput playerInput;
    private InputAction lightAction;
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        lightAction = playerInput.actions["Light"];
    }
    private void Start()
    {
        #region nulls
        if (lantern == null)
        {
            Debug.LogWarning("No se encontró Lantern !!");
            return;
        }
        #endregion
        lantern.SetActive(false);
    }
    void ChargeLight()
    {
        if(canCharge)
            PlayerSingleton.Instance.lightEnergy += 1 * Time.deltaTime;
    }
    void ChargeLimits()
    {
        Mathf.Clamp(PlayerSingleton.Instance.lightEnergy, 0, PlayerSingleton.Instance.maxLightEnergy);
    }
    void PerformLightAction()
    {
        #region nulls
        if (lantern == null)
        {
            Debug.LogWarning("No se encontró Lantern !!");
            return;
        }
        #endregion
        if (lightAction.triggered && isON == false)
        {
            isON = true;
            lantern.SetActive(true);
        }
        else if (lightAction.triggered && isON)
        {
            isON = false;
            lantern.SetActive(false);
        }
    }
    void OnPerformed()
    {
        if(isON)
            PlayerSingleton.Instance.lightEnergy -= 1 * Time.deltaTime; 
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
