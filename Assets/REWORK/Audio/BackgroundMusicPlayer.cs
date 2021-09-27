using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundMusicPlayer : MonoBehaviour
{
    Scene scene;
    private void Start()
    {
        scene = SceneManager.GetActiveScene();
        switch(scene.name)
        {
            case "Main Menu":
                SoundManager.instance.Play("MenuTheme");
                SoundManager.instance.Stop("BG1");
                break;
            case "Comics":
                SoundManager.instance.UpdatePlay("MenuTheme");
                SoundManager.instance.Stop("BG1");
                break;
            case "Nivel Tutorial":
                SoundManager.instance.Stop("MenuTheme");
                SoundManager.instance.Play("BG1");
                break;
            case "DefeatScene":
                SoundManager.instance.Stop("BG1");
                break;
        }
    }
}
