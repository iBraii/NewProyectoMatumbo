using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class TutorialManager : MonoBehaviour
{
    public GameObject player;
    public GameObject firstEnemy;
    void Start()
    {
        
    }

    
    void Update()
    {
        
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
