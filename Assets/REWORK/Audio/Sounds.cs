using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
[System.Serializable]
public class Sounds
{
    public string name;
    public AudioClip audioClip;
    public bool loop;

    [Range(0f, 1f)]
    public float volume;
    [Range(0f, 1f)]
    public float pitch;
    [HideInInspector]
    public AudioSource audioSource;
}
