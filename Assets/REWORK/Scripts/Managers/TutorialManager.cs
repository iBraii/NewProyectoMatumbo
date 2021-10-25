using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.InputSystem;
using TMPro;
public class TutorialManager : MonoBehaviour
{
    [Header("Player reference")]
    [SerializeField] private GameObject player;

    [Header("Enemy Encounter")]
    [SerializeField] private GameObject firstEnemy;

    [Space] [SerializeField] private TextMeshPro[] indication;

    [Header("Atrapasueños objeto padre")]
    [SerializeField] private GameObject atrapa;

    [Header("Shader")]
    [SerializeField] private Material desvanecer;

    [Header("Atrapasueños mesh")]
    [SerializeField] private GameObject actualDreamCatcher;

    [Header("Particula guia")]
    [SerializeField] private GameObject particles;

    [Header("Cuadro que se cae")]
    [SerializeField] private GameObject cuadro;

    private bool canPlaySound=true;
    void OnDreamCatcherGrabbed()
    {
       
        if (cuadro == null) return;
        cuadro.GetComponent<Rigidbody>().useGravity = true;
        cuadro.GetComponent<Rigidbody>().centerOfMass = new Vector3(-.2f, 0, 0);
        AudioSource[] sources = cuadro.GetComponents<AudioSource>();

        for(int i = 0; i < sources.Length; i++) sources[i].Play();
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
        if (other.CompareTag("Player")) GrabDreamCatcher();
    }

    private void ResetCamera()
    {
        OnDreamCatcherGrabbed();
        player.GetComponent<Dreams>().enabled = true;
        PlayerSingleton.Instance.canMove = true;
        Camera.main.GetComponent<Animator>().SetBool("enemy", false);
        particles.GetComponent<SphereCollider>().enabled = true;
        gameObject.SetActive(false);
    }
}
