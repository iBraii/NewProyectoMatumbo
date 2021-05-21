using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTesting : MonoBehaviour
{
    private Rigidbody rb;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //InvokeRepeating("Moveeee", 3, 1);
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Moveeee();
    }
    void Moveeee()
    {
        if(player.GetComponent<PlayerM>().isUsingWeapon == false)
        {
            rb.velocity = new Vector3(0, 0, -0.7f);
        }     
    }
    public void Paralize()
    {
        rb.velocity = new Vector3(0, 0,0);
    }
}
