using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Creditos : MonoBehaviour
{
    public GameObject panel;
    public bool end;
    public float initialVolume;
    public AudioSource ad;
    void Start()
    {
        end = false;
        initialVolume = PlayerPrefs.GetFloat("volume");
        initialVolume *= ad.volume;
        ad.volume = initialVolume;
        SoundManager.instance.Stop("PlayerIdle");
        SoundManager.instance.Stop("LowRumble");
    }

    // Update is called once per frame
    void Update()
    {
        VolumeChange();
        
    }

    public void ActivatePanel()
    {
        panel.SetActive(true);
    }
    public void SetEnd()
    {
        end = true;
    }
    void VolumeChange()
    {
        if (end)
        {
            ad.volume -=Time.deltaTime/18;
            Debug.Log(ad.volume);
        }
    }
    public void LoadLevel()
    {
        SceneManager.LoadScene(0);
    }
}
