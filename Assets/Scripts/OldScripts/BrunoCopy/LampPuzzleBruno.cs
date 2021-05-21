using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampPuzzleBruno : PuzzlesBruno
{
    public bool allButtonsPressed;
    public int buttonsPressedCounter;
    public GameObject[] array_objButtons;
    public bool isLightOn;

    private void  ButtonsController()
    {

    }
    private void LightsController()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("ButtonPressed");
        }
    }
}
