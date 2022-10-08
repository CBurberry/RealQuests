using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AppManager : MonoBehaviour
{
    public enum Panels : int
    {
        Entry = 0,
        Quests,
        Items
    }

    public static AppManager Instance { get; private set; }

    //Panels references from scene, align index of array with the enum value when setting references.
    [SerializeField]
    private GameObject[] panels;

    private GameObject activePanel;

    //Singleton pattern
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else 
        {
            Debug.LogWarning("Duplicate instances of " + typeof(AppManager) + " detected! Deleting duplicate.");
            Destroy(this);
        }

        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        SetPanelActive(0);
    }

    public void AddNewQuestItem(string title, RewardType rewardType, int count)
    {
        //Create new quest data
        Quest quest = new Quest();
        quest.Title = title;
        quest.Rewards = new Reward[]
        {
            new Reward
            {
                Type = rewardType,
                Count = count
            } 
        };

        //Add to save data
        SaveSystem.Instance.AddQuest(quest);

        //Update UI scrollview with new element
        var questsPanel = activePanel.GetComponent<QuestsPanel>();
        questsPanel.RefreshQuests();
    }

    public void EditQuestItem(Guid entryToEdit, Quest editedEntry)
    {
        SaveSystem.Instance.ModifyQuest(entryToEdit, editedEntry);

        //Update UI scrollview with new element
        var questsPanel = activePanel.GetComponent<QuestsPanel>();
        questsPanel.RefreshQuests();
    }

    public void SetPanelActive(int targetIndex)
    {
        activePanel?.SetActive(false);
        var target = panels[targetIndex];
        activePanel = target;
        target.SetActive(true);
    }
}
