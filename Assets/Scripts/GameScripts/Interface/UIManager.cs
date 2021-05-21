using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Image stressBar;
    public GameObject obj_player;
    public PlayerM sc_playerM;
    public PlayerC sc_playerC;

    public Image dreamCatcher;
    public Sprite[] dreamCatcherSprite;

    public Image dreamCatcherBar;

    public Text hintText;
    
    void Start()
    {
        obj_player = GameObject.Find("Player");
        sc_playerC = obj_player.GetComponent<PlayerC>();
        PlayerPrefs.SetInt("prevLevel", SceneManager.GetActiveScene().buildIndex);
        Debug.Log(PlayerPrefs.GetInt("prevLevel"));
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHUD();
        CursorController();

        if (Input.GetKeyDown(KeyCode.P))
        {
            SceneManager.LoadScene("VictoryScene");
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
        if (sc_playerM.closeToPuzzle && !sc_playerM.solvingPuzzle)
        {
            
            hintText.text = "E TO INTERACT";
        }else if (sc_playerC.obj_Box!=null && !Input.GetKey(KeyCode.E))
        {
            hintText.text = "HOLD E TO INTERACT";
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
