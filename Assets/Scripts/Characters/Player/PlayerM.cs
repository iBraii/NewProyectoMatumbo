using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerM : Characters
{
    public float cooldownForStressRegen;
    public float VFXTime = 4f;
    public CinemachineBasicMultiChannelPerlin screenShake;
    public PlayerC sc_playerC;
    //--------Movement Vars---------------
    public CharacterController cmp_controller;
    public float speed = 5;
    public float rotationSpeed = 1;
    public float turnTime = 1;
    public Transform pos_cam;
    public bool canMove = true;
    public Vector3 rotatioProves;
    //--------------------------------------
    public float boxSpeedH, boxSpeedV;

    //---------Jump Vars------------------
    public Vector3 pos_playerVelocity;
    public float gravity = -9.81f;
    public float jumpForce = 2f;
    public bool grounded;
    //public float jumpRaycastDistance;
    //public LayerMask whatIsGround;
    //-------------------------------------

    //---------Stress Vars----------------
    public float maxStress = 10f;
    public float stressCooldown;
    //------------------------------------
    public AudioSource steps;

    //---------DreamCatcher-----------
    public bool hasWeapon;
    public bool isUsingWeapon;
    public float uses;
    public float useLimit;
    public float usedTime;
    public float lastUsedTime;
    public float cooldownForRegen;
    public float knockBackForce;
    //---------------------------------


    //------------StoryHints----------
    public int storyHints;
    public int maxStoryHints;
    //--------------------------------


    //-------HideOnBlanketVars-----
    public bool isInmune;
    public float isInmuneTimer, isInmuneMaxTimer;
    public bool isCloseToBlanket;
    public bool isHiding, hideTimer;
    public float UnhideTimer;
    //-----------------------------


    //-------PushBoxVars-----
    public bool isCloseToBox;
    public float distanceToBox;
    public Vector3 pos_lookAt;
    public Vector3 pos_lookDirection;
    public RaycastHit hit;
    //public Vector3 pos_pushBoxDir;
    public bool isMovingBox;
    //-----------------------------

    //------Puzzles---------
    public bool solvingPuzzle;
    public bool closeToPuzzle;

    //-------LayerMask----------
    public LayerMask playerLook;
    //--------------------------
    public bool MovementLearned;

    private void Awake()
    {
        steps = GetComponent<AudioSource>();
        //boxPos = GameObject.Find("BoxPos");
        screenShake = GameObject.Find("ThirdPersonCamera").GetComponent<CinemachineBasicMultiChannelPerlin>();
        isHiding = false;
        maxStoryHints = 20;
        cmp_controller = GetComponent<CharacterController>();
        pos_playerVelocity = new Vector3(0f, 0f, 0f);
        sc_playerC = GetComponent<PlayerC>();
        //life = 0f;
        //MovementLearned = false;
    }
    private void Update()
    {
        
    }
}
