using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HumanoidC))]
[RequireComponent(typeof(HumanoidM))]
public class HumanoidV : MonoBehaviour
{
    private HumanoidC sc_humanoidC;
    private HumanoidM sc_humanoidM;
    // Start is called before the first frame update
    void Start()
    {
        sc_humanoidC = GetComponent<HumanoidC>();
        sc_humanoidM = GetComponent<HumanoidM>();
    }

    // Update is called once per frame
    void Update()
    {
        sc_humanoidC.CloseToWaypoint();
        sc_humanoidC.FollowPlayer();
        sc_humanoidC.DetectPlayer();
        sc_humanoidM.Denied();
        sc_humanoidC.Stunned();


        //Prueba de aturdimiento

        if (Input.GetMouseButtonDown(1))
        {
            sc_humanoidM.isDenied = true;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            sc_humanoidM.playerOnView = true;
        }
    }  
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            sc_humanoidM.playerOnView = false;
        }
    }
}
