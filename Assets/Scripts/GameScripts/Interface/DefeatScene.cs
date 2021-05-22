using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DefeatScene : MonoBehaviour
{
    public Text hintsText;
    public string[] hints;
    void Start()
    {
        hints = new string[3];
        hints[0] = "You can hide in some places";
        hints[1] = "You can hide in some places";
        hints[2] = "You can hide in some places";

        hintsText.text = hints[0];
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void LoadPrevLevel()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("prevLevel"));
    }
}
