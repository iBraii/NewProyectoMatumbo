using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    bool denied;
    public bool inPlayerAtkRange;

    private void Awake()
    {
        denied = false;
        Dreams.onWeaponUsed += Deny;
    }
    private void OnDisable()
    {
        Dreams.onWeaponUsed -= Deny;
    }
    public void Deny()
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
