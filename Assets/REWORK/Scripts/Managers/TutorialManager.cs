using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
public class TutorialManager : MonoBehaviour
{
    public GameObject player;
    public GameObject firstEnemy;

    public TextMeshPro[] indication;

    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction cameraAction;

    private bool movement, look, jump;
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"];
        cameraAction = playerInput.actions["Look"];
        jumpAction = playerInput.actions["Jump"];
    }

    void Update()
    {
        GetPlayerInput();
    }

    private void GetPlayerInput()
    {
        if (moveAction.triggered)
            ChangeAlphaValue(indication[0], 20);
        if (cameraAction.triggered)
            ChangeAlphaValue(indication[1], .4f);
        if (jumpAction.triggered)
            ChangeAlphaValue(indication[2], 100);
    }

    private void ChangeAlphaValue(TextMeshPro text,float intensity)
    {
        text.color = new Color(255, 255, 255, text.color.a - (intensity*Time.deltaTime));
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Invoke("ResetCamera", 12);
            player.GetComponent<Dreams>().enabled = true;
            PlayerSingleton.Instance.canMove = false;
            GetComponent<BoxCollider>().enabled = false;
            Camera.main.GetComponent<Animator>().SetBool("enemy", true);
            firstEnemy.SetActive(true);
        }
    }

    private void ResetCamera()
    {
        PlayerSingleton.Instance.canMove = true;
        Camera.main.GetComponent<Animator>().SetBool("enemy", false);
    }
}
