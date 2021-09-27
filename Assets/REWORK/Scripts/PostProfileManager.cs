using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PostProfileManager : MonoBehaviour
{
    public static PostProfileManager Instance;
    private Volume vol;
    public VolumeProfile[] profile;
    
    void Awake()
    { 
        Instance = this;
        vol = GetComponent<Volume>();
    }

    public void ChangeProfile(int index)
    {
        vol.profile = profile[index];
    }
}
