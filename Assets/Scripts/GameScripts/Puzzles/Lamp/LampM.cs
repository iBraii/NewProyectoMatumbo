using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampM : Puzzles
{
    public bool allButtonsPressed;
    public int buttonsCounter;
    public int buttonsPressedCounter;
    public bool isLightOn;
    public GameObject obj_light;
    public GameObject obj_blockedZone;

    // Start is called before the first frame update
    void Start()
    {
        isCompleted = false;
        allButtonsPressed = false;
        buttonsPressedCounter = 0;
        isLightOn = false;
    }

}
