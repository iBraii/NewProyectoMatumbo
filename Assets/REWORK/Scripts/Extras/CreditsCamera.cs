using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsCamera : MonoBehaviour
{
    [SerializeField] private ChangeScene cs;
    public void transitionToMenu()
    {
        cs.Change("Main Menu");
    }
}
