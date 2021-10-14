using System.IO;
using UnityEngine;

public enum DataType
{
    Settings, LevelInformation, Achievements
}
public static class SaveSystem
{
    #if UNITY_EDITOR
    private static readonly string SavePath = Application.dataPath + "/Saves/";
    #else
    private static readonly string SavePath = Application.persistentDataPath + "/Saves/";
    #endif

    public static void Init()
    {
        Debug.Log(SavePath);

        if (!Directory.Exists(SavePath))
            Directory.CreateDirectory(SavePath);
    }

    private static string PathType(DataType type)
    {
        string path = "";
        switch (type)
        {
            case DataType.Settings: 
                path = "settings.txt"; 
                break;
            case DataType.LevelInformation: 
                path = "levelInformation.txt"; 
                break;
            case DataType.Achievements:
                path = "achievements.txt";
                break;
            
        }
        return path;
    }
    public static void SaveData(string json, DataType type) { File.WriteAllText(SavePath + PathType(type), json); }

    public static string LoadData(DataType type)
    {
        string finalPath = PathType(type);
        if (File.Exists(SavePath + finalPath))
        {
            string json = File.ReadAllText(SavePath + finalPath);
            return json;
        }
        else
            return null;
    }
}
