using UnityEngine;
using System;
using DG.Tweening;

public class Button : MonoBehaviour
{
    [SerializeField] private GameObject[] darkAura;
    [SerializeField] private GameObject[] darkZone;
    public bool buttonToggle;
    
    private void ButtonState(bool state) 
    {
        foreach (GameObject go in darkAura)
            go.SetActive(state);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Box") || other.CompareTag("Player"))
        {
            ButtonState(false);
            GetComponentInChildren<ButtonChange>().ButtonColor();
            if (buttonToggle == false)
                SoundManager.instance.Play("PlacaOn");
            buttonToggle = true;

            foreach (GameObject go in darkZone)
            {
                go.GetComponent<Renderer>().material.DOFade(0, 2);
                go.GetComponent<BoxCollider>().enabled = false;
                
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Box") || other.CompareTag("Player"))
        {
            ButtonState(false);
            buttonToggle = true;
            GetComponentInChildren<ButtonChange>().ButtonColor();
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
            GetComponentInChildren<ButtonChange>().ButtonColor();

            foreach (GameObject go in darkZone)
            {
                go.GetComponent<Renderer>().material.DOFade(.74f, 2);
                go.GetComponent<BoxCollider>().enabled = true;
                
            }
        }
    }
}
