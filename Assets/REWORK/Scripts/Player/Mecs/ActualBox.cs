using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActualBox : MonoBehaviour
{
    public Rigidbody rb;
    public float speed;

    private float initialSpeed;
    void Start()
    {
        initialSpeed = speed;
        rb = GetComponent<Rigidbody>();
    }
   
    public void Motion(Vector3 direction)
    {
        rb.velocity = direction * speed;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Wall"))
        {
            rb.velocity = Vector3.zero;
        }
    }


}
