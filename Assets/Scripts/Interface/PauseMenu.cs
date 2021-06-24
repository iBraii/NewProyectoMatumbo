using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Cinemachine;

public class PauseMenu : MonoBehaviour
{
    public bool gameIsPaused;
    public bool optionsOn;
    public GameObject obj_pausePanel;
    public GameObject obj_optionsPanel;
    public GameObject obj_player;

    public GameObject obj_sensSlider;
    public GameObject obj_volumeSlider;
    public GameObject obj_timerToggle;
    public GameObject obj_levelTimer;

    public CinemachineFreeLook thirdPersonCamera;
    void Start()
    {
        gameIsPaused = false;
        optionsOn = false;
        obj_timerToggle.GetComponent<Toggle>().isOn = false;
        obj_player = GameObject.Find("Player");

        obj_sensSlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("Sens");
        obj_volumeSlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("volume");
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInput();
        TimerController();
    }
    void PlayerInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape)&&optionsOn==false)
        {
            if (gameIsPaused)
            {
                gameIsPaused = false;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                ResumeGame();
            }else if (!gameIsPaused)
            {
                PauseGame();
            }
        }

        
    }
    public void PauseGame()
    {
        gameIsPaused = true;
        Time.timeScale = 0f;
        obj_pausePanel.SetActive(true);
        
    }
    public void ApplyAndClose()
    {
        PlayerPrefs.SetFloat("Sens", obj_sensSlider.GetComponent<Slider>().value);
        PlayerPrefs.SetFloat("volume", obj_volumeSlider.GetComponent<Slider>().value);

        thirdPersonCamera.m_XAxis.m_MaxSpeed = PlayerPrefs.GetFloat("Sens");
        optionsOn = false;
        CloseOptions();
    }

    public void ResumeGame()
    {
        gameIsPaused = false;
        Time.timeScale = 1f;
        obj_pausePanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void OpenOptions()
    {
        optionsOn = true;
        obj_optionsPanel.SetActive(true);
        obj_pausePanel.SetActive(false);
    }
    public void CloseOptions()
    {
        obj_optionsPanel.SetActive(false);
        obj_pausePanel.SetActive(true);
    }
    public void TimerController()
    {
        if (obj_timerToggle.GetComponent<Toggle>().isOn)
        {
            obj_levelTimer.SetActive(true);
        }
        else
        {
            obj_levelTimer.SetActive(false);
        }
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Closing game...");
    }
}
