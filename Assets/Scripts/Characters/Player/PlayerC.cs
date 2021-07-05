using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(PlayerM))]
public class PlayerC : MonoBehaviour
{
    //------Scripts--------
    private PlayerM sc_playerM;
    private Movement sc_Movement;
    public BoxPush obj_Box;
    public GameObject box;
    
    //private PlayerController sc_playerController;
    //---------------------


    //------Keys--------
    public KeyCode keyUp;
    public KeyCode keyDown;
    public KeyCode keyLeft;
    public KeyCode keyRight;
    public KeyCode interactKey;
    public KeyCode useWeaponKey;
    public KeyCode jumpKey;
    //------------------

    //------Inputs------
    public float horizontal;
    public float vertical;
    //------------------

    void Start()
    {
        
        sc_playerM = GetComponent<PlayerM>();
        sc_Movement = GetComponent<Movement>();
        //sc_Movement.rb = GetComponent<Rigidbody>();
        //sc_Movement.rb.centerOfMass = new Vector3(0, -1.5f, 0);
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

    public void RegenStress(float regenValue, float initialDelay)
    {
        if(sc_playerM.life > 0)
        {
            sc_playerM.cooldownForStressRegen += Time.deltaTime;
            if (sc_playerM.cooldownForStressRegen >= initialDelay)
            {
                sc_playerM.life -= regenValue * Time.deltaTime;
            }
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

        if (Input.GetKeyDown(jumpKey) && sc_playerM.grounded)
        {
            sc_playerM.pos_playerVelocity.y += Mathf.Sqrt(sc_playerM.jumpForce * -3f * sc_playerM.gravity);
        }

    }
    public void Inputs()
    {
        if (Input.GetKey(keyUp))
        {
            vertical = 1;
        }
        else if (Input.GetKey(keyDown))
        {
            vertical = -1;
        }
        else if (!Input.GetKey(keyDown) && !Input.GetKey(keyUp))
        {
            vertical = 0;
        }
        if (Input.GetKey(keyRight) && !sc_playerM.isMovingBox)
        {
            horizontal = 1;
        }
        else if (Input.GetKey(keyLeft) && !sc_playerM.isMovingBox)
        {
            horizontal = -1;
        }
        else if (!Input.GetKey(keyLeft) && !Input.GetKey(keyRight))
        {
            horizontal = 0;
        }
        if (Input.GetKey(keyDown) && Input.GetKey(keyUp))
        {
            vertical = 0;
        }
        if (Input.GetKey(keyLeft) && Input.GetKey(keyRight))
        {
            horizontal = 0;
        }

        if (horizontal != 0 || vertical != 0)
        {
            sc_playerM.isMoving = true;
        }
        else
        {
            sc_playerM.isMoving = false;
        }
    }

    //public void ControllerMoveAndRotate()
    //{
    //    Debug.Log(sc_Movement.rb.centerOfMass);
    //    float angl_rot = gameObject.transform.eulerAngles.y % 360;
    //    if (Input.GetKey(keyRight) & Input.GetKey(keyUp))
    //    {
    //        angl_rot = 45;
    //    }
    //    else if (Input.GetKey(keyRight) & Input.GetKey(keyDown))
    //    {
    //        angl_rot = 135;
    //    }
    //    if (Input.GetKey(keyLeft) & Input.GetKey(keyUp))
    //    {
    //        angl_rot = 315;
    //    }
    //    else if (Input.GetKey(keyLeft) & Input.GetKey(keyDown))
    //    {
    //        angl_rot = 225;
    //    }
    //    sc_playerM.rotatioProves = new Vector3(horizontal, 0f, vertical);
    //    sc_Movement.PlayerRotation(sc_playerM.rotatioProves.normalized, 100, sc_playerM.pos_cam);
    //}

    //---------------------------------------------------DreamCatcher----------------------------------------------------------------------

    public void DreamCatcher()
    {
        if (sc_playerM.hasWeapon)
        {
            if (Input.GetKey(useWeaponKey) && sc_playerM.GroundCheck() && sc_playerM.isMovingBox == false && sc_playerM.isHiding == false)
            {
                sc_playerM.isUsingWeapon = true;  
            }
            else if (Input.GetKeyUp(useWeaponKey))
            {
                sc_playerM.isUsingWeapon = false;
                sc_playerM.cooldownForRegen = 0;
            }
            if (sc_playerM.isUsingWeapon)
            {
                sc_playerM.canMove = false;
                sc_playerM.isMoving = false;
                sc_playerM.uses -= .5f * Time.deltaTime;
                sc_playerM.lastUsedTime = sc_playerM.usedTime;
            }
            else
            {
                sc_playerM.canMove = true;
                sc_playerM.cooldownForRegen += Time.deltaTime;
                if (sc_playerM.cooldownForRegen >= 2f)
                {
                    sc_playerM.uses += .2f * Time.deltaTime;
                }
            }
            if (sc_playerM.uses <= 0)
            {
                sc_playerM.isUsingWeapon = false;
                sc_playerM.uses = 0;
            }
            if(sc_playerM.uses >= sc_playerM.useLimit)
            {
                sc_playerM.uses = sc_playerM.useLimit;
            }
        }
    }


    //------------------------------------------------------------------------------------------------------------------------------------
    public void MoveBoxController()
    {

        if (Input.GetKey(interactKey))
        {
            if(obj_Box.BoxLeftSide())
            {
                transform.localRotation = Quaternion.Euler(0, 90, 0);
                sc_playerM.isMovingBox = true;
            }
            else if(obj_Box.BoxRightSide())
            {
                transform.localRotation = Quaternion.Euler(0, -90, 0);
                sc_playerM.isMovingBox = true;
            }
            else if(obj_Box.BoxForwardSide())
            {
                transform.localRotation = Quaternion.Euler(0, -180, 0);
                sc_playerM.isMovingBox = true;
            }
            else if(obj_Box.BoxBackSide())
            {
                transform.localRotation = Quaternion.Euler(0, 0, 0);
                sc_playerM.isMovingBox = true;
            }
            /*Quaternion rotation = Quaternion.LookRotation(sc_playerM.pos_lookDirection);
            transform.localRotation = rotation;*/
        }
        else if(!Input.GetKey(interactKey) || obj_Box == null)
        {
            sc_playerM.isMovingBox = false;
        }

        if(sc_playerM.isMovingBox)
        {
            box.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            if (Input.GetKey(keyUp))
            {
                sc_playerM.speed = 5f;
                BoxPush();
            }
            else if (Input.GetKey(keyDown))
            {
                sc_playerM.speed = 1.9f;
                BoxPull();
            }
        }
        else
        {
            sc_playerM.speed = 5f;
            box.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            obj_Box.PushBox(0, 0);
            obj_Box.PullBox(0, 0);
        }
    }

    //-------------------------------------------------Empuje de cajas----------------------------------------------------------------------------
    public void BoxPush()
    {
        if(obj_Box.canPushz == true && obj_Box.canPushx == true)
        {
            obj_Box.PushBox(2.5f, 2.5f);
        }
        if(obj_Box.canPushz == true && obj_Box.canPushx == false)
        {
            obj_Box.PushBox(0, 2.5f);
        }
        if(obj_Box.canPushz == false && obj_Box.canPushx == true)
        {
            obj_Box.PushBox(2.5f, 0);
        }
        if(obj_Box.canPushz == false && obj_Box.canPushx == false)
        {
            obj_Box.PushBox(0, 0);
        }

    }
    public void BoxPull()
    {
        obj_Box.PullBox(3f, 3f);
    }


    public void DetectBox()
    {
        Physics.Raycast(transform.position, transform.forward, out sc_playerM.hit,sc_playerM.distanceToBox);

        if(sc_playerM.hit.collider != null)
        {
            if (sc_playerM.hit.collider.CompareTag("Box"))
            {
                obj_Box = sc_playerM.hit.collider.gameObject.GetComponent<BoxPush>();
                box = sc_playerM.hit.collider.gameObject;
            }
            else
            {
                obj_Box = null;
                box = null;
            }
        }
        else
        {
            obj_Box = null;
            box = null;
        }
    }
    //----------------------------------------------------------------------------------------------------------------------------------------

    //---------------------------------------------Mantita---------------------------------------------
    public void HideOnBlanket()
    {
        if (Input.GetKeyDown(interactKey) && sc_playerM.isCloseToBlanket && sc_playerM.isHiding == false)
        {
            sc_playerM.isInmune = true;
            sc_playerM.isHiding = true;
            sc_playerM.isMoving = false;
        }
        else if (Input.GetKeyDown(interactKey) && sc_playerM.isCloseToBlanket && sc_playerM.isHiding == true)
        {
            sc_playerM.hideTimer = true;  
        }
        if(sc_playerM.hideTimer == true)
        {
            sc_playerM.UnhideTimer += Time.deltaTime;
            if(sc_playerM.UnhideTimer >= 3f)
            {
                sc_playerM.isHiding = false;
                sc_playerM.isInmune = false;
                sc_playerM.hideTimer = false;
                sc_playerM.UnhideTimer = 0;
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

                sc_playerM.hasWeapon = true;
                //sc_playerM.useLimit = 20;
                other.gameObject.SetActive(false);
            
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
