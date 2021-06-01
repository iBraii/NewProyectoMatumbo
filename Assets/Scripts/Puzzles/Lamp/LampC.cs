using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampC : MonoBehaviour
{
    private LampM sc_lampM;

    // Start is called before the first frame update
    void Start()
    {
        sc_lampM = GetComponent<LampM>();
    }
    public void ButtonsController()
    {
        if(sc_lampM.buttonsCounter == sc_lampM.buttonsPressedCounter)
        {
            sc_lampM.allButtonsPressed = true;
        }
        else
        {
            sc_lampM.allButtonsPressed = false;
        }
    }
    public void CompletedUpdater()
    {
        if (sc_lampM.allButtonsPressed)
        {
            sc_lampM.isCompleted = true;
        }
        else
            sc_lampM.isCompleted = false;
        if(sc_lampM.isCompleted)
        {
            sc_lampM.isLightOn = true;
        }
        else
        {
            sc_lampM.isLightOn = false;
        }
    }
    public void LightAndZoneUpdater()
    {
        if(sc_lampM.isLightOn)
        {
            sc_lampM.obj_light.SetActive(true);
            sc_lampM.obj_blockedZone.SetActive(false);
        }
        else
        {
            sc_lampM.obj_light.SetActive(false);
            sc_lampM.obj_blockedZone.SetActive(true);
        }
    }
    
}
