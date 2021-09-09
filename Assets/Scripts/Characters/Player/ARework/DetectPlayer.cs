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
        player = GameObject.Find("NewPlayer");
        if (player == null)
        {
            Debug.LogError("No se encontró al player");
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

    public bool CheckIfMoreDistance(GameObject origin, float rangeDistance)
    {
        if (Vector3.Distance(origin.transform.position, player.transform.position) >= rangeDistance)
            return true;
        else
            return false;
    }
}
