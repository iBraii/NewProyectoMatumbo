using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HightCheck : MonoBehaviour
{
    public GameObject player;
    public float start;
    public float highest=0;
    public float result;
    void Start()
    {
        player = GameObject.Find("NewPlayer");
        start = player.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.y > highest)
            highest = player.transform.position.y;

        result = highest - start;
    }
}
