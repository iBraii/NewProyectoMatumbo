using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActualBox : MonoBehaviour
{
    public Rigidbody rb;
    public float speed;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
   
    public void Motion(Vector3 direction)
    {
        rb.velocity = direction * speed;
    }
  


}
