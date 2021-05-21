using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowV : MonoBehaviour
{
    private ShadowC sc_ShadowC;
    private ShadowM sc_ShadowM;
    void Start()
    {
        sc_ShadowC = GetComponent<ShadowC>();
        sc_ShadowM = GetComponent<ShadowM>();
    }

    // Update is called once per frame
    void Update()
    {
        sc_ShadowC.CheckDistanceToPlayer();
        sc_ShadowC.ChasePlayer();
        sc_ShadowM.Denied();
        sc_ShadowC.Stunned();
    }
}
