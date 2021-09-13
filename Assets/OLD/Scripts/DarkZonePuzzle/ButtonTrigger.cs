using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTrigger : MonoBehaviour
{
    public GameObject lamp;
    private LampM sc_lampM;
    public bool noSum;
    private void Start()
    {
        sc_lampM = lamp.gameObject.GetComponent<LampM>();
        noSum = false;
    }
    private void Update()
    {
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Box") && noSum == false)
        {
            Debug.Log("ButtonPressed");
            sc_lampM.buttonsPressedCounter++;
            noSum = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Box") && noSum == true)
        {
            Debug.Log("ButtonUnpressed");
            sc_lampM.buttonsPressedCounter--;
            noSum = false;
        }
    }
}
