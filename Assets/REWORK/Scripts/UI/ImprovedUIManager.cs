using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class ImprovedUIManager : MonoBehaviour
{
    [HideInInspector]
    public bool gameIsPaused=false;
    private PlayerInput playerInput;
    private InputAction escapeAction;

    private GameObject pausePanel;
    private GameObject optionPanel;

    public CinemachineFreeLook cm;
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        escapeAction = playerInput.actions["Escape"];
    }
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        SoundManager.instance.Play("BG1");

        pausePanel = GameObject.Find("PausePanel");
        optionPanel = GameObject.Find("OptionsPanel");
        pausePanel.SetActive(false);
        optionPanel.SetActive(false);
    }
    private void Update()
    {
        InputCheck();
    }
    private void InputCheck()
    {
        if (escapeAction.triggered)
            PauseGame();
    }
    public void PauseGame()
    {
        gameIsPaused = !gameIsPaused;
        if (gameIsPaused)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;

        CursorToggle();
    }
   
    public void CursorToggle()
    {
        if (gameIsPaused)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            pausePanel.SetActive(true);
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            pausePanel.SetActive(false);
            optionPanel.SetActive(false);
        }
    }

    public void SetCameraSensitivity()
    {
        cm.m_XAxis.m_MaxSpeed=PlayerPrefs.GetFloat("Sensitivity");
        cm.m_YAxis.m_MaxSpeed = PlayerPrefs.GetFloat("Sensitivity")/250;
    }
    
}
