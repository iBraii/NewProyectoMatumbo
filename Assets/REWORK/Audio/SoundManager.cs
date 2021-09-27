using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class SoundManager : MonoBehaviour
{
    
    public Sounds[] array_sounds;
    public static SoundManager instance;
    public AudioMixerGroup mixer;
    private float smoothSound;

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
            sounds.audioSource.volume = sounds.volume * PlayerPrefs.GetFloat("Volume");
            sounds.audioSource.pitch = sounds.pitch;
            sounds.audioSource.outputAudioMixerGroup = mixer;
        }
    }
    private void Update()
    {
        foreach (Sounds sounds in array_sounds)
        {
            sounds.audioSource.volume = sounds.volume;
        }
    }

    public void ChangeIndividualVolume(string name, float maxVolume/*, float smoothTime*/)
    {
        Sounds sounds = System.Array.Find(array_sounds, sound => sound.name == name);
        if (sounds == null)
        {
            Debug.Log("El sonido " + name + " no se encontró");
            return;
        }
        //sounds.volume = Mathf.SmoothDamp(sounds.volume, maxVolume, ref smoothSound, smoothTime);
        sounds.volume = Mathf.Lerp(0.0001f, 1, maxVolume);
    }

    public void Play(string name)
    {
        Sounds sounds = System.Array.Find(array_sounds, sound => sound.name == name);
        if (sounds == null)
        {
            Debug.Log("El sonido " + name + " no se encontró");
            return;
        }
        sounds.audioSource.Play();
    }
    public void Pause(string name)
    {
        Sounds sounds = System.Array.Find(array_sounds, sound => sound.name == name);
        if (sounds == null)
        {
            Debug.Log("El sonido " + name + " no se encontró");
            return;
        }
        sounds.audioSource.Pause();
    }
    public void Stop(string name)
    {
        Sounds sounds = System.Array.Find(array_sounds, sound => sound.name == name);
        if (sounds == null)
        {
            Debug.Log("El sonido " + name + " no se encontró");
            return;
        }
        sounds.audioSource.Stop();
    }
    public void Resume(string name)
    {
        Sounds sounds = System.Array.Find(array_sounds, sound => sound.name == name);
        if (sounds == null)
        {
            Debug.Log("El sonido " + name + " no se encontró");
            return;
        }
        sounds.audioSource.UnPause();
    }
    public void UpdatePlay(string name)
    {
        Sounds sounds = System.Array.Find(array_sounds, sound => sound.name == name);
        if (sounds == null)
        {
            Debug.Log("El sonido " + name + " no se encontró");
            return;
        }
        if(sounds.audioSource.isPlaying == false)
        {
            sounds.audioSource.Play();
        }
    }
    public void SetVolume(float volumeVal)
    {
        mixer.audioMixer.SetFloat("MyExposedParam", Mathf.Log10(volumeVal) * 20);
    }
}
