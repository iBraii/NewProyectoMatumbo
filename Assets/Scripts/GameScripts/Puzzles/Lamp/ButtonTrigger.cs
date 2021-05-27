using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTrigger : MonoBehaviour
{
    public GameObject lamp;
    private LampM sc_lampM;
    public LayerMask cajita;
    public bool noSum;
    private void Start()
    {
        sc_lampM = lamp.gameObject.GetComponent<LampM>();
        noSum = false;
    }
    private void Update()
    {
        Debug.DrawRay(transform.position, Vector3.up);
        if(Physics.Raycast(transform.position,Vector3.up, 0.5f, cajita) && noSum == false)
        {
            sc_lampM.buttonsPressedCounter++;
            noSum = true;
        }
        else if(!Physics.Raycast(transform.position, Vector3.up, 0.5f, cajita) && noSum == true)
        {
            sc_lampM.buttonsPressedCounter--;
            noSum = false;
        }
        /*if(Physics.Raycast(transform.position, Vector3.up, 0.5f, cajita))
        {
            Debug.Log("oe ya p");
        }*/
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Box"))
    //    {
    //        Debug.Log("ButtonPressed");
    //        sc_lampM.buttonsPressedCounter ++;
    //    }
    //}
    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.CompareTag("Box"))
    //    {
    //        Debug.Log("ButtonUnpressed");
    //        sc_lampM.buttonsPressedCounter--;
    //    }
    //}
}
