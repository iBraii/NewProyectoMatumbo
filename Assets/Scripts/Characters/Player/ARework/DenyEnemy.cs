using UnityEngine;

public class DenyEnemy : MonoBehaviour
{
    public static bool denied;
    private bool inPlayerAtkRange;
    private void Awake()
    {
        denied = false;
        Dreams.onWeaponUsed += Deny;
    }
    private void OnDisable()
    {
        Dreams.onWeaponUsed -= Deny;
    }
    private void Update()
    {
        Debug.Log(denied);
    }
    void Deny()
    {
        if(inPlayerAtkRange)
            denied = true;
        else
            denied = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("DreamCatcher"))
            inPlayerAtkRange = true;      
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("DreamCatcher"))
            inPlayerAtkRange = false;  
    }
}
