using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MisionChecker : MonoBehaviour
{
    public int misionIndex;
    public GameObject[] aditionalObj;
    void Start()
    {
        //SaveSystem.Load();
        if (SaveSystem.data.achievementCompleted[misionIndex])
        {
            gameObject.SetActive(false);
            for(int i = 0; i < aditionalObj.Length; i++)
            {
                aditionalObj[i].SetActive(false);
            }
        }
            
    }

    
}
