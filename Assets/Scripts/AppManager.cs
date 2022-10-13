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

    public void AddNewQuestItem(Quest quest)
    {
        //Add to save data
        SaveSystem.Instance.AddQuest(quest);

        //Update UI scrollview with new element
        var questsPanel = activePanel.GetComponent<QuestsPanel>();
        questsPanel.RefreshQuests();
    }

    public void EditQuestItem(Quest originalEntry, Quest editedEntry)
    {
        SaveSystem.Instance.ModifyQuest(originalEntry, editedEntry);

        //Update UI scrollview with new element
        var questsPanel = activePanel.GetComponent<QuestsPanel>();
        questsPanel.RefreshQuests();
    }

    public void RactivateRepeatableQuest(Quest quest)
    {
        quest.IsCooldownActive = false;
        SaveSystem.Instance.ReactivateRepeatableQuest(quest);

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
