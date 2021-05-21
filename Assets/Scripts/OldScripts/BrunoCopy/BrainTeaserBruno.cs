using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class BrainTeaserBruno : JigsawBruno
{
    public GameObject thirdPersonCamera;
    public Camera mainCamera;
    public GameObject puzzleCameraPosition;
    public GameObject player;
    public float distanceToPlayer;
    public float minDistance;
    public Transform currentPosition;
    void Start()
    {
        player = GameObject.Find("Player");
        mainCamera = FindObjectOfType<Camera>();
        thirdPersonCamera = GameObject.Find("ThirdPersonCamera");
        currentPosition = transform;
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
            }else if (!isActive)
            {
                ActivePuzzle();
            }
        }
    }
    void ActivePuzzle()
    {
        if (PlayerClose() && Input.GetMouseButtonDown(1)&&!isActive)
        {
            isActive = true;
            thirdPersonCamera.SetActive(false);
            mainCamera.transform.position = puzzleCameraPosition.transform.position;
            mainCamera.transform.LookAt(transform);
            Cursor.lockState = CursorLockMode.None;
            player.GetComponent<PlayerM>().canMove = false;
        }
    }

    void DeactivatePuzzle()
    {
        if (isActive&& Input.GetMouseButtonDown(1))
        {
            isActive = false;
            thirdPersonCamera.SetActive(true);
            Cursor.lockState = CursorLockMode.Locked;
            player.GetComponent<PlayerM>().canMove = true;
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
