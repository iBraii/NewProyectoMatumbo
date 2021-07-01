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
    public bool closeToPlayer;
    public Rigidbody rb;
    public NavMeshAgent pAgent;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        obj_player = GameObject.Find("Player");
        
    }
    public void Denied()
    {
        if (obj_player.GetComponent<PlayerM>().isUsingWeapon && closeToPlayer == true)
        {
            obj_player.GetComponent<PlayerM>().usedTime += Time.deltaTime;
            isDenied = true;
        }
    }
    public void DetectPlayer()
    {
        if (Vector3.Distance(transform.position, obj_player.transform.position) > 7.5f)
        {
            closeToPlayer = false;
        }
        else if (Vector3.Distance(transform.position, obj_player.transform.position) < 7.5f)
        {
            closeToPlayer = true;
        }
    }
    public void DamageTaken()
    {

    }
}
