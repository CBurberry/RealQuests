using Newtonsoft.Json;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
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

    public void ModifyQuest(Guid entryToEdit, Quest editedEntry)
    {
        if (editedEntry.Id != entryToEdit)
        {
            return;
        }

        var entry = Data.ActiveQuests.First(x => x.Id == entryToEdit);
        entry = editedEntry;
        Save();
    }

    public void RemoveQuest(Quest quest)
    {
        Data.ActiveQuests.Remove(quest);
        Save();
    }

    public void AddRewards(Reward[] rewards)
    {
        //Find the corresponding entry and add count, or add a the entry if it doesnt exist.
        foreach (var reward in rewards)
        {
            if (Data.OwnedRewards.Any(x => x.Type == reward.Type))
            {
                Data.OwnedRewards.First(x => x.Type == reward.Type).Count += reward.Count;
            }
            else 
            {
                Data.OwnedRewards.Add(reward);
            }
        }

        Save();
    }

    public int GetRewardCount(RewardType type)
    {
        var reward = Data.OwnedRewards.FirstOrDefault(x => x.Type == type);
        return (reward != null) ? reward.Count : 0;
    }

    public bool HasRewards(RewardType type, int count)
    {
        return Data.OwnedRewards.Any(x => x.Type == type && x.Count >= count);
    }

    public void ConsumeReward(RewardType type, int count)
    {
        //Check there is a sufficient availablity
        if (!HasRewards(type, count))
        {
            return;
        }

        //Either decrement the count or remove the reward item outright.
        var reward = Data.OwnedRewards.First(x => x.Type == type);
        if (reward.Count > count)
        {
            reward.Count -= count;
        }
        else if (reward.Count == count)
        {
            Data.OwnedRewards.Remove(reward);
        }

        Save();
    }

    public void Save()
    {
        string jsonData = JsonConvert.SerializeObject(Data, Formatting.Indented);
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
        Data.RepeatableQuests = new List<Quest>();
        Data.CompletedQuests = new List<Quest>();
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
    public List<Quest> RepeatableQuests;
    public List<Quest> CompletedQuests;
    public List<Reward> OwnedRewards;
}
