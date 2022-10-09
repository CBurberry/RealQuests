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

    //Check for repeatable quests cooldowns once every minute.
    private float repeatableQuestCheckDelay = 60f;
    private float repeatableQuestCheckTimer = 0f;

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

    private void Update()
    {
        repeatableQuestCheckTimer += Time.deltaTime;
        if (repeatableQuestCheckTimer > repeatableQuestCheckDelay)
        {
            repeatableQuestCheckTimer = 0f;
            var elapsedQuests = SaveSystem.Data.RepeatableQuests.Where(x => !x.IsInCooldown()).ToList();
            for (int i = 0; i < elapsedQuests.Count(); i++)
            {
                RactivateRepeatableQuest(elapsedQuests[i]);
            }
        }
    }

    public void AddNewQuestItem(Quest quest)
    {
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

    public void RactivateRepeatableQuest(Quest quest)
    {
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
