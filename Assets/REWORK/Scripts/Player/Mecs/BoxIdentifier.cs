using UnityEngine;

public class BoxIdentifier : MonoBehaviour
{
    public int boxType;
    private Rigidbody rb;
    public float centerMassY;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0, centerMassY, 0);
    }
}
