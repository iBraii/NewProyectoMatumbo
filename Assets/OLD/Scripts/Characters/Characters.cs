using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Characters : MonoBehaviour
{
    public float life;
    public float movSpeed;
    public float distanceToTheGround;
    public LayerMask GroundLayer;
    public bool isMoving;

    public bool GroundCheck()
    {
       if( Physics.Raycast(new Vector3(transform.localPosition.x - .155f, transform.localPosition.y, transform.localPosition.z ), Vector3.down, distanceToTheGround, GroundLayer) ||
        Physics.Raycast(new Vector3(transform.localPosition.x + .295f, transform.localPosition.y, transform.localPosition.z), Vector3.down, distanceToTheGround, GroundLayer) ||
        Physics.Raycast(new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z - .295f), Vector3.down, distanceToTheGround, GroundLayer)||
        Physics.Raycast(new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z + .295f), Vector3.down, distanceToTheGround, GroundLayer))
        {
            return true;
        }
       else
        {
            return false;
        }


    }
    public void LifeController(float _maxLife)
    {
        if(life >= _maxLife)
        {
            life = _maxLife;
        }
        if(life <= 0)
        {
            life = 0;
        }
    }
}
