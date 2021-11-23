using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTrigger : MonoBehaviour
{
    public string soundID;
    [Tooltip("Se requiere que el jugador interactue con el objeto para reproducir un sonido?")]
    public bool interactionRequiered;

    private bool playerOnRange;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerOnRange)
        {
            SoundManager.instance.Play(soundID);
            Deactivate();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (interactionRequiered)
                playerOnRange = true;
            else
            {
                SoundManager.instance.Play(soundID);
                Deactivate();
            }
                
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (interactionRequiered)
            playerOnRange = false;
       
    }
    private void Deactivate() => gameObject.SetActive(false);

}
