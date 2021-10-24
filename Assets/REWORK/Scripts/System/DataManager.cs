using UnityEngine;

[System.Serializable] public class Data
{
    public float sensitivity;
    public float volume;
    public bool[] levelCompleted = new bool[6];
    public bool[] achievementCompleted=new bool[6];
}

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        SaveSystem.Load();
        Debug.Log("Data Loaded");
        DontDestroyOnLoad(gameObject);
    }


    [ContextMenu("RESET ALL BITCH")]
    public void ResetDefault()
    {
        SaveSystem.data.sensitivity = 275;
        SaveSystem.data.volume = .5f;

        for(int i = 0; i < SaveSystem.data.levelCompleted.Length; i++)
        {
            SaveSystem.data.levelCompleted[i] = false;
            SaveSystem.data.achievementCompleted[i] = false;
        }
        SaveSystem.Save();
    }


    [ContextMenu("RESET PREFERENCES")]
    public void ResetPreferences()
    {
        SaveSystem.data.sensitivity = 275;
        SaveSystem.data.volume = .5f;

        SaveSystem.Save();
    }


    [ContextMenu("RESET ACHIEVEMENTS")]
    public void ResetAchivement()
    {
        for (int i = 0; i < SaveSystem.data.levelCompleted.Length; i++)
        {
            SaveSystem.data.achievementCompleted[i] = false;
        }
        SaveSystem.Save();
    }
    private void OnApplicationQuit() => SaveSystem.Save();
}
