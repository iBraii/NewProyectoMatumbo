using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TimerText : MonoBehaviour
{
    public Text myText;
    private TransitionTrigger sc_TT;
    private string minutes, seconds, miliseconds;
    // Start is called before the first frame update
    void Start()
    {
        myText = GetComponent<Text>();
        sc_TT = GameObject.Find("TransitionTrigger").GetComponent<TransitionTrigger>();
    }

    // Update is called once per frame
    void Update()
    {
        minutes = ((int)sc_TT.maxTimer / 60).ToString("00");
        seconds = (sc_TT.maxTimer % 60).ToString("00");
        miliseconds = (sc_TT.maxTimer % 1 * 100).ToString("00");
        myText.text = ("Level Time: " + minutes + ":" + seconds + ":" + miliseconds);
    } 
}
