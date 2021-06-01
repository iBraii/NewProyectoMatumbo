using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectV : MonoBehaviour
{
    private ObjectC sc_ObjectC;
    private ObjectM sc_ObjectM;
    void Start()
    {
        sc_ObjectC = GetComponent<ObjectC>();
        sc_ObjectM = GetComponent<ObjectM>();
    }

    // Update is called once per frame
    void Update()
    {
        sc_ObjectC.PlayerDistanceCheck();
        sc_ObjectM.Denied();
        sc_ObjectC.Stunned();
        
    }
}
