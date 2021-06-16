using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public bool tutorialCompleted, animaticSeen;
    public string sceneToLoad;
    public GameObject obj_optionsPanel;
    public GameObject obj_rankingPanel;
    public GameObject obj_mainPanel;
    private ChangeScene sc_changescn;
    public GameObject obj_chapterSelectionPanel;

    public GameObject obj_chapter2Block;
    public GameObject obj_chapter3Block;

    public GameObject obj_sensSlider;
    public GameObject obj_volumeSlider;
    public int level1;
    public int level2;
    void Start()
    {
        obj_sensSlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("Sens");
        obj_volumeSlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("volume");
        SoundManager.instance.generalVolume = PlayerPrefs.GetFloat("volume");
        sc_changescn = GetComponent<ChangeScene>();
        UpdateChapterSelection();
        level1 = PlayerPrefs.GetInt("TutorialCompleted");
        level2 = PlayerPrefs.GetInt("Chapter2Completed");
        ApplySettings();
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        if (Input.GetKeyDown(KeyCode.M))
        {
            PlayerPrefs.SetInt("TutorialCompleted",0);
            PlayerPrefs.SetInt("Chapter2Completed",0);
            Start();
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            PlayerPrefs.SetInt("TutorialCompleted", 1);
            PlayerPrefs.SetInt("Chapter2Completed", 1);
            Start();
        }
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
        obj_sensSlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("Sens");
        obj_volumeSlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("volume");
        obj_mainPanel.SetActive(true);
        obj_rankingPanel.SetActive(false);
        obj_optionsPanel.SetActive(false);
    }
    public void OpenChapterSelection()
    {
        obj_chapterSelectionPanel.SetActive(true);
        obj_mainPanel.SetActive(false);
    }
    public void CloseChapterSelection()
    {
        obj_chapterSelectionPanel.SetActive(false);
        obj_mainPanel.SetActive(true);
    }

    public void UpdateChapterSelection()
    {
        if (PlayerPrefs.GetInt("TutorialCompleted") == 1)
        {
            obj_chapter2Block.SetActive(false);
        }
        else
        {
            obj_chapter2Block.SetActive(true);
        }

        if (PlayerPrefs.GetInt("Chapter2Completed") == 1)
        {
            obj_chapter3Block.SetActive(false);
        }
        else 
        {
            obj_chapter3Block.SetActive(true);
        }
    }
    public void ApplySettings()
    {
        PlayerPrefs.SetFloat("Sens", obj_sensSlider.GetComponent<Slider>().value);
        PlayerPrefs.SetFloat("volume", obj_volumeSlider.GetComponent<Slider>().value);
        SoundManager.instance.generalVolume = PlayerPrefs.GetFloat("volume");
    }
}
