using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public bool tutorialCompleted, animaticSeen;
    public string sceneToLoad;
    public GameObject obj_optionsPanel;
    public GameObject obj_rankingPanel;
    public GameObject obj_mainPanel;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadGame()
    {
        if (tutorialCompleted && animaticSeen)
        {
            SceneManager.LoadScene("Level1");
        }
        else
        {
            SceneManager.LoadScene("AnimaticStart");
        }
    }
    public void QuitGame()
    {
        LoadScene.CloseGame();
    }

    public void OpenOptions()
    {
        obj_optionsPanel.SetActive(true);
        obj_mainPanel.SetActive(false);
    }
    public void RankingTable()
    {
        obj_rankingPanel.SetActive(true);
        obj_mainPanel.SetActive(false);
    }

    public void BackButton()
    {
        obj_mainPanel.SetActive(true);
        obj_rankingPanel.SetActive(false);
        obj_optionsPanel.SetActive(false);
    }

}
