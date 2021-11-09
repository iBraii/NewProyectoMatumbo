using UnityEngine;

public class CheckTrigger : MonoBehaviour
{
    public int checkPointId;
    private CheckPoints cp;

    private void Awake() => cp = FindObjectOfType<CheckPoints>();

    private void Start()
    {
        //if (CheckPoints.currentCPIndex >= checkPointId) gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //CheckPoints.currentCPIndex = checkPointId;
            cp.SavePositions(checkPointId);
            transform.localScale = Vector3.zero;
        }
    }
}
