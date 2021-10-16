using UnityEngine;

public class SecondaryMissionCall : MonoBehaviour
{
    public static void CallEvent(System.Action action)
    {
        action?.Invoke();
    }
}
