using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
[RequireComponent(typeof(PlayerM))]
public class PlayerC : MonoBehaviour
{
    //------Scripts--------
    private PlayerM sc_playerM;
    public BoxPush obj_Box;
    
    //private PlayerController sc_playerController;
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
        sc_playerM = GetComponent<PlayerM>();
        //sc_playerController = GetComponent<PlayerController>();
    }

    public void AddStress()
    {
        sc_playerM.life++;
        sc_playerM.stressCooldown = 0;
    }

    public void ReduceStress(int ammount)
    {
        sc_playerM.life -= ammount;
    }
    
    public void Die()
    {
        if(sc_playerM.life >= 10)
        {
            gameObject.SetActive(false);
        }
    }


    public void PlayerJump()
    {
        sc_playerM.GroundCheck();
        sc_playerM.grounded = sc_playerM.GroundCheck();
        sc_playerM.pos_playerVelocity.y += sc_playerM.gravity * Time.deltaTime;

        if (sc_playerM.grounded && sc_playerM.pos_playerVelocity.y < 0)
        {
            sc_playerM.pos_playerVelocity.y = 0f;
        }

        if (sc_playerM.pos_playerVelocity.y > 0)
        {
            sc_playerM.pos_playerVelocity += Vector3.up * sc_playerM.gravity * Time.deltaTime;
        }
        sc_playerM.cmp_controller.Move(sc_playerM.pos_playerVelocity * Time.deltaTime);


        if (Input.GetKeyDown(KeyCode.Space) && sc_playerM.grounded)
        {
            sc_playerM.pos_playerVelocity.y += Mathf.Sqrt(sc_playerM.jumpForce * -3f * sc_playerM.gravity);
        }

    }

    //---------------------------------------------------DreamCatcher----------------------------------------------------------------------

    public void DreamCatcher()
    {
        if (sc_playerM.hasWeapon)
        {
            if (Input.GetKey(KeyCode.Q))
            {
                sc_playerM.isUsingWeapon = true;
            }
            else if (Input.GetKeyUp(KeyCode.Q))
            {
                sc_playerM.isUsingWeapon = false;
            }
            if (sc_playerM.isUsingWeapon)
            {
                
                //sc_playerM.obj_enemyTest.GetComponent<EnemyTesting>().Paralize();
                sc_playerM.useLimit -= 1 * Time.deltaTime;
            }
            else
            {

            }
            if (sc_playerM.useLimit <= 0)
            {
                sc_playerM.isUsingWeapon = false;
                sc_playerM.hasWeapon = false;
                sc_playerM.useLimit = 0;
            }
        }
    }


    //------------------------------------------------------------------------------------------------------------------------------------


    //-------------------------------------------------Empuje de cajas----------------------------------------------------------------------------
    public void BoxPush()
    {
        /*sc_playerM.pos_lookDirection = sc_playerM.pos_lookAt.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(sc_playerM.pos_lookDirection);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation,10);*/
        //if(obj_Box.canPushz && obj_Box.canPushx&& Input.GetKey(interactKey) && Input.GetKey(keyUp))
        //{           
        //       obj_Box.PushBox(3, 3);
        //        sc_playerM.isMovingBox = true;                    
        //}
        //else if (!Input.GetKey(interactKey) || !Input.GetKey(keyUp) && !obj_Box.canPushz && !obj_Box.canPushx)
        //{
        //    sc_playerM.isMovingBox = false;
        //    obj_Box.PushBox(0, 0);
        //}
        ///////////////////////////////////
        //if(obj_Box.canPushx && !obj_Box.canPushz&& Input.GetKey(interactKey) && Input.GetKey(keyUp))
        //{

        //        obj_Box.PushBox(3, 0);
        //        sc_playerM.isMovingBox = true;        

        //}
        //else if (!Input.GetKey(interactKey) || !Input.GetKey(keyUp) && !obj_Box.canPushx)
        //{
        //    sc_playerM.isMovingBox = false;
        //    obj_Box.PushBox(0, 0);
        //}
        ////////////////////////////////////////
        // if(!obj_Box.canPushx && obj_Box.canPushz&& Input.GetKey(interactKey) && Input.GetKey(keyUp))
        //{ 
        //    sc_playerM.isMovingBox = true;
        //    obj_Box.PushBox(0, 3);   
        //}
        //else if (!Input.GetKey(interactKey) || !Input.GetKey(keyUp) && !obj_Box.canPushz)
        //{
        //    sc_playerM.isMovingBox = false;
        //    obj_Box.PushBox(0, 0);
        //}
        // ///////////////////////////////////////////
        //if(!obj_Box.canPushx && !obj_Box.canPushz&&Input.GetKey(interactKey) && Input.GetKey(keyUp))
        //{            
        //        obj_Box.PushBox(0, 0);
        //        sc_playerM.isMovingBox = true;                   
        //}
        //else if (!Input.GetKey(interactKey) || !Input.GetKey(keyUp) && !obj_Box.canPushz && !obj_Box.canPushx)
        //{
        //    sc_playerM.isMovingBox = false;
        //    obj_Box.PushBox(0, 0);
        //}
        
        if (Input.GetKey(interactKey) && Input.GetKey(keyUp))
        {
            if(obj_Box.canPushz && obj_Box.canPushx)
            {
                obj_Box.PushBox(3, 3);
                sc_playerM.isMovingBox = true;
            }else if(!obj_Box.canPushz && !obj_Box.canPushx)
            {
                sc_playerM.isMovingBox = false;
                obj_Box.PushBox(0, 0);
            }

            else if(obj_Box.canPushx && !obj_Box.canPushz)
            {
                sc_playerM.isMovingBox = true;
                obj_Box.PushBox(0, 3);
            }else if (!obj_Box.canPushx)
            {
                sc_playerM.isMovingBox = false;
                obj_Box.PushBox(0, 0);
            }else if(!obj_Box.canPushx && obj_Box.canPushz)
            {
                sc_playerM.isMovingBox = true;
                obj_Box.PushBox(0, 3);
            }else if (!obj_Box.canPushz)
            {
                sc_playerM.isMovingBox = false;
                obj_Box.PushBox(0, 0);
            }else if(!obj_Box.canPushx && !obj_Box.canPushz)
            {
                obj_Box.PushBox(0, 0);
                sc_playerM.isMovingBox = true;
            }else if (!obj_Box.canPushx)
            {
                sc_playerM.isMovingBox = false;
                obj_Box.PushBox(0, 0);
            }
        }
        else
        {
            obj_Box.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        }
       

    }
    public void BoxPull()
    {
        if (Input.GetKey(interactKey) && Input.GetKey(keyDown))
        {
            obj_Box.PullBox(7, 7);
        }
       
    }
    public void DetectBox(Collider other)
    {
        if (other.gameObject.CompareTag("Box"))
        {
            obj_Box = other.gameObject.GetComponent<BoxPush>();
        }
        else
        {
            obj_Box = null;
        }
    }
    //----------------------------------------------------------------------------------------------------------------------------------------

    //---------------------------------------------Mantita---------------------------------------------
    public void HideOnBlanket()
    {
        if (Input.GetKeyDown(hideKey))
        {
            if (sc_playerM.isCloseToBlanket && sc_playerM.isHiding == false)
            {
                //if (Input.GetKeyDown(hideKey))
                //{
                sc_playerM.isInmune = true;
                sc_playerM.isHiding = true;
                //}
            }
        }
        if (Input.GetKeyDown(interactKey))
        {
            if (sc_playerM.isCloseToBlanket && sc_playerM.isHiding == true)
            {

            //if (Input.GetKeyDown(interactKey))
            //{
                sc_playerM.isInmune = false;
                sc_playerM.isHiding = false;
            //}

            }
        }
    }
    //------------------------------------------------------------------------------------------------------


    //----------------------------------------maxStoryHints-------------------------------------------------
    public void StoryHintsControl()
    {
        if (sc_playerM.storyHints >= sc_playerM.maxStoryHints)
        {
            sc_playerM.storyHints = sc_playerM.maxStoryHints;
        }
        if (sc_playerM.storyHints < 1)
        {
            sc_playerM.storyHints = 0;
        }
    }


    //------------------------------------------------------------------------------------------------------


    


    public void PickDC(Collider other)
    {
        //--------------DreamCatcher----------------------------
        if (other.gameObject.CompareTag("DreamCatcher"))
        {
            if (Input.GetKeyDown(interactKey))
            {
                sc_playerM.hasWeapon = true;
                sc_playerM.useLimit = 20;
                other.gameObject.SetActive(false);
            }
        }
        //-------------------------------------------------------
    }
    public void PickSH(Collider other)
    {
        //--------------StoryHints----------------------------
        if (other.gameObject.CompareTag("StoryHints"))
        {
            if (Input.GetKeyDown(interactKey))
            {
                sc_playerM.storyHints += 1;
                other.gameObject.SetActive(false);
            }
        }
        //-------------------------------------------------------
    }
    public void IsOnBlanket(Collider other)
    {
        //--------------Blanket----------------------------------
        if (other.gameObject.CompareTag("Blanket"))
        {
            sc_playerM.isCloseToBlanket = true;
        }
        //-------------------------------------------------------
    }
    public void IsOffBlanket(Collider other)
    {
        //--------------Blanket----------------------------------
        if (other.gameObject.CompareTag("Blanket"))
        {
            sc_playerM.isCloseToBlanket = false;
        }
        //-------------------------------------------------------
    }
    public void PickSweets(Collider other)
    {
        if (other.CompareTag("Candy"))
        {
                ReduceStress(2);
                other.gameObject.SetActive(false);
        }
    }    
}
