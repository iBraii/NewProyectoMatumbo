using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordMakerM : Jigsaw
{
    public GameObject thirdPersonCamera;
    public Camera mainCamera;
    public GameObject puzzleCameraPosition;
    public GameObject player;
    public float distanceToPlayer;
    public float minDistance;
    public Transform currentPosition;
    public LayerMask letritasMask;
    public int correctPiecesCounter;
    public int maxPieces;
    public bool playerClose;
    public bool solvingPuzzle;

    //-------Temporal---------
    //Hago referencia al cuerpo del jugador (cilindro) para desactivar el mesh renderer
    //y no estorbe al momento de resolver un puzzle.
    public GameObject playerBody;
    public GameObject playerEyes;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        mainCamera = FindObjectOfType<Camera>();
        thirdPersonCamera = GameObject.Find("ThirdPersonCamera");
        currentPosition = transform;

        playerBody = GameObject.Find("Player");
        playerEyes = GameObject.Find("Look");
    }

    // Update is called once per frame
    void Update()
    {
        player.GetComponent<PlayerM>().closeToPuzzle = playerClose;
        
    }
}
