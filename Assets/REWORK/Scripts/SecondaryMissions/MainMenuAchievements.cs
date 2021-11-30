using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuAchievements : MonoBehaviour
{
    public GameObject[] lights;

    void Start() =>  ActivateLights();

    public void ActivateLights()
    {
        for (int i = 0; i < lights.Length; i++)
        {
            if (SaveSystem.data.achievementCompleted[i])
                lights[i].SetActive(true);
            else lights[i].SetActive(false);
        }
    }
}
