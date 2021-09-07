using UnityEngine;

public class GroundCheckUpdater : MonoBehaviour
{
    [SerializeField] LayerMask GroundLayer;
    [SerializeField] float floorDistance;
    bool Grounded() => Physics.Raycast(transform.position, Vector3.down, floorDistance, GroundLayer);
    private void Awake()
    {
        floorDistance = 0.1635f;
        PlayerSingleton.Instance.isGrounded = Grounded();
    }

    void SetSingletonData()
    {
        if (Grounded() && PlayerSingleton.Instance.isGrounded == false)
        {
            PlayerSingleton.Instance.isGrounded = true;
            Debug.Log(PlayerSingleton.Instance.isGrounded);
        }
            
        else if (!Grounded() && PlayerSingleton.Instance.isGrounded)
        {
            PlayerSingleton.Instance.isGrounded = false;
            Debug.Log(PlayerSingleton.Instance.isGrounded);
        }
    }
    private void Update()
    {
        SetSingletonData();
    }
}
