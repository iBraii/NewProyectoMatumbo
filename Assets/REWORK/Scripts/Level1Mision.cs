using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Level1Mision : MonoBehaviour
{
    public Transform[] childPosition;
    private Vector3[] position;
    public int currentIndex;
    public int maxIndex;

    public bool playerClose;
    private GameObject player;

    public bool misionCompleted;
    public GameObject emisionSphere;
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
                misionCompleted = true;
                emisionSphere.SetActive(true);
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
