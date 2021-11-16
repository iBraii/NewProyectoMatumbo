using UnityEngine;

public class HideOnTent : MonoBehaviour
{
    public bool isHiding;
    private void Update()
    {
        PlayerSingleton.Instance.isHiding = isHiding;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Carpa"))
            isHiding = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Carpa"))
            isHiding = false;
    }
}
