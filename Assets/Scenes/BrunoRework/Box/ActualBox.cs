using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActualBox : MonoBehaviour
{
    public Rigidbody rb;
    public float speed;
    public float initialDistanceToPlayer;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        BoxSpeedController();
    }
    public void Motion(Vector3 direction)
    {
        rb.velocity = direction * speed;
    }
    private void BoxSpeedController()
    {
        if (Vector3.Distance(transform.position, GameObject.Find("NewPlayer").transform.position) > initialDistanceToPlayer)
        {
            speed = 1.5f;
        }
        else
        {
            speed = 1.3f;
        }
    }
    public void GetInitialDistanceToPlayer(Transform player)
    {
        initialDistanceToPlayer = Vector3.Distance(transform.position, player.position);
    }

    

   

    
}
