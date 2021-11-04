using UnityEngine;

public class GroundCheckUpdater : MonoBehaviour
{
    private bool grounded;

    private void Awake() => PlayerSingleton.Instance.isGrounded = grounded;
    private void Update() => SetSingletonData();

    void SetSingletonData()
    {
        if (grounded && PlayerSingleton.Instance.isGrounded == false)
            PlayerSingleton.Instance.isGrounded = true;     
        else if (!grounded && PlayerSingleton.Instance.isGrounded)
            PlayerSingleton.Instance.isGrounded = false;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
            grounded = true;

        if (other.CompareTag("Box"))
            GetComponent<GrabBox>().enabled = false;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 6)
            grounded = true;

        if (other.CompareTag("Box"))
            GetComponent<GrabBox>().enabled = false;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 6)
            grounded = false;

        if (other.CompareTag("Box"))
            GetComponent<GrabBox>().enabled = true;
    }
}
