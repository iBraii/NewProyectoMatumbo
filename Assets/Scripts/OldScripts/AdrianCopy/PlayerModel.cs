using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel : Characters
{
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
    public Vector3 pos_pushBoxDir;
    public GameObject obj_box;
    //-----------------------------


    //-------LayerMask----------
    public LayerMask playerLook;
    //--------------------------


    //-------MoveVars---------
    public bool walkActive;
    //------------------------

    private void Awake()
    {
        isHiding = false;
        obj_enemyTest = GameObject.Find("EnemyTest");
        maxStoryHints = 20;
    }
    private void Update()
    {

    }
}
