using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NewHideInBlanket : MonoBehaviour
{
    
    //[SerializeField] bool blanketAtReach;
    //private PlayerSingleton ps;

    //private void Awake()
    //{
    //    ps=PlayerSingleton.Instance;
    //    ps.interactAction.performed +=Hide;

    //}
    //private void OnDisable()
    //{
    //    ps.interactAction.performed -= Hide;
    //}
    //void Hide(InputAction.CallbackContext callback)
    //{
    //    if (blanketAtReach)
    //    {
    //        if (PlayerSingleton.Instance.isHiding)
    //        {
    //            PlayerSingleton.Instance.isHiding = false;
    //        }
    //        else
    //        {
    //            PlayerSingleton.Instance.isHiding = true;
    //        }
    //    }
    //}

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.tag == "Blanket")
    //    {
    //        blanketAtReach = true;
    //    }
    //}
    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.tag == "Blanket")
    //    {
    //        blanketAtReach = false;
    //    }
    //}
}
