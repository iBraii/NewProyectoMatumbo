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
        switch (scene.name)
        {
            case "Main Menu":
                SoundManager.instance.Play("MenuTheme");
                SoundManager.instance.Stop("BG1");
                SoundManager.instance.Stop("LowRumble");
                SoundManager.instance.Stop("HeartBeat");
                break;
            case "Comics":
                SoundManager.instance.UpdatePlay("MenuTheme");
                SoundManager.instance.Stop("BG1");
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                break;
            case "NivelTuto":
                SoundManager.instance.Stop("MenuTheme");
                SoundManager.instance.Play("BG1");
                break;
            case "Nivel1":
                SoundManager.instance.Stop("MenuTheme");
                SoundManager.instance.Play("BG1");
                break;
            case "Nivel2":
                SoundManager.instance.Stop("MenuTheme");
                SoundManager.instance.Play("BG1");
                break; 
            case "Nivel3":
                SoundManager.instance.Stop("MenuTheme");
                SoundManager.instance.Play("BG1");
                break;
            case "DefeatScene":
                SoundManager.instance.Stop("BG1");
                SoundManager.instance.Stop("LowRumble");
                SoundManager.instance.Stop("HeartBeat");
                break;
        }
    }
}
