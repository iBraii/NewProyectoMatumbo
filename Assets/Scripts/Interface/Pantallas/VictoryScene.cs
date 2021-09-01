using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VictoryScene : MonoBehaviour
{
    public Text storyContextText;
    public int previousLevel;
    public string[] story;
    void Start()
    {
        story = new string[3];
        story[0] = "The true nightmare begins...";
        story[1] = "The true nightmare begins...";
        story[2] = "The true nightmare begins...";

        storyContextText.text = story[0];
        previousLevel = PlayerPrefs.GetInt("prevLevel");
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("prevLevel")+1);
    }
    public void PlayMusic()
    {
        SoundManager.instance.Play("ButtonPressed");
    }
}
