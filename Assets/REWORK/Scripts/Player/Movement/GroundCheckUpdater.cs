using UnityEngine;

public class GroundCheckUpdater : MonoBehaviour
{
    [SerializeField] LayerMask GroundLayer;
    [SerializeField] float floorDistance;
    bool Grounded() => Physics.Raycast(transform.position, Vector3.down, floorDistance, GroundLayer);
    private void Awake()
    {
        PlayerSingleton.Instance.isGrounded = Grounded();
    }

    void SetSingletonData()
    {
        if (Grounded() && PlayerSingleton.Instance.isGrounded == false)
        {
            PlayerSingleton.Instance.isGrounded = true;
        }
            
        else if (!Grounded() && PlayerSingleton.Instance.isGrounded)
        {
            PlayerSingleton.Instance.isGrounded = false;
        }
    }
    private void Update()
    {
        SetSingletonData();
    }
}
