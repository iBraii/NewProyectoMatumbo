using UnityEngine;
using System;
using DG.Tweening;

public class Button : MonoBehaviour
{
    [SerializeField] private GameObject[] darkAura;
    [SerializeField] private GameObject[] darkZone;
    public bool buttonToggle;
    public static event Action onButtonChanged;
    
    private void ButtonState(bool state) 
    {
        foreach (GameObject go in darkAura)
        {
            go.GetComponent<BoxCollider>().enabled=state;
            go.GetComponent<MeshRenderer>().enabled=state;
            
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Box") || other.CompareTag("Player"))
        {
            ButtonState(false);
            onButtonChanged?.Invoke();
            if (buttonToggle == false)
                SoundManager.instance.Play("PlacaOn");
            buttonToggle = true;

            foreach (GameObject go in darkZone)
            {
                go.GetComponent<Renderer>().material.DOFade(0, 2);
            }
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
            if (buttonToggle == true)
                SoundManager.instance.Play("PlacaOff");
            buttonToggle = false;
            onButtonChanged?.Invoke();

            foreach (GameObject go in darkZone)
            {
                go.GetComponent<Renderer>().material.DOFade(.74f, 2);
            }
        }
    }
}
