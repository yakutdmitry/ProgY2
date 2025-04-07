using System;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static void loadData(ScriptableObject SO, string name)
    {
        string path = Path.Combine(Application.persistentDataPath, name); 
        
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            JsonUtility.FromJsonOverwrite(json, SO);
        }
        else
        {
            Debug.Log("No file found");
        }
    }

    public static void saveData(ScriptableObject SO, string name)
    {
        string path = Path.Combine(Application.persistentDataPath, name);
        string json = JsonUtility.ToJson(SO);
        
        File.WriteAllText(path, json);
    }
}
