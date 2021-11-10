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
    public int currentIndex = 0;
    public int nextIndex = 0;

    public float journeyLenght;
    private float t;
    public float speed;

    public GameObject objectHolder;
    
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
        transform.position = Vector3.Lerp(position[currentIndex], position[nextIndex], t / journeyLenght);
        if ((t / journeyLenght) >= 1)
        {
            active = false;          
            onPath = false;
            currentIndex++;
        }
    }
    public void SetValues()
    {
        if (currentIndex < nextIndex - 1)
        {
            currentIndex = nextIndex - 1;
            transform.position = position[nextIndex - 1];
        }
        if (!active && currentIndex != childPos.Length - 1)
        {
            t = 0;
            active = true;
            onPath = true;
        }
        else GetComponent<VisualEffect>().Stop();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")&&onPath==false)
        {
            if (!active&&currentIndex!=childPos.Length-1)
            {
                t = 0;
                active = true;
                onPath = true;
            }
            else GetComponent<VisualEffect>().Stop();
        }
    }
    [ContextMenu("Add Position")]
    private void AddPosition()
    {
        GameObject go = new GameObject("Pos" + (childPos.Length + 1));
        go.transform.position = childPos[childPos.Length - 1].position;
        go.transform.parent = objectHolder.transform;
        SphereCollider collider= go.AddComponent<SphereCollider>();
        WaypointIdentifier identifier = go.AddComponent<WaypointIdentifier>();
        identifier.index = childPos.Length + 1;
        identifier.parent = this;
        collider.radius = .8f;
        collider.isTrigger = true;
        
        Transform[] temporal = new Transform[childPos.Length + 1];

        for(int i = 0; i < childPos.Length; i++)
            temporal[i] = childPos[i];
        temporal[childPos.Length] = go.transform;
        childPos = temporal;
    }
    private void OnDrawGizmos()
    {
        if (position == null)
        {
            foreach (Transform t in childPos)
                Gizmos.DrawWireSphere(t.position, .2f);
            for (int i = 0; i < childPos.Length - 1; i++)
                Debug.DrawLine(childPos[i].position, childPos[i + 1].position);
        }
        else
        {
            foreach (Vector3 r in position)
                Gizmos.DrawWireSphere(r, .2f);
            for (int i = 0; i < childPos.Length - 1; i++)
                Debug.DrawLine(childPos[i].position, childPos[i + 1].position);
        }
        
    }
}
