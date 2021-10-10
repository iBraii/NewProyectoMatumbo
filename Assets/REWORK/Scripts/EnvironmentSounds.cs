using UnityEngine;
using UnityEngine.Audio;

public class EnvironmentSounds : MonoBehaviour
{
    //AUDIOCOMPONENT===========
    public AudioClip[] clips;
    private AudioSource source;

    //RANDOMVARS===============
    private int index;
    private float value;
    private float stereoPan;

    //FUNCTIONVARS
    public float randomMin, randomLimit;
    private float timer;

    private void Awake()
    {
        source = GetComponent<AudioSource>();

        index = Random.Range(0, clips.Length);
        value = Random.Range(randomMin, randomLimit);
        stereoPan = Random.Range(-1f, 1);
    }
    
    void ReproduceSoundRandomizerTime()
    {
        timer += Time.deltaTime;
        if (timer >= value) 
        { 
            timer = 0;

            source.clip = clips[index];
            source.panStereo = stereoPan;

            index = Random.Range(0, clips.Length);
            value = Random.Range(randomMin, randomLimit);
            stereoPan = Random.Range(-1f, 1);

            if (!source.isPlaying) source.Play(); 
        }
    }
    private void Update()
    {
        ReproduceSoundRandomizerTime();
    }
}
