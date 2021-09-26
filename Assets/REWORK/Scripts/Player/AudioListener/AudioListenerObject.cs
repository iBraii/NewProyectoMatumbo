using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioListenerObject : MonoBehaviour
{
    public Transform player;
    void Update()
    { 
        if(player == null)
        {
            Debug.LogWarning("no se encuentra al player");
            return;
        }
        Vector3 cameraRotation = Camera.main.transform.localEulerAngles;
        cameraRotation.x = 0;
        cameraRotation.z = 0;
        transform.localEulerAngles = cameraRotation;
        transform.position = player.transform.position;
    }
}
