using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MisionChecker : MonoBehaviour
{
    [SerializeField] private GameObject[] aditionalObj;

    void Start()
    {
        InteractText interact = FindObjectOfType<InteractText>();
        if (interact == null) return;
        if (SaveSystem.data.achievementCompleted[interact.collectableIndex])
        {
            gameObject.SetActive(false);
            for (int i = 0; i < aditionalObj.Length; i++)
                aditionalObj[i].SetActive(false);
        }
    }
}