using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowC : MonoBehaviour
{
    private ShadowM sc_ShadowM;
    void Start()
    {
        sc_ShadowM = GetComponent<ShadowM>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckDistanceToPlayer()
    {
        if (sc_ShadowM.distanceToPlayer <= sc_ShadowM.agroRange && !sc_ShadowM.isDenied)
        {
            sc_ShadowM.canChase = true;
            sc_ShadowM.blink = true;
        } else if (sc_ShadowM.distanceToPlayer >= sc_ShadowM.chaseRange || sc_ShadowM.isDenied)
        {
            sc_ShadowM.canChase = false;
            sc_ShadowM.blink = false;
        }
    }

    public void ChasePlayer()
    {
        if (sc_ShadowM.canChase)
        {
            sc_ShadowM.pAgent.SetDestination(sc_ShadowM.obj_player.transform.position);
            sc_ShadowM.pAgent.speed = 6f;
            Quaternion rotation = Quaternion.LookRotation(sc_ShadowM.pos_lookDirection);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, sc_ShadowM.rotationSpeed * Time.deltaTime);

        }
        else
        {
            sc_ShadowM.pAgent.SetDestination(sc_ShadowM.pos_initialPosition);
            sc_ShadowM.pAgent.speed = 3.5f;
        }
        if (sc_ShadowM.blink)
        {
            sc_ShadowM.blinkTime += Time.deltaTime;
            if (sc_ShadowM.blinkTime >= sc_ShadowM.maxBlinkTime)
            {
                sc_ShadowM.warningRenderer.enabled = !sc_ShadowM.warningRenderer.enabled;
                sc_ShadowM.blinkTime = 0;
            }
        }
        else
        {
            sc_ShadowM.warningRenderer.enabled = false;
        }
    }

    public void Stunned()
    {
        if (sc_ShadowM.isDenied)
        {
                sc_ShadowM.deniedTimer += Time.deltaTime;
            sc_ShadowM.canChase = false;
            

            if (sc_ShadowM.deniedTimer >= sc_ShadowM.deniedDuration)
            {
                sc_ShadowM.deniedTimer = 0;
                sc_ShadowM.isDenied = false;
            }      
        }
    }
}
