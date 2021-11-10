using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointIdentifier : MonoBehaviour
{
    public int index;
    public MainMissionLerp parent;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (parent.nextIndex < index)
            {
                parent.nextIndex = index;
                parent.SetValues();
            }           
            gameObject.SetActive(false);
        }
    }
}
