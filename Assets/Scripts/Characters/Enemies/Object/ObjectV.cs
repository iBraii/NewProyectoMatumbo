using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectV : MonoBehaviour
{
    private ObjectC sc_ObjectC;
    private ObjectM sc_ObjectM;
    public Animator animControl;
    void Start()
    {
        sc_ObjectC = GetComponent<ObjectC>();
        sc_ObjectM = GetComponent<ObjectM>();
        animControl = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        sc_ObjectC.PlayerDistanceCheck();
        sc_ObjectM.Denied();
        sc_ObjectC.Stunned();
        AnimatorController();
        
    }
    void AnimatorController()
    {
        if(sc_ObjectM.isIddle == false)
        {
            animControl.SetBool("isScreaming", true);
        }
        else
        {
            animControl.SetBool("isScreaming", false);
        }
        if(sc_ObjectM.isDenied)
        {
            animControl.SetBool("isDenied", true);
        }
        else
        {
            animControl.SetBool("isDenied", false);
        }
        

    }
}
