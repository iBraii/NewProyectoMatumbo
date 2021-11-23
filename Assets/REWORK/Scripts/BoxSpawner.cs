using UnityEngine;

public class BoxSpawner : MonoBehaviour
{
    private Vector3 initialPos;
    private Vector3 initialRot;

    private void Start()
    {
        initialPos = transform.position;
        initialRot = transform.localEulerAngles;
    } 

    private void ResetToInitalPos()
    {
        transform.position = initialPos;
        transform.localEulerAngles = initialRot;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("DeathBox"))
            ResetToInitalPos();  
    }
}