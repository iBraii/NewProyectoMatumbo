using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerModel))]
public class PlayerCtrl : MonoBehaviour
{
    //------Scripts--------
    private PlayerModel sc_playerModel;
    private PlayerController sc_playerController;
    //---------------------


    //------Keys--------
    public KeyCode keyUp;
    public KeyCode keyDown;
    public KeyCode keyLeft;
    public KeyCode keyRight;
    public KeyCode interactKey;
    public KeyCode hideKey;
    public KeyCode jumpKey;
    //------------------

    //------Inputs------
    public float horizontal;
    public float vertical;
    //------------------



    void Start()
    {
        sc_playerModel = GetComponent<PlayerModel>();
        sc_playerController = GetComponent<PlayerController>();
    }


    void Update()
    {
        //Move();
        BoxPush();
        HideOnBlanket();
        DreamCatcher();
        StoryHintsControl();
        
    }

    //---------------------------------------------------DreamCatcher----------------------------------------------------------------------

    void DreamCatcher()
    {
        if(sc_playerModel.hasWeapon)
        {
            if(Input.GetKey(KeyCode.Q))
            {
                sc_playerModel.isUsingWeapon = true;
            }
            else if (Input.GetKeyUp(KeyCode.Q))
            {
                sc_playerModel.isUsingWeapon = false;
            }
            if(sc_playerModel.isUsingWeapon)
            {
                sc_playerModel.obj_enemyTest.GetComponent<EnemyTesting>().Paralize();
                sc_playerModel.useLimit -= 1 * Time.deltaTime;
            }
            else
            {

            }
            if(sc_playerModel.useLimit <= 0)
            {
                sc_playerModel.isUsingWeapon = false;
                sc_playerModel.hasWeapon = false;
                sc_playerModel.useLimit = 0;
            }
        }
    }


    //------------------------------------------------------------------------------------------------------------------------------------










    //-------------------------------------------------Empuje de cajas----------------------------------------------------------------------------
    void BoxPush()
    {
        sc_playerModel.isCloseToBox = Physics.Raycast(transform.position, Vector3.forward, sc_playerModel.distanceToBox, sc_playerModel.playerLook);
        if (sc_playerModel.isCloseToBox == true && Input.GetKey(interactKey))
        {
            sc_playerModel.obj_box.gameObject.GetComponent<Rigidbody>().AddForce(sc_playerModel.pos_pushBoxDir * Time.deltaTime);
        }
    }
    //----------------------------------------------------------------------------------------------------------------------------------------








    //---------------------------------------------Mantita---------------------------------------------
    void HideOnBlanket()
    {
        /*if (Input.GetKeyDown(interactKey))
        {*/
            if (sc_playerModel.isCloseToBlanket && sc_playerModel.isHiding == false)
            {
               if(Input.GetKeyDown(hideKey))
                {
                    sc_playerModel.isInmune = true;
                    sc_playerModel.isHiding = true;
                }
            }

            if (sc_playerModel.isCloseToBlanket && sc_playerModel.isHiding == true)
            {

                if(Input.GetKeyDown(interactKey))
                {
                    sc_playerModel.isInmune = false;
                    sc_playerModel.isHiding = false;
                }

            }
        //}
    }
    //------------------------------------------------------------------------------------------------------


    //----------------------------------------maxStoryHints-------------------------------------------------
    void StoryHintsControl()
    {
        if(sc_playerModel.storyHints >= sc_playerModel.maxStoryHints)
        {
            sc_playerModel.storyHints = sc_playerModel.maxStoryHints;
        }
        if (sc_playerModel.storyHints < 1)
        {
            sc_playerModel.storyHints = 0;
        }
    }


    //------------------------------------------------------------------------------------------------------





    //--------------------Triggers-------------------------------------
    private void OnTriggerEnter(Collider other)
    {

        PickDC(other);
        DetectBlanket(other);
        PickSH(other);

    }
    private void OnTriggerStay(Collider other)
    {


        PickDC(other);
        DetectBlanket(other);
        PickSH(other);


    }

    private void OnTriggerExit(Collider other)
    {
        //--------------Blanket----------------------------------
        if (other.gameObject.CompareTag("Blanket"))
        {
            sc_playerModel.isCloseToBlanket = false;
        }
        //---------------------------------------------------------
    }
    //---------------------------------------------------------------------------------


    void PickDC(Collider other)
    {
        //--------------DreamCatcher----------------------------
        if (other.gameObject.CompareTag("DreamCatcher"))
        {
            if (Input.GetKeyDown(interactKey))
            {
                sc_playerModel.hasWeapon = true;
                sc_playerModel.useLimit = 20;
                other.gameObject.SetActive(false);
            }
        }
        //-------------------------------------------------------
    }
    void PickSH(Collider other)
    {
        //--------------StoryHints----------------------------
        if (other.gameObject.CompareTag("StoryHints"))
        {
            if (Input.GetKeyDown(interactKey))
            {
                sc_playerModel.storyHints += 1;
                other.gameObject.SetActive(false);
            }
        }
        //-------------------------------------------------------
    }
    void DetectBlanket(Collider other)
    {
        //--------------Blanket----------------------------------
        if (other.gameObject.CompareTag("Blanket"))
        {
            sc_playerModel.isCloseToBlanket = true;
        }            
        //-------------------------------------------------------
    }












    //----------------Movement--------------------------------------------------------
    void Move()
    {
            if (sc_playerModel.walkActive == true)
            {

                float angl_rot = gameObject.transform.eulerAngles.y % 360;
                float hor = 0;
                float ver = 0;
                if (Input.GetKey(keyRight))
                {
                    hor = -1;
                    sc_playerController.Move_in_X(sc_playerModel.movSpeed, 1);
                    angl_rot = 90;
                }
                else if (Input.GetKey(keyLeft))
                {
                    hor = 1;
                    sc_playerController.Move_in_X(sc_playerModel.movSpeed, -1);
                    angl_rot = 270;
                }
                else
                {
                    hor = 0;
                }

                if (Input.GetKey(keyUp))
                {
                    ver = -1;
                    sc_playerController.Move_in_Z(sc_playerModel.movSpeed, 1);
                    angl_rot = 0;
                }
                else if (Input.GetKey(keyDown))
                {
                    ver = 1;
                    sc_playerController.Move_in_Z(sc_playerModel.movSpeed, -1);
                    angl_rot = 180;
                }
                else
                {
                    ver = 0;
                }
                //------------------------------------------------//
                if (Input.GetKey(keyRight) & Input.GetKey(keyUp))
                {
                    angl_rot = 45;
                }
                else if (Input.GetKey(keyRight) & Input.GetKey(keyDown))
                {
                    angl_rot = 135;
                }
                if (Input.GetKey(keyLeft) & Input.GetKey(keyUp))
                {
                    angl_rot = 315;
                }
                else if (Input.GetKey(keyLeft) & Input.GetKey(keyDown))
                {
                    angl_rot = 225;
                }
                sc_playerController.rotatioProves = new Vector3(hor, 0f, ver);
                sc_playerController.Rote_Y_Two(sc_playerController.rotatioProves.normalized, 1.0f);   
        }
    }
    //-----------------------------------------------------------------------------------------------------


}
