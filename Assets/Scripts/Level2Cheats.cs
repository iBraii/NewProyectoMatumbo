using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2Cheats : MonoBehaviour
{
    public GameObject obj_player;
    public Transform[] position;
    void Start()
    {
        obj_player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        TPP();   
    }
    void TPP()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            obj_player.GetComponent<CharacterController>().enabled = false;
            obj_player.transform.position = position[0].position;
            obj_player.GetComponent<CharacterController>().enabled = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            obj_player.GetComponent<CharacterController>().enabled = false;
            obj_player.transform.position = position[1].position;
            obj_player.GetComponent<CharacterController>().enabled = true;
        }
    }
}
