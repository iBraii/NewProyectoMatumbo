using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxPush : MonoBehaviour
{
    public float mxDistance;
    public LayerMask playerMask;
    [SerializeField]
    private Movement sc_movement;

    // Start is called before the first frame update
    void Start()
    {

        sc_movement = GetComponent<Movement>();

    }

    // Update is called once per frame
    void Update()
    {
        /*Debug.DrawRay(transform.localPosition, Vector3.left , Color.white, 0.5f);
        Debug.DrawRay(transform.localPosition, Vector3.right, Color.white, 0.5f);
        Debug.DrawRay(transform.localPosition, Vector3.back, Color.white, 0.5f);
        Debug.DrawRay(transform.localPosition, Vector3.forward, Color.white, 0.5f);*/

    }
    public bool BoxLeftSide()
    {
        return Physics.Raycast(transform.localPosition, Vector3.left,mxDistance, playerMask);     
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
    public void MoveBox(float speedx,float speedz)
    {
        if(BoxLeftSide())
        {
            sc_movement.Move_Anydir(-speedx, 0, 0);
        }
        if(BoxRightSide())
        {
            sc_movement.Move_Anydir(speedx, 0, 0);
        }
        if(BoxBackSide())
        {
            sc_movement.Move_Anydir(0, 0, -speedz);
        }
        if(BoxForwardSide())
        {
            sc_movement.Move_Anydir(0, 0, speedz);
        }
    }
}
