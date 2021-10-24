using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using DG.Tweening;
using UnityEngine.InputSystem;

public class Level1Mision : MonoBehaviour
{
    public Transform[] childPosition;
    public Vector3[] position;
    public int currentIndex;
    public int maxIndex;

    public bool playerClose;
    private GameObject player;

    public bool misionCompleted;
    public GameObject emisionSphere;
    private AudioSource humming;
    void Start()
    {
        emisionSphere.SetActive(false);
        player = GameObject.Find("NewPlayer");
        position = new Vector3[childPosition.Length];
        for (int i = 0; i < childPosition.Length; i++)
        {
            position[i] = childPosition[i].position;
        }
        maxIndex = position.Length-1;
        humming = GetComponent<AudioSource>();
    }
    void Update()
    {
        transform.position = position[currentIndex];
        emisionSphere.transform.position = transform.position;
        PlayerInteraction();
    }
    private void PlayerInteraction()
    {
        if (playerClose == false) return;

        if (player.GetComponent<PlayerInput>().actions["Interact"].triggered)
        {
            if (currentIndex < maxIndex)
            {
                currentIndex++;               
                SoundManager.instance.Play("Confirmation");
            }
            else
            {
                SaveSystem.data.achievementCompleted[1] = true;
                SaveSystem.Save();
                AchievementPop.onMisionCompleted?.Invoke("YOU HAVE FOUND THE TEDDY BEAR");
                misionCompleted = true;
                emisionSphere.SetActive(true);
                humming.DOFade(0, 2);
                Invoke("DeactivateTeddy", 2);
                
            }
            
        }
    }
    public void DeactivateTeddy() => gameObject.SetActive(false);
    
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            playerClose = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            playerClose = false;
    }

}
