using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CulpaTrigger : MonoBehaviour
{
    public GameObject culpa;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            culpa.SetActive(true);
        }
    }
}
