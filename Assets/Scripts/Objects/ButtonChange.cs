using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonChange : MonoBehaviour
{
    public GameObject mat1;
    public Color color1 , color2;
    // Start is called before the first frame update
    void Start()
    {
        mat1.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", color1 * 1.5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Box"))
        {
            mat1.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", color2 * 2.5f);
        }     
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Box"))
        {
            mat1.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", color1 * 1.5f);
        }
    }
}
