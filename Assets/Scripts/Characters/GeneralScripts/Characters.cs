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
        return Physics.Raycast(transform.position, Vector3.down, distanceToTheGround, GroundLayer);

    }
    public void LifeController(float _maxLife)
    {
        if(life >= _maxLife)
        {
            life = _maxLife;
        }
        
    }
}
