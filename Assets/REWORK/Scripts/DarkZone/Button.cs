using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Button : MonoBehaviour
{
    public GameObject[] darkZone;
    public bool buttonToggle;
    public static event Action onButtonChanged;
    
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
            SoundManager.instance.Play("PlacaOn");
            buttonToggle = true;
            onButtonChanged?.Invoke();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Box") || other.CompareTag("Player"))
        {
            ButtonState(false);
            buttonToggle = true;
            onButtonChanged?.Invoke();
        }   
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Box") || other.CompareTag("Player"))
        {
            ButtonState(true);
            SoundManager.instance.Play("PlacaOff");
            buttonToggle = false;
            onButtonChanged?.Invoke();
        }
    }
}
