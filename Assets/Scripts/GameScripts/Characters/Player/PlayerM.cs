using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerM : Characters
{
    public CinemachineBasicMultiChannelPerlin screenShake;
    public PlayerC sc_playerC;
    //--------Movement Vars---------------
    public CharacterController cmp_controller;
    public float speed = 5;
    public float rotationSpeed = 1;
    public float turnTime = 1;
    public Transform pos_cam;
    public bool canMove = true;
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


    //---------DreamCatcher-----------
    public bool hasWeapon;
    public bool isUsingWeapon;
    public float useLimit;
    //---------------------------------


    //------------StoryHints----------
    public int storyHints;
    public int maxStoryHints;
    //--------------------------------



    //---------Enemies-----------------
    public GameObject obj_enemyTest;

    //-------HideOnBlanketVars-----
    public bool isInmune;
    public float isInmuneTimer, isInmuneMaxTimer;
    public bool isCloseToBlanket;
    public bool isHiding;
    //-----------------------------


    //-------PushBoxVars-----
    public bool isCloseToBox;
    public float distanceToBox;
    public Transform pos_lookAt;
    public Vector3 pos_lookDirection;
    //public Vector3 pos_pushBoxDir;
    public ObjectPush sc_box;
    public bool isMovingBox;
    //-----------------------------

    //------Puzzles---------
    public bool solvingPuzzle;
    public bool closeToPuzzle;

    //-------LayerMask----------
    public LayerMask playerLook;
    //--------------------------


    private void Awake()
    {
        //boxPos = GameObject.Find("BoxPos");
        screenShake = GameObject.Find("ThirdPersonCamera").GetComponent<CinemachineBasicMultiChannelPerlin>();
        isHiding = false;
        obj_enemyTest = GameObject.Find("EnemyTest");
        maxStoryHints = 20;
        cmp_controller = GetComponent<CharacterController>();
        pos_playerVelocity = new Vector3(0f, 0f, 0f);
        sc_playerC = GetComponent<PlayerC>();
        if (sc_playerC.box != null)
        {
            pos_lookAt = sc_playerC.box.transform;
        }
        //life = 0f;
    }
    private void Update()
    {
        if (sc_playerC.box != null)
        {
            pos_lookAt = sc_playerC.box.transform;
        }
    }
}
