using UnityEngine;

public class DenyEnemy : MonoBehaviour
{
    public bool inRange;
    private bool detectAtkRange;

    private void Awake()
    {
        inRange = false;
        Dreams.onWeaponUsed += Deny;
    }
    private void OnDisable() => Dreams.onWeaponUsed -= Deny;
    void Deny()
    {
        if(detectAtkRange) inRange = true;
        else inRange = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("DreamCatcher"))
        {
            detectAtkRange = true;
            PlayerSingleton.Instance.weapUsedTime = 0;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("DreamCatcher"))
            detectAtkRange = false;  
    }
}
