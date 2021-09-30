using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.InputSystem;
using TMPro;
public class TutorialManager : MonoBehaviour
{
    public GameObject player;
    public GameObject firstEnemy;

    public TextMeshPro[] indication;
    public GameObject atrapa;

    private bool playerClose;
    private PlayerInput playerInput;
    private InputAction grabAction;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        grabAction = playerInput.actions["Interact"];
        atrapa = GameObject.Find("DREAMCATCHER");
        
    }
    private void Update()
    {
        GrabDreamCatcher();
    }
    private void GrabDreamCatcher()
    {
        if (playerClose&&grabAction.triggered)
        {
            Invoke("ResetCamera", 15);
            PlayerSingleton.Instance.canMove = false;
            GetComponent<BoxCollider>().enabled = false;
            Camera.main.GetComponent<Animator>().SetBool("enemy", true);
            firstEnemy.SetActive(true);
            atrapa.SetActive(false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerClose = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerClose = false;
        }
    }

    private void ResetCamera()
    {
        player.GetComponent<Dreams>().enabled = true;
        PlayerSingleton.Instance.canMove = true;
        Camera.main.GetComponent<Animator>().SetBool("enemy", false);
    }
}
