using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPush : MonoBehaviour
{
    Vector3 pos_dir;
    public Rigidbody rb;
    float forceToPush;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
       // rb.isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {
        //p_rb.AddForce(pos_dir * forceToPush, ForceMode.Force);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
           // rb.isKinematic = false;
            /*pos_dir = collision.contacts[0].point - transform.position;
            pos_dir = -pos_dir.normalized;*/
        }     
    }
}
