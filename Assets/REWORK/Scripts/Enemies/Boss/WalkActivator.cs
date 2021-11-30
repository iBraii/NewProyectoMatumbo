using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkActivator : MonoBehaviour
{
    [SerializeField] private EspectroCulpaNuevo culpa;

    public void StartWalking()
    {
        culpa.StartWalking();
    }
}
