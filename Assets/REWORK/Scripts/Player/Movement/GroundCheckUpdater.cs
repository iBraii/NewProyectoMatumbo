using UnityEngine;

public class GroundCheckUpdater : MonoBehaviour
{
    [SerializeField] LayerMask GroundLayer;
    [SerializeField] float floorDistance;
    public bool grounded;
    private void Awake()
    {
        PlayerSingleton.Instance.isGrounded = grounded;
    }

    void SetSingletonData()
    {
        if (grounded && PlayerSingleton.Instance.isGrounded == false)
        {
            PlayerSingleton.Instance.isGrounded = true;
        }
            
        else if (!grounded && PlayerSingleton.Instance.isGrounded)
        {
            PlayerSingleton.Instance.isGrounded = false;
        }
    }
    private void Update()
    {
        SetSingletonData();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
            grounded = true;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 6)
            grounded = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 6)
            grounded = false;
    }
}
