using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TutorialManager : MonoBehaviour
{
    public GameObject player;
    public GameObject firstEnemy;

    public TextMeshPro[] indication;

    private void Start()
    {
        GameManager.Instance.UpdateGameState(GameState.OnLvls);
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
        }
    }

    private void ResetCamera()
    {
        player.GetComponent<Dreams>().enabled = true;
        PlayerSingleton.Instance.canMove = true;
        Camera.main.GetComponent<Animator>().SetBool("enemy", false);
    }
}
