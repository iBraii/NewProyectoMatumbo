using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CulpaC : MonoBehaviour
{
    public CulpaM sc_culpaM;

    void Start()
    {
        sc_culpaM = GetComponent<CulpaM>();
        sc_culpaM.player.GetComponent<PlayerM>().canMove = false;
        Presentation();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void IncreaseIndex()
    {
        sc_culpaM.waypointIndex++;
        if (sc_culpaM.waypointIndex >= sc_culpaM.waypoint.Length)
            this.gameObject.SetActive(false);
    }
    public void CloseToWaypoint()
    {
        sc_culpaM.distanceToWaypoint = Vector3.Distance(transform.position, sc_culpaM.waypoint[sc_culpaM.waypointIndex].position);
        if (sc_culpaM.distanceToWaypoint <= 1)
        {
            IncreaseIndex();
            sc_culpaM.agent.SetDestination(sc_culpaM.waypoint[sc_culpaM.waypointIndex].position);

        }
       
    }

    public void Presentation()
    {
        sc_culpaM.thirdPersonCamera.GetComponent<CinemachineFreeLook>().LookAt = this.gameObject.transform;
        Invoke("ResetCamera", 2);
        
    }
    public void ResetCamera()
    {
        sc_culpaM.player.GetComponent<PlayerM>().canMove = true;
        sc_culpaM.thirdPersonCamera.GetComponent<CinemachineFreeLook>().LookAt = sc_culpaM.player.transform;
    }
   
    
}
