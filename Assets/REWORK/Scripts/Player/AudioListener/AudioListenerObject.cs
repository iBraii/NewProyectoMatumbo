using UnityEngine;

public class AudioListenerObject : MonoBehaviour
{
    [SerializeField] private Transform player;
    void Update()
    { 
        if(player == null)
        {
            Debug.LogWarning("AudioListener/ no se encuentra al player");
            return;
        }
        Vector3 cameraRotation = Camera.main.transform.localEulerAngles;
        cameraRotation.x = 0;
        cameraRotation.z = 0;
        transform.localEulerAngles = cameraRotation;
        transform.position = player.transform.position;
    }
}
