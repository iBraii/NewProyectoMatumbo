using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class LoadScene
{
    
    public static void MyLoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public static void CloseGame()
    {
        Application.Quit();
        Debug.Log("Closing game...");
    }
}
