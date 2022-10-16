using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestsPanel : MonoBehaviour
{
    [SerializeField]
    private GameObject activeQuestsLayoutGroup;
    [SerializeField]
    private GameObject inactiveQuestsLayoutGroup;
    [SerializeField]
    private GameObject questsItemPrefab;
    [SerializeField]
    private GameObject backButton;
    [SerializeField]
    private GameObject addNewQuestButton;
    [SerializeField]
    private GameObject createQuestPanel;
    [SerializeField]
    private GameObject questSelectionPanel;

    //Update once every minute
    private float checkDelay = 60f;
    private float checkTimer = 0f;

    private void Update()
    {
        //Set the update to only occur once a minute to save on processing.
        checkTimer += Time.deltaTime;
        if (checkTimer > checkDelay)
        {
            checkTimer = 0f;
            foreach (var item in inactiveQuestsLayoutGroup.GetComponentsInChildren<QuestItemButton>()) 
            {
                item.UpdateCooldownTimer();
                item.CheckCooldownComplete();
            }
        }
    }

    public void RefreshQuests()
    {
        //Drop all child elements
        DropLayoutGroupChildren(activeQuestsLayoutGroup);
        DropLayoutGroupChildren(inactiveQuestsLayoutGroup);

        //Recreate all elements of the active quests panel scrollview from data
        foreach (var quest in SaveSystem.Data.ActiveQuests)
        {
            var questItemButton = Instantiate(questsItemPrefab, activeQuestsLayoutGroup.transform).GetComponent<QuestItemButton>();
            questItemButton.SetData(quest);
        }

        //Recreate all elements of the inactive quests panel scrollview from data
        foreach (var quest in SaveSystem.Data.RepeatableQuests)
        {
            var questItemButton = Instantiate(questsItemPrefab, inactiveQuestsLayoutGroup.transform).GetComponent<QuestItemButton>();
            questItemButton.SetData(quest);
        }
    }

    public Quest GetSelectedQuest()
    {
        return questSelectionPanel.GetComponent<QuestSelectionPanel>().Quest;
    }

    public void SetSelectedQuest(Quest quest)
    {
        var component = questSelectionPanel.GetComponent<QuestSelectionPanel>();
        component.Quest = quest;
    }

    public void SetFooterButtonsActive(bool value)
    {
        backButton.SetActive(value);
        addNewQuestButton.SetActive(value);
    }

    public void SetCreateQuestPanelActive(bool value)
    {
        if (value) 
        {
            createQuestPanel.GetComponent<CreateEditQuestPanel>().WindowMode = CreateEditQuestPanel.Mode.Create;
        }

        createQuestPanel.SetActive(value);
    }

    public void SetEditQuestPanelActive(bool value)
    {
        if (value)
        {
            createQuestPanel.GetComponent<CreateEditQuestPanel>().WindowMode = CreateEditQuestPanel.Mode.Edit;
        }

        createQuestPanel.SetActive(value);
    }

    public void SetQuestSelectionPanelActive(bool value)
    {
        questSelectionPanel.SetActive(value);
    }

    public void SetActiveQuestItemsPanelActive(bool value)
    {
        activeQuestsLayoutGroup.SetActive(value);
    }

    public void SetInactiveQuestItemsPanelActive(bool value)
    {
        inactiveQuestsLayoutGroup.SetActive(value);
    }

    private void DropLayoutGroupChildren(GameObject layoutGroup)
    {
        int i = 0;

        //Array to hold all child obj
        GameObject[] allChildren = new GameObject[layoutGroup.gameObject.transform.childCount];

        //Find all child obj and store to that array
        foreach (Transform child in layoutGroup.gameObject.transform)
        {
            allChildren[i] = child.gameObject;
            i += 1;
        }
        var reversedAllChildren = allChildren.Reverse();

        //Now destroy them
        foreach (GameObject child in reversedAllChildren)
        {
            DestroyImmediate(child.gameObject);
        }
    }
}
