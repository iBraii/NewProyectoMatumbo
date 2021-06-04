using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CulpaV : MonoBehaviour
{
    public CulpaC sc_culpaC;
    void Start()
    {
        sc_culpaC = GetComponent<CulpaC>();

    }

    // Update is called once per frame
    void Update()
    {
        sc_culpaC.CloseToWaypoint();

    }
}
