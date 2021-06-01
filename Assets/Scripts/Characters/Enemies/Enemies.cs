using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemies : MonoBehaviour
{
    public bool isIddle;
    public Vector3 pos_Player;
    public GameObject obj_player;
    public bool isDenied;
    public Rigidbody rb;
    public NavMeshAgent pAgent;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        obj_player = GameObject.Find("Player");
        
    }
    private void PlayerIsNearby()
    {

    }
    public void Denied()
    {
        if (obj_player.GetComponent<PlayerM>().isUsingWeapon)
        {
            //pAgent.isStopped = true;
            isDenied = true;
            //Debug.Log("a");
        }
        /*else
        {
            //pAgent.isStopped = false;
            //isDenied = false;
        }*/
    }

    public void DamageTaken()
    {

    }
}
