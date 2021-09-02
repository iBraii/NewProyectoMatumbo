using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPointLight : MonoBehaviour
{
    
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            other.GetComponent<Mylight>().onLight = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            other.GetComponent<Mylight>().onLight = false;
        }
    }
}
