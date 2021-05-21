using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HumanoidM))]
public class HumanoidC : MonoBehaviour
{
    private HumanoidM sc_humanoidM;
    private PlayerM sc_playerM;
    // Start is called before the first frame update
    void Start()
    {
        sc_humanoidM = GetComponent<HumanoidM>();
        sc_playerM = GetComponent<PlayerM>();
    }

    //----------------------------Movimiento en patrulla--------------------------------------------------
    void IncreaseIndex()
    {
        sc_humanoidM.waypointIndex++;
        if (sc_humanoidM.waypointIndex >= sc_humanoidM.waypoint.Length)
            sc_humanoidM.waypointIndex = 0;
    }
    public void CloseToWaypoint()
    {
        sc_humanoidM.distanceToWaypoint = Vector3.Distance(transform.position, sc_humanoidM.waypoint[sc_humanoidM.waypointIndex].position);
        if (!sc_humanoidM.isFollowingPlayer)
        {
            if (sc_humanoidM.distanceToWaypoint <= sc_humanoidM.pAgent.stoppingDistance)
            {
                IncreaseIndex();
                sc_humanoidM.pAgent.SetDestination(sc_humanoidM.waypoint[sc_humanoidM.waypointIndex].position);
            }
        }
    }
    //----------------------------Seguimiento del jugador--------------------------------------------------
    public void FollowPlayer()
    {
        if (sc_humanoidM.distanceToPlayer <= sc_humanoidM.agroRange&& !sc_humanoidM.player.GetComponent<PlayerM>().isHiding && !sc_humanoidM.isDenied
            &&sc_humanoidM.playerOnView)
        {
            
            sc_humanoidM.isFollowingPlayer = true;
            sc_humanoidM.blink = true;
            sc_humanoidM.pAgent.SetDestination(sc_humanoidM.player.transform.position);
            Quaternion rotation = Quaternion.LookRotation(sc_humanoidM.pos_lookDirection);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, sc_humanoidM.rotationSpeed * Time.deltaTime);
            sc_humanoidM.timeToReturn = 0;
        }
        if(sc_humanoidM.blink)
        {
            sc_humanoidM.blinkTime += Time.deltaTime;
            if (sc_humanoidM.blinkTime >= sc_humanoidM.maxBlinkTime)
            {
                sc_humanoidM.warningRenderer.enabled = !sc_humanoidM.warningRenderer.enabled;
                sc_humanoidM.blinkTime = 0;
            }
        }
        else
        {
            sc_humanoidM.warningRenderer.enabled = false;
        }
        if((sc_humanoidM.isFollowingPlayer && sc_humanoidM.distanceToPlayer >= sc_humanoidM.agroRange)||
            (sc_humanoidM.isFollowingPlayer && sc_humanoidM.player.GetComponent<PlayerM>().isHiding)||
            sc_humanoidM.isDenied)
        {
            sc_humanoidM.blink = false;
            sc_humanoidM.timeToReturn += Time.deltaTime;
            sc_humanoidM.pAgent.isStopped = true;
            if (sc_humanoidM.timeToReturn > sc_humanoidM.timeToReturnMax)
            {
                sc_humanoidM.pAgent.isStopped = false;
                sc_humanoidM.pAgent.SetDestination(sc_humanoidM.waypoint[sc_humanoidM.waypointIndex].position);
                sc_humanoidM.isFollowingPlayer = false;
                sc_humanoidM.timeToReturn = 0;
            }
        }
        
    }
    public void DeniedUpdater()
    {
        if(sc_playerM.isUsingWeapon)
        {
            sc_humanoidM.isDenied = true;
        }     
    }

    public Vector3 PointForAngle(float angle, float distance)
    {
        return transform.TransformDirection(
            new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0, Mathf.Cos(angle * Mathf.Deg2Rad)))
            * distance;
    }
    public void DetectPlayer()
    {
        Vector3 playerVector = sc_humanoidM.player.transform.position - transform.position;
        if (Vector3.Angle(playerVector.normalized, transform.forward) < sc_humanoidM.visionAngle / 2)
        {
            if (playerVector.magnitude < sc_humanoidM.visionDistance)
            {
                sc_humanoidM.playerOnView = true;
            }
            else
                sc_humanoidM.playerOnView = false;
        }
    }
    //----------------------------Aturdimiento con el atrapasuenos--------------------------------------------------
    public void Stunned()
    {
        if (sc_humanoidM.isDenied)
        {
            sc_humanoidM.isDenied = true;
            sc_humanoidM.stunTime += Time.deltaTime;
            sc_humanoidM.pAgent.isStopped = true;

            if (sc_humanoidM.stunTime >= sc_humanoidM.maxStunTime)
            {
                sc_humanoidM.stunTime = 0;
                sc_humanoidM.isDenied = false;
                sc_humanoidM.pAgent.isStopped = false;
                sc_humanoidM.pAgent.SetDestination(sc_humanoidM.waypoint[sc_humanoidM.waypointIndex].position);
            }
        }
        
    }

    private void OnDrawGizmos()
    {
        if (sc_humanoidM.visionAngle <= 0)
            return;

        float halfVisonAngle = sc_humanoidM.visionAngle / 2;
        Vector3 point1, point2;
        point1 = PointForAngle(halfVisonAngle,sc_humanoidM.visionDistance);
        point2 = PointForAngle(-halfVisonAngle, sc_humanoidM.visionDistance);
        Gizmos.color =sc_humanoidM.playerOnView?Color.green:Color.red;
        Gizmos.DrawLine(transform.position, transform.position + point1);
        Gizmos.DrawLine(transform.position, transform.position + point2);

        Gizmos.DrawRay(transform.position, transform.forward * 4);
    }
}
