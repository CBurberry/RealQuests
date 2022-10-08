using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class QuestSelectionPanel : MonoBehaviour
{
    [HideInInspector]
    public Quest Quest;

    //UI Elements to show
    [SerializeField]
    private GameObject backButton;

    [SerializeField]
    private GameObject addNewQuestButton;

    [SerializeField]
    private GameObject questItemsGroup;

    [SerializeField]
    private Text titleText;

    //References
    [SerializeField]
    private QuestsPanel questsPanel;
    [SerializeField]
    private CreateEditQuestPanel createEditQuestPanel;

    public void SetTitle(string title)
    {
        titleText.text = title;
    }

    public void CompleteQuest()
    {
        Quest.Completions++;
        if (!Quest.IsRepeatable) 
        {
            //Remove quest from active quests list to completed quests 
            if (!SaveSystem.Data.ActiveQuests.Remove(Quest))
            {
                throw new InvalidOperationException("Could remove quest from active quests!");
            }

            SaveSystem.Data.CompletedQuests.Add(Quest);
        }

        //Credit rewards to owned rewards
        SaveSystem.Instance.AddRewards(Quest.Rewards);

        //Update quest view
        questsPanel.RefreshQuests();
        Close();
    }

    public void EditQuest()
    {
        gameObject.SetActive(false);
        createEditQuestPanel.WindowMode = CreateEditQuestPanel.Mode.Edit;
        createEditQuestPanel.gameObject.SetActive(true);
    }

    public void DeleteQuest()
    {
        SaveSystem.Instance.RemoveQuest(Quest);

        //Update quest view
        questsPanel.RefreshQuests();
        Close();
    }

    private void Close()
    {
        //Set visibilities
        backButton.SetActive(true);
        addNewQuestButton.SetActive(true);
        questItemsGroup.SetActive(true);
        gameObject.SetActive(false);
    }
}
