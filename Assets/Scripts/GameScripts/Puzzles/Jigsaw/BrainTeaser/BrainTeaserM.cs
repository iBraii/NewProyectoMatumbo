using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrainTeaserM : Jigsaw
{
    public GameObject thirdPersonCamera;
    public Camera mainCamera;
    public GameObject puzzleCameraPosition;
    public GameObject player;
    public float distanceToPlayer;
    public float minDistance;
    public Transform currentPosition;

    public int correctPieces;
    public GameObject selectedPiece;
    public int layer = 1;
    public RaycastHit rayHit;

    public GameObject obj_aura;
    public float damage=0.01f;
    public bool playerOnAura;
    public bool isMakingPuzzle;
    public bool playerClose;

    public GameObject obj_canvas;
    // Start is called before the first frame update
    void Start()
    {
        isMakingPuzzle = false;
        obj_canvas = GameObject.Find("Canvas");
        player = GameObject.Find("Player");
        mainCamera = FindObjectOfType<Camera>();
        thirdPersonCamera = GameObject.Find("ThirdPersonCamera");
        currentPosition = transform;
    }

    // Update is called once per frame
    void Update()
    {
       
        player.GetComponent<PlayerM>().closeToPuzzle = playerClose;
    }
}
