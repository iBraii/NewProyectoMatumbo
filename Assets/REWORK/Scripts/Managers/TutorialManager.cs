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
    public GameObject grabText;

    public TextMeshPro[] indication;
    public GameObject atrapa;

    private bool playerClose;
    private PlayerInput playerInput;
    private InputAction grabAction;

    public Material desvanecer;
    public GameObject actualDreamCatcher;

    public GameObject particles;
    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        grabAction = playerInput.actions["Interact"];
        atrapa = GameObject.Find("DREAMCATCHER");
        grabText.SetActive(false);
    }
    private void Update()
    {
        GrabDreamCatcher();
    }
    private void GrabDreamCatcher()
    {
        if (playerClose&&grabAction.triggered)
        {
            Invoke("CameraAnim", 1.5f);
            actualDreamCatcher.GetComponent<Renderer>().material = desvanecer;
            SoundManager.instance.Play("Confirmation");
        }    
    }
    private void CameraAnim()
    {
        Invoke("ResetCamera", 15);
        PlayerSingleton.Instance.canMove = false;
        GetComponent<BoxCollider>().enabled = false;
        Camera.main.GetComponent<Animator>().SetBool("enemy", true);
        firstEnemy.SetActive(true);
        atrapa.SetActive(false);
        grabText.GetComponent<TextMeshProUGUI>().text = "";
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerClose = true;
            grabText.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerClose = false;
            grabText.SetActive(false);
        }
    }

    private void ResetCamera()
    {
        player.GetComponent<Dreams>().enabled = true;
        PlayerSingleton.Instance.canMove = true;
        Camera.main.GetComponent<Animator>().SetBool("enemy", false);
        particles.GetComponent<SphereCollider>().enabled = true;
    }
}
