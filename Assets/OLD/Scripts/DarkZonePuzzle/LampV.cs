using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampV : MonoBehaviour
{
    private LampM sc_lampM;
    private LampC sc_lampC;
    private void Start()
    {
        sc_lampM = GetComponent<LampM>();
        sc_lampC = GetComponent<LampC>();
    }
    void Update()
    {
        sc_lampC.ButtonsController();
        sc_lampC.CompletedUpdater();
        sc_lampC.LightAndZoneUpdater();
    }
}
