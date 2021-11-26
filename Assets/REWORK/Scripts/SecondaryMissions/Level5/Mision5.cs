using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Mision5 : MonoBehaviour
{
    public int totalInteractions;
    public int currentInteractions;
    public bool completed = false;
    [HideInInspector] public PlayerInput input;
    [HideInInspector] public InteractText interact;
    void Start()
    {
        input = FindObjectOfType<PlayerInput>();
        interact = FindObjectOfType<InteractText>();
        totalInteractions = transform.childCount;
    }
    void Update()
    {
        CheckIfComplete();
    }

    private void CheckIfComplete()
    {
        if (currentInteractions == totalInteractions && completed == false)
        {
            completed = true;
            AchievementPop.onMisionCompleted?.Invoke("You faced your guilt");
            SaveSystem.data.achievementCompleted[interact.collectableIndex] = true;
            SaveSystem.Save();
            if (FindObjectOfType<GameJoltTrophies>())
                FindObjectOfType<GameJoltTrophies>().CompareTrophies();
        }
    }
}
