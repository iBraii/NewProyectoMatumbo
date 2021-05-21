using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordMakerBruno : JigsawBruno
{
    
    public GameObject thirdPersonCamera;
    public Camera mainCamera;
    public GameObject puzzleCameraPosition;
    public GameObject player;
    public float distanceToPlayer;
    public float minDistance;
    public Transform currentPosition;

    //-------Temporal---------
    //Hago referencia al cuerpo del jugador (cilindro) para desactivar el mesh renderer
    //y no estorbe al momento de resolver un puzzle.
    public GameObject playerBody;
    public GameObject playerEyes;
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
        PlayerInput();
        PlayerClose();
    }

    void PlayerInput()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (isActive)
            {
                DeactivatePuzzle();
            }
            else if (!isActive)
            {
                ActivePuzzle();
            }
        }
    }
    void ActivePuzzle()
    {
        if (PlayerClose() && Input.GetMouseButtonDown(1) && !isActive)
        {
            isActive = true;
            thirdPersonCamera.SetActive(false);
            mainCamera.transform.position = puzzleCameraPosition.transform.position;
            mainCamera.transform.LookAt(transform);
            Cursor.lockState = CursorLockMode.None;
            player.GetComponent<PlayerM>().canMove = false;

            playerBody.GetComponent<MeshRenderer>().enabled = false;
            playerEyes.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    void DeactivatePuzzle()
    {
        if (isActive && Input.GetMouseButtonDown(1))
        {
            isActive = false;
            thirdPersonCamera.SetActive(true);
            Cursor.lockState = CursorLockMode.Locked;
            player.GetComponent<PlayerM>().canMove = true;
            playerBody.GetComponent<MeshRenderer>().enabled = true;
            playerEyes.GetComponent<MeshRenderer>().enabled = true;
        }
    }

    bool PlayerClose()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distanceToPlayer < minDistance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
