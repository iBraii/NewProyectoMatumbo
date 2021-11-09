using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class MainMissionLerp : MonoBehaviour
{
    public Transform[] childPos;
    private Vector3[] position;
    private bool active;
    public bool onPath;
    private int index = 0;

    public float journeyLenght;
    private float t;
    public float speed;   
    void Start()
    {
        position = new Vector3[childPos.Length];
        for(int i = 0; i < childPos.Length; i++)
            position[i] = childPos[i].position;
    }

    void Update()
    {
        LerpToPosition();
    }
    private void LerpToPosition()
    {
        if (active == false) return;

        t += Time.deltaTime * speed;
        transform.position = Vector3.Lerp(position[index], position[index + 1], t / journeyLenght);
        if ((t / journeyLenght) >= 1)
        {
            active = false;
            t = 0;
            onPath = false;
            if (index != childPos.Length - 1)
                index++;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")&&onPath==false)
        {
            if (!active&&index!=childPos.Length-1)
            {
                active = true;
                onPath = true;
            }
            else GetComponent<VisualEffect>().Stop();
        }
    }
    [ContextMenu("Add Position")]
    private void AddPosition()
    {
        GameObject go = new GameObject("Pos" + position.Length + 1);
        go.transform.position = childPos[childPos.Length - 1].position;
        go.transform.parent = transform;

        Transform[] temporal = new Transform[childPos.Length + 1];
        for(int i = 0; i < childPos.Length; i++)
            temporal[i] = childPos[i];
        temporal[childPos.Length] = go.transform;
        childPos = temporal;
    }
    private void OnDrawGizmos()
    {        
        foreach(Transform t in childPos)
            Gizmos.DrawWireSphere(t.position, .2f);
        for(int i = 0; i < childPos.Length - 1; i++)
            Debug.DrawLine(childPos[i].position, childPos[i + 1].position);
    }
}
