using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CulpaTrigger : MonoBehaviour
{
    public GameObject culpa;
    public GameObject player;
    public bool firstIteration;
    public float distanceToPlayer;
    public bool playerClose;
    public float slowSpeed;
    public float highSpeed;
    void Start()
    {
        
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        GetDistanceToPlayer();
    }
    public void GetDistanceToPlayer()
    {
        distanceToPlayer = Vector3.Distance(player.transform.position, culpa.transform.position);

        if (distanceToPlayer <= 25)
        {
            playerClose = true;
        }else
        {
            playerClose = false;
        }
    }
    public void DynamicChange()
    {
        if (playerClose)
        {
            culpa.GetComponent<NavMeshAgent>().speed = slowSpeed;
            Debug.Log("Reduciendo Velocidad");
        }
        else
        {
            culpa.GetComponent<NavMeshAgent>().speed = highSpeed;
            Debug.Log("Aumentando Velocidad");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {           
            culpa.SetActive(true);
            if (firstIteration == false)
            {
                DynamicChange();
            }
            
        }
        
    }
}
