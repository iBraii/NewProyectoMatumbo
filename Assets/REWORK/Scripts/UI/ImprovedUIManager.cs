using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using UnityEngine.UI;

public class ImprovedUIManager : MonoBehaviour
{
    //ATRAPASUEÑOS
    public Animator DC_anim;
    float timer;
    public float maxUIDC;
    public Image DCBar;

    [HideInInspector]
    public bool gameIsPaused=false;
    private PlayerInput playerInput;
    private InputAction escapeAction;

    public GameObject pausePanel;
    public GameObject optionPanel;

    public CinemachineFreeLook cm;
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        escapeAction = playerInput.actions["Escape"];
    }
    private void Start()
    {
        SetCameraSensitivity();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        SoundManager.instance.Play("BG1");

        #region nulls
        if (pausePanel == null)
        {
            Debug.LogWarning("No hay pausepanel");
            return;
        }
        if (optionPanel == null)
        {
            Debug.LogWarning("No hay optionpanel");
            return;
        }
        #endregion
        pausePanel.SetActive(false);
        optionPanel.SetActive(false);
    }
    private void Update()
    {
        InputCheck();
        AtrapasueñosUI();
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
        #region nulls
        if (pausePanel == null)
        {
            Debug.LogWarning("No hay pausepanel");
            return;
        }
        if (optionPanel == null)
        {
            Debug.LogWarning("No hay optionpanel");
            return;
        }
        #endregion
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
        cm.m_XAxis.m_MaxSpeed= Data.Instance.setting.sensitivity;
        cm.m_YAxis.m_MaxSpeed = Data.Instance.setting.sensitivity / 250;
    }
    
    void AtrapasueñosUI()
    {
        #region nulls
        if (DC_anim == null)
        {
            Debug.LogWarning("No hay animator");
            return;
        }
        if (DCBar == null)
        {
            Debug.LogWarning("No hay DCBar");
            return;
        }
        #endregion

        DCBar.fillAmount = PlayerSingleton.Instance.dreamEnergy / PlayerSingleton.Instance.maxDreamEnergy;

        if (PlayerSingleton.Instance.usingWeap == false && PlayerSingleton.Instance.dreamEnergy >= PlayerSingleton.Instance.maxDreamEnergy && DC_anim.GetBool("Active"))
        {
            timer += Time.deltaTime;
            if (timer >= maxUIDC)
            {
                DC_anim.SetBool("Active", false);
                timer = 0;
            }
        }
        else if(PlayerSingleton.Instance.usingWeap)
        {
            DC_anim.SetBool("Active", true);
        } 
    }   
}
