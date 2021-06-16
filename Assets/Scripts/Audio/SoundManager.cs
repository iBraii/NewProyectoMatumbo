using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class SoundManager : MonoBehaviour
{
    [Range(0f,2f)]
    public float generalVolume;
    public Sounds[] array_sounds;
    public static SoundManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);


        foreach (Sounds sounds in array_sounds)
        {
            sounds.audioSource = gameObject.AddComponent<AudioSource>();
            sounds.audioSource.loop = sounds.loop;
            sounds.audioSource.clip = sounds.audioClip;
            sounds.audioSource.volume = sounds.volume * generalVolume;
            sounds.audioSource.pitch = sounds.pitch;
        }
    }

    void Start()
    {
        //Play("MusicTheme");
    }

    public void Play(string name)
    {
        Sounds sounds = System.Array.Find(array_sounds, sound => sound.name == name);
        if (sounds == null)
        {
            Debug.Log("El sonido " + name + " no se encontr�");
            return;
        }
        sounds.audioSource.Play();
    }
    public void Pause(string name)
    {
        Sounds sounds = System.Array.Find(array_sounds, sound => sound.name == name);
        if (sounds == null)
        {
            Debug.Log("El sonido " + name + " no se encontr�");
            return;
        }
        sounds.audioSource.Pause();
    }
    public void Stop(string name)
    {
        Sounds sounds = System.Array.Find(array_sounds, sound => sound.name == name);
        if (sounds == null)
        {
            Debug.Log("El sonido " + name + " no se encontr�");
            return;
        }
        sounds.audioSource.Stop();
    }
    public void Resume(string name)
    {
        Sounds sounds = System.Array.Find(array_sounds, sound => sound.name == name);
        if (sounds == null)
        {
            Debug.Log("El sonido " + name + " no se encontr�");
            return;
        }
        sounds.audioSource.UnPause();
    }
}
