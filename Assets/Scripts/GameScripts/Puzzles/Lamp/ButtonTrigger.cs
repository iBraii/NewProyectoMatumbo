using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTrigger : MonoBehaviour
{
    public GameObject lamp;
    private LampM sc_lampM;
    private void Start()
    {
        sc_lampM = lamp.gameObject.GetComponent<LampM>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Box"))
        {
            //Debug.Log("ButtonPressed");
            sc_lampM.buttonsPressedCounter ++;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Box"))
        {
            //Debug.Log("ButtonUnpressed");
            sc_lampM.buttonsPressedCounter--;
        }
    }
}
