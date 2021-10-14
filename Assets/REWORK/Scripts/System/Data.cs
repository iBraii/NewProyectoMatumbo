using UnityEngine;
using System.Collections.Generic;
using Newtonsoft.Json;

[System.Serializable] public struct Setting
{
    public float sensitivity;
    public float volume;
}
[System.Serializable] public struct LevelInfo
{
    public List<bool> levelCompleted;
}
[System.Serializable] public struct Achievement
{
    public List<bool> missionCompleted;
}

public class Data : MonoBehaviour
{
    public Setting setting;
    public LevelInfo lvlInfo;
    public Achievement achieve;

    public static Data Instance;

    private void Awake()
    {
        SaveSystem.Init();
        LoadCurrentData(ref setting, DataType.Settings);
        LoadCurrentData(ref lvlInfo, DataType.LevelInformation);
        LoadCurrentData(ref achieve, DataType.Achievements);

        if (Instance == null) Instance = this;
        else Destroy(this.gameObject);
        DontDestroyOnLoad(this.gameObject);
    }
    private void OnApplicationQuit() => SaveAllData();

    public void LoadCurrentData<T>(ref T data, DataType type)
    {
        string json = SaveSystem.LoadData(type);
        while (json == null) { SaveCurrentData(data, type); json = SaveSystem.LoadData(type); }
        data = JsonConvert.DeserializeObject<T>(json);
    }

    public void SaveCurrentData<T>(T data, DataType type)
    {
        string json = JsonConvert.SerializeObject(data);
        SaveSystem.SaveData(json, type);
    }

    public void SaveAllData()
    {
        SaveCurrentData(setting, DataType.Settings);
        SaveCurrentData(lvlInfo, DataType.LevelInformation);
        SaveCurrentData(achieve, DataType.Achievements);
    }

    public void SaveSettings() => SaveCurrentData(setting, DataType.Settings);
    public void SaveLevelInfo() => SaveCurrentData(lvlInfo, DataType.LevelInformation);
    public void SaveAchievements() => SaveCurrentData(achieve, DataType.Achievements);
}
