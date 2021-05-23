using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxPush : MonoBehaviour
{
    public float mxDistance, wallDistance;
    public LayerMask playerMask, wallMask;
    [SerializeField]
    private Movement sc_movement;
    public bool right, left, back, forward, canPushx, canPushz;
    public RaycastHit groundHit;
    public Vector3 groundPoint;
    public Vector3 groundSum;

    // Start is called before the first frame update
    void Start()
    {

        sc_movement = GetComponent<Movement>();
        canPushx = true;
        canPushz = true;
        //groundSum = new Vector3(0,0.75f, 0);
    }

    // Update is called once per frame
    public bool BoxLeftSide()
    {
        return Physics.Raycast(transform.localPosition, Vector3.left, mxDistance, playerMask);
    }
    public bool BoxRightSide()
    {
        return Physics.Raycast(transform.localPosition, Vector3.right, mxDistance, playerMask);
    }
    public bool BoxBackSide()
    {
        return Physics.Raycast(transform.localPosition, Vector3.back, mxDistance, playerMask);
    }
    public bool BoxForwardSide()
    {
        return Physics.Raycast(transform.localPosition, Vector3.forward, mxDistance, playerMask);
    }
    //void GroundRayCheck()
    //{
    //    if(Physics.Raycast(transform.localPosition, Vector3.down,out groundHit, 20, wallMask))
    //    {
    //        groundPoint = groundHit.point;
    //    }
    //}
    ////////////////////////////////////////////////////////////////////////////////////////////
    private void Update()
    {
        this.left = Physics.Raycast(this.transform.localPosition, Vector3.left, wallDistance, wallMask);
        this.right = Physics.Raycast(this.transform.localPosition, Vector3.right, wallDistance, wallMask);
        this.back = Physics.Raycast(this.transform.localPosition, Vector3.back, wallDistance, wallMask);
        this.forward = Physics.Raycast(this.transform.localPosition, Vector3.forward, wallDistance, wallMask);

        if (left || right)
        {
            canPushx = false;
        }
        else if (!left&&!right)
        {
            canPushx = true;
        }
        if(back || forward)
        {
            canPushz = false;
        }
        else if(!back&&!forward)
        {
            canPushz = true;
        }
        Debug.Log(this.forward+"a");
    }

   /* public bool BoxLeftWall()
    {
        return Physics.Raycast(transform.localPosition, Vector3.left, wallDistance, wallMask);
    }
    public bool BoxRightWall()
    {
        return Physics.Raycast(transform.localPosition, Vector3.right, wallDistance, wallMask);
    }
    public bool BoxBackWall()
    {
        return Physics.Raycast(transform.localPosition, Vector3.back, wallDistance, wallMask);
    }
    public bool BoxForwardWall()
    {
        return Physics.Raycast(transform.localPosition, Vector3.forward, wallDistance, wallMask);
    }*/



    ////////////////////////////////////////////////////////////////////////////////////////////

    public void PushBox(float speedx, float speedz)
    {
        if (BoxLeftSide() && !right)
        {
            sc_movement.Move_Anydir(-speedx, 0, 0);
        }
        if (BoxRightSide() && !left)
        {
            sc_movement.Move_Anydir(speedx, 0, 0);
        }
        if (BoxBackSide() && !forward)
        {
            sc_movement.Move_Anydir(0, 0, -speedz);
        }
        if (BoxForwardSide() && !back)
        {
            sc_movement.Move_Anydir(0, 0, speedz);
        }
    }
    public void PullBox(float speedx, float speedz)
    {
        if (BoxLeftSide())
        {
            sc_movement.Move_Anydir(-speedx, 0, 0);
        }
        if (BoxRightSide())
        {
            sc_movement.Move_Anydir(speedx, 0, 0);
        }
        if (BoxBackSide())
        {
            sc_movement.Move_Anydir(0, 0, -speedz);
        }
        if (BoxForwardSide())
        {
            sc_movement.Move_Anydir(0, 0, speedz);
        }
    }


}
