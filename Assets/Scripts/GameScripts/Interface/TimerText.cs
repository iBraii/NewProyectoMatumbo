using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TimerText : MonoBehaviour
{
    public Text myText;
    private TransitionTrigger sc_TT;
    // Start is called before the first frame update
    void Start()
    {
        myText = GetComponent<Text>();
        sc_TT = GameObject.Find("TransitionTrigger").GetComponent<TransitionTrigger>();
    }

    // Update is called once per frame
    void Update()
    {
        myText.text = ("" + sc_TT.maxTimer.ToString("00"));
    } 
}
