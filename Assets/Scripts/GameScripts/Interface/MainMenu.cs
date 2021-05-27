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
    private ChangeScene sc_changescn;
    void Start()
    {
        sc_changescn = GetComponent<ChangeScene>();
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void LoadGame()
    {
        if (tutorialCompleted && animaticSeen)
        {
            sc_changescn.Change("Level1");
        }
        else
        {
            sc_changescn.Change("AnimaticStart");
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
