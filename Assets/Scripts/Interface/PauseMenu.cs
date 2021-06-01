using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool gameIsPaused;
    public GameObject obj_pausePanel;
    public GameObject obj_optionsPanel;
    public GameObject obj_player;
    void Start()
    {
        gameIsPaused = false;
        obj_player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInput();
    }
    void PlayerInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
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
        obj_optionsPanel.SetActive(true);
    }
    public void CloseOptions()
    {
        obj_optionsPanel.SetActive(false);
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
