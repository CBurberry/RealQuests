using Newtonsoft.Json;
using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    public static SaveSystem Instance { get; private set; }
    public static SaveData Data;

    private const string filename = "saved.json";
    private string fullPath;

    private void Awake()
    {
        //Singleton pattern
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("Duplicate instances of " + typeof(AppManager) + " detected! Deleting duplicate.");
            Destroy(this);
        }

        fullPath = Application.persistentDataPath + Path.DirectorySeparatorChar + filename;
        Debug.Log("Save data location: " + fullPath);

        Load();
        DontDestroyOnLoad(this);
    }

    public void AddQuest(Quest quest)
    {
        Data.ActiveQuests.Add(quest);
        Save();
    }

    public void Save()
    {
        string jsonData = JsonConvert.SerializeObject(Data);
        File.WriteAllText(fullPath, jsonData);
        Debug.Log("Saved data.");
    }

    private void Load()
    {
        //Try and load a file from the persist data location, if one does not exist - initialize a new object
        if (File.Exists(fullPath))
        {
            try
            {
                string jsonData = File.ReadAllText(fullPath);
                Data = JsonConvert.DeserializeObject<SaveData>(jsonData);
                Debug.Log("Loaded save data from file.");
            }
            catch (Exception ex)
            {
                Debug.LogError("Error loading save data, creating new data...");
                Debug.LogException(ex);
                InitNewSaveData();
            }
        }
        else 
        {
            Debug.Log("No save data found, creating new data...");
            InitNewSaveData();
        }
    }

    private void InitNewSaveData()
    {
        Data = new SaveData();
        Data.ActiveQuests = new List<Quest>();
        Data.OwnedRewards = new List<Reward>();

        //Save the new data so we have an initial copy in the filesystem
        Save();
    }

    private void OnApplicationQuit()
    {
        Save();
    }
}

[Serializable]
public struct SaveData
{
    public List<Quest> ActiveQuests;
    public List<Reward> OwnedRewards;
}
