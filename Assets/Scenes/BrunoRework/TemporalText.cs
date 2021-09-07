using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TemporalText : MonoBehaviour
{
    private Text thisText;
    private Text otherText;
    private PlayerMovement p;
    void Start()
    {
        thisText=GetComponent<Text>();
        p = GameObject.Find("NewPlayer").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        //thisText.text =""+ p.playerVelocity;
        //otherText.text = "" + p.moveDirection;
    }
}
