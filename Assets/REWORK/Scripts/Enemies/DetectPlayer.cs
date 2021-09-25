using UnityEngine;

public class DetectPlayer : MonoBehaviour
{
    public GameObject player;

    private static DetectPlayer _detection;
    public static DetectPlayer detection
    {
        get
        {
            return _detection;
        }
    }

    private void Awake()
    {
        _detection = this;
        if (player == null)
        {
            Debug.LogWarning("No se encontró al player");
            return;
        }
    }

    public bool CheckIfLessDistance(GameObject origin, float rangeDistance)
    {
        if (Vector3.Distance(origin.transform.position, player.transform.position) <= rangeDistance)
            return true;
        else
            return false;
    }

    public bool CheckIfVisionRange(GameObject origin, float angle,float rangeDistance)
    {
        Vector3 forward = origin.transform.forward;
        Vector3 target = player.transform.position - origin.transform.position;
        target.y = 0;

        float myRange = Vector3.Angle(forward, target);

        if (myRange < angle/2 && CheckIfLessDistance(origin, rangeDistance))
            return true;
        else
            return false;
    }
}
