using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectC : MonoBehaviour
{
    private ObjectM sc_ObjectM;
    void Start()
    {
        sc_ObjectM = GetComponent<ObjectM>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    public void PlayerDistanceCheck()
    {
        if(sc_ObjectM.distanceToPlayer <= sc_ObjectM.agroRange && !sc_ObjectM.isDenied)
        {
            sc_ObjectM.isIddle = false;
        }
        if(sc_ObjectM.isIddle == false && sc_ObjectM.distanceToPlayer >= sc_ObjectM.screamRange && !sc_ObjectM.isDenied)
        {
            sc_ObjectM.isIddle = true;
        }





        if(sc_ObjectM.isIddle == false)
        {
            Activate();
            sc_ObjectM.timeToIddle = 0;
            //Debug.Log("te grito");
        }
        else
        {
            Deactivate();
            //Debug.Log("no te grito");
        }
    
    }

    public void Activate()
    {
        sc_ObjectM.blink = true;
        AttackingVisual();
        sc_ObjectM.form[0].SetActive(false);
        sc_ObjectM.form[1].SetActive(true);
        sc_ObjectM.timeToIddle = 0;   
    }
    
    public void Deactivate()
    {

        sc_ObjectM.timeToIddle += Time.deltaTime;
        if (sc_ObjectM.timeToIddle >= sc_ObjectM.maxTimetoIddle)
        {

            sc_ObjectM.blink = false;
            sc_ObjectM.form[0].SetActive(true);
            sc_ObjectM.form[1].SetActive(false);

            sc_ObjectM.isIddle = true;
            sc_ObjectM.timeToIddle = 0;
        }
        AttackingVisual();
    }
    void AttackingVisual()
    {
        if (sc_ObjectM.blink)
        {
            sc_ObjectM.blinkTime += Time.deltaTime;
            if (sc_ObjectM.blinkTime >= sc_ObjectM.maxBlinkTime)
            {
                sc_ObjectM.warningRenderer.enabled = !sc_ObjectM.warningRenderer.enabled;
                sc_ObjectM.blinkTime = 0;
            }
        }
        else
        {
            sc_ObjectM.warningRenderer.enabled = false;
        }
    }
    public void Stunned()
    {
        if (sc_ObjectM.isDenied)
        {
            sc_ObjectM.deniedTimer += Time.deltaTime;
            
            if (sc_ObjectM.deniedTimer >= sc_ObjectM.deniedDuration)
            {
                sc_ObjectM.isDenied = false;
                sc_ObjectM.deniedTimer = 0;
                
            }
        }
    }
}
