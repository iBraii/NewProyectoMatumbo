using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public GameObject[] darkZone;
    


    private void ButtonState(bool state)
    {
        foreach (GameObject go in darkZone)
        {
            go.SetActive(state);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Box") || other.CompareTag("Player"))
        {
            ButtonState(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Box") || other.CompareTag("Player"))
        {
            ButtonState(true);
        }
    }
}
