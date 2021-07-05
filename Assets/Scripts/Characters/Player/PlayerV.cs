using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;
[RequireComponent(typeof(Movement))]
public class PlayerV : MonoBehaviour
{
    public VisualEffect DreamCatcherVFX, AreaEffectVFX;
    private PlayerC sc_playerC;
    private PlayerM sc_playerM;
    private Movement sc_movement;
    public Animator animCntr;
    public GameObject boxVol;
    // Start is called before the first frame update
    void Awake()
    {
        sc_playerC = GetComponent<PlayerC>();
        sc_playerM = GetComponent<PlayerM>();
        sc_movement = GetComponent<Movement>();
        animCntr = GetComponent<Animator>();
    }
    private void Start()
    {
        SoundManager.instance.Play("LowRumble");
        SoundManager.instance.Stop("MenuTheme");
    }
    //private void FixedUpdate()
    //{
    //    if (sc_playerC.obj_Box != null)
    //    {

    //        sc_playerC.BoxPush();

    //    }
    //}


    // Update is called once per frame
    void Update()
    {
        ActivateVolume();
        sc_playerC.HideOnBlanket();
        sc_playerC.RegenStress(.1f,3);
        sc_playerC.DreamCatcher();
        sc_playerC.StoryHintsControl();
        sc_playerM.LifeController(10);
        sc_playerC.DetectBox();
        AnimationController();
        ParticlesController();
        if (sc_playerM.MovementLearned)
        {
            if (sc_playerM.canMove && sc_playerM.isUsingWeapon == false && sc_playerM.isHiding == false)
            {
                sc_movement.PlayerMovement(sc_playerM.speed, sc_playerM.rotationSpeed, sc_playerM.turnTime, sc_playerM.pos_cam);
                sc_playerC.Inputs();
                //sc_playerC.ControllerMoveAndRotate();
            }
            if (sc_playerM.isHiding == false && sc_playerM.isUsingWeapon == false)
            {
                sc_playerC.PlayerJump();
            }
        }


        if (sc_playerC.obj_Box != null || sc_playerC.box != null)
        {
            sc_playerC.MoveBoxController();
        }
        else
        {
            sc_playerM.isMovingBox = false;
        }      
    }


    void AnimationController()
    {
        animCntr.SetBool("isMoving", sc_playerM.isMoving);
        animCntr.SetFloat("life", sc_playerM.life);
        animCntr.SetBool("boxState", sc_playerM.isMovingBox);
        animCntr.SetBool("pushingBox", sc_playerM.isMovingBox && Input.GetKey(sc_playerC.keyUp));
        animCntr.SetBool("pullingBox", sc_playerM.isMovingBox && Input.GetKey(sc_playerC.keyDown));
        if(sc_playerM.isHiding == true && sc_playerM.hideTimer == false)
        {
            animCntr.SetBool("isHiding", true);
        }
        else if(sc_playerM.hideTimer == true && sc_playerM.isHiding == true)
        {
            animCntr.SetBool("isHiding", false);
        }       
        animCntr.SetFloat("playerYVelocity",sc_playerM.pos_playerVelocity.y);
        animCntr.SetBool("onGround", sc_playerM.GroundCheck());
    }

    void ParticlesController()
    {
        if(sc_playerM.isUsingWeapon == true)
        {
            sc_playerM.VFXTime += Time.deltaTime;
            DreamCatcherVFX.Play();
            AreaEffectVFX.Play();
            AreaEffectVFX.SetFloat("InputFloat", sc_playerM.VFXTime);
        }
        else
        {
            DreamCatcherVFX.Stop();
            sc_playerM.VFXTime = 4f;
            AreaEffectVFX.Stop();
        }
    }
    //--------------------Triggers-------------------------------------
    private void OnTriggerEnter(Collider other)
    {

        sc_playerC.PickDC(other);
        sc_playerC.IsOnBlanket(other);
        sc_playerC.PickSH(other);
        sc_playerC.PickSweets(other);

    }
    private void OnTriggerStay(Collider other)
    {
        sc_playerC.IsOnBlanket(other);
        sc_playerC.PickSH(other);
    }

    private void OnTriggerExit(Collider other)
    {
        sc_playerC.IsOffBlanket(other);
    }
    void ActivateVolume()
    {
        if(Input.GetMouseButtonDown(1))
        {
            if(boxVol.activeInHierarchy == true)
            {
                boxVol.SetActive(false);
            }
            else
            {
                boxVol.SetActive(true);
            }      
        }
    }
}
