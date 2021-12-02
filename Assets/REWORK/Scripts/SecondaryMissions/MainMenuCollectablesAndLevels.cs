using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCollectablesAndLevels : MonoBehaviour
{
    public GameObject[] lights;
    public GameObject[] buttons;

    void Start()
    {
        ActivateLights();
        ActivateButtons();
    }

    public void ActivateLights()
    {
        for (int i = 0; i < lights.Length; i++)
        {
            //Debug.Log("Mision "+i+" " + SaveSystem.data.achievementCompleted[i]);
            if (SaveSystem.data.achievementCompleted[i])
                lights[i].SetActive(true);
            else lights[i].SetActive(false);
        }
    }

    private void ActivateButtons()
    {
        for(int i = 1; i < buttons.Length; i++)
        {
            if (SaveSystem.data.levelCompleted[i])
                buttons[i].SetActive(true);
            else buttons[i].SetActive(false);
        }
    }
}
