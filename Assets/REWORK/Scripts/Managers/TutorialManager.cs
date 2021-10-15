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


    public Material desvanecer;
    public GameObject actualDreamCatcher;

    public GameObject particles;
    private bool canPlaySound=true;
    private void Start()
    {
        
    }
    private void Update()
    {
        
    }
    private void GrabDreamCatcher()
    {
            Invoke("CameraAnim", 1.5f);
            actualDreamCatcher.GetComponent<Renderer>().material = desvanecer;
            if(canPlaySound)
                SoundManager.instance.Play("Confirmation");
            canPlaySound = false;          
    }
    private void CameraAnim()
    {
        Invoke("ResetCamera", 13);
        PlayerSingleton.Instance.canMove = false;
        GetComponent<BoxCollider>().enabled = false;
        Camera.main.GetComponent<Animator>().SetBool("enemy", true);
        firstEnemy.SetActive(true);
        atrapa.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GrabDreamCatcher();
        }
    }

    private void ResetCamera()
    {
        player.GetComponent<Dreams>().enabled = true;
        PlayerSingleton.Instance.canMove = true;
        Camera.main.GetComponent<Animator>().SetBool("enemy", false);
        particles.GetComponent<SphereCollider>().enabled = true;
        gameObject.SetActive(false);
    }
}
