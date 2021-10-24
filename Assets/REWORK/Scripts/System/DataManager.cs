using UnityEngine;

[System.Serializable] public class Data
{
    public float sensitivity;
    public float volume;
    public bool[] levelCompleted;
    public bool[] achievementCompleted;
}

public class DataManager : MonoBehaviour
{
    private void Awake()
    {
        SaveSystem.Load();
        Debug.Log("Data Loaded");
    }

    private void OnApplicationQuit() => SaveSystem.Save();
}
