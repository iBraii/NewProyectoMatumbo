using UnityEngine;

[System.Serializable] public class Data
{
    public float sensitivity = 275;
    public float volume = .5f;
    public bool[] levelCompleted = new bool[6];
    public bool[] achievementCompleted = new bool[6];
    public bool[] levelCompletedNoDmg = new bool[6];
}

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        SaveSystem.Load();
    }


    [ContextMenu("Reset all")]
    public void ResetDefault()
    {
        SaveSystem.data.sensitivity = 275;
        SaveSystem.data.volume = .5f;

        for(int i = 0; i < SaveSystem.data.levelCompleted.Length; i++)
        {
            SaveSystem.data.levelCompleted[i] = false;
            SaveSystem.data.achievementCompleted[i] = false;
            SaveSystem.data.levelCompletedNoDmg[i] = false;
        }
        SaveSystem.Save();
    }


    [ContextMenu("Reset preferences")]
    public void ResetPreferences()
    {
        SaveSystem.data.sensitivity = 275;
        SaveSystem.data.volume = .5f;

        SaveSystem.Save();
    }


    [ContextMenu("Reset achievements")]
    public void ResetAchivement()
    {
        for (int i = 0; i < SaveSystem.data.levelCompleted.Length; i++)
            SaveSystem.data.achievementCompleted[i] = false;
        SaveSystem.Save();
    }

    [ContextMenu("Reset achievements")]
    public void ResetLevelsCompleted()
    {
        for (int i = 0; i < SaveSystem.data.levelCompleted.Length; i++)
        {
            SaveSystem.data.levelCompleted[i] = false;
            SaveSystem.data.levelCompletedNoDmg[i] = false;
        }
        SaveSystem.Save();
    }

    private void OnApplicationQuit() => SaveSystem.Save();
}
