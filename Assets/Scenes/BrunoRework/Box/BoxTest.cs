using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxTest : MonoBehaviour
{
    public GameObject semiPlayer;
    [Range(0, 100)]
    public float speed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(semiPlayer.transform.position, Vector3.up,speed * Time.deltaTime);
    }
}
