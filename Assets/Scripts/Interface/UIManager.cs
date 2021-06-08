using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Cinemachine;

public class UIManager : MonoBehaviour
{
    public Image stressBar;
    public GameObject obj_player;
    public PlayerM sc_playerM;
    public PlayerC sc_playerC;
    public ChangeScene sc_changescn;
    public Image dreamCatcher;
    public Sprite[] dreamCatcherSprite;

    public Image dreamCatcherBar;

    public Text hintText;
    public CinemachineFreeLook thirdPersonCamera;
    
    void Start()
    {
        sc_changescn = GetComponent<ChangeScene>();
        obj_player = GameObject.Find("Player");
        sc_playerC = obj_player.GetComponent<PlayerC>();
        PlayerPrefs.SetInt("prevLevel", SceneManager.GetActiveScene().buildIndex);
        Debug.Log(PlayerPrefs.GetInt("prevLevel"));
        thirdPersonCamera = GameObject.Find("ThirdPersonCamera").GetComponent<CinemachineFreeLook>();
        thirdPersonCamera.m_XAxis.m_MaxSpeed = PlayerPrefs.GetFloat("Sens");
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHUD();
        CursorController();
        KillPlayer();
    }
    void KillPlayer()
    {
        if (obj_player.gameObject.GetComponent<PlayerM>().life >= 10)
        {
            sc_changescn.Change("DefeatScene");  
        }
    }
    public void UpdateHUD()
    {
        sc_playerM = obj_player.GetComponent<PlayerM>();
        //------------------------------Stres Bar---------------------------------------------------------------------
        stressBar.fillAmount = sc_playerM.life / sc_playerM.maxStress;
        //------------------------------DreamCatcher---------------------------------------------------------------------
        if (sc_playerM.hasWeapon)
        {
            dreamCatcher.GetComponent<Image>().sprite = dreamCatcherSprite[0];
        }
        else
        {
            dreamCatcher.GetComponent<Image>().sprite = dreamCatcherSprite[1];
        }
        //------------------------------DreamCatcher bar---------------------------------------------------------------------

        if (sc_playerM.hasWeapon)
        {
            dreamCatcherBar.GetComponent<Image>().enabled = true;
            dreamCatcherBar.fillAmount = sc_playerM.useLimit / 20;
        }
        else
        {
            dreamCatcherBar.GetComponent<Image>().enabled = false;
        }

        //-------------------------Hint text------------------------------------------------------------------------------
        //if (sc_playerM.closeToPuzzle && !sc_playerM.solvingPuzzle)
        //{

        //    hintText.text = "E TO INTERACT";
        //} else 
        
        if (sc_playerC.obj_Box != null)
        {
            if(sc_playerC.obj_Box.BoxLeftSide() || sc_playerC.obj_Box.BoxRightSide() || sc_playerC.obj_Box.BoxForwardSide() || sc_playerC.obj_Box.BoxBackSide())
            {
                if(sc_playerM.isMovingBox == false)
                {
                    hintText.text = "HOLD E TO INTERACT";
                }      
                else
                {
                    hintText.text = "";
                }
            }     
        }
        else
        {
            hintText.text = "";
        }
    }

    public void CursorController()
    {
        

        if (GetComponent<PauseMenu>().gameIsPaused||sc_playerM.solvingPuzzle)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

        }
        else if(!GetComponent<PauseMenu>().gameIsPaused && !sc_playerM.solvingPuzzle)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        if (Input.GetMouseButton(0)&&sc_playerM.solvingPuzzle)
        {
            Cursor.visible = false;
        }
        
    }
}
