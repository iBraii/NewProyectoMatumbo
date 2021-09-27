using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TutorialManager : MonoBehaviour
{
    public GameObject player;
    public GameObject firstEnemy;

    public TextMeshPro[] indication;
    public GameObject atrapa;

    private void Start()
    {
        atrapa = GameObject.Find("DREAMCATCHER");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Invoke("ResetCamera", 15);
            PlayerSingleton.Instance.canMove = false;
            GetComponent<BoxCollider>().enabled = false;
            Camera.main.GetComponent<Animator>().SetBool("enemy", true);
            firstEnemy.SetActive(true);
            atrapa.SetActive(false);
        }
    }

    private void ResetCamera()
    {
        player.GetComponent<Dreams>().enabled = true;
        PlayerSingleton.Instance.canMove = true;
        Camera.main.GetComponent<Animator>().SetBool("enemy", false);
    }
}
