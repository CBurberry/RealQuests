using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestsPanel : MonoBehaviour
{
    [SerializeField]
    private GameObject layoutGroup;

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

    public void RefreshQuests()
    {
        //Drop all child elements
        DropLayoutGroupChildren();

        //Recreate all elements of the quests panel scrollview from data
        foreach (var quest in SaveSystem.Data.ActiveQuests)
        {
            var questItemButton = Instantiate(questsItemPrefab, layoutGroup.transform).GetComponent<QuestItemButton>();
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
        component.SetTitle("Complete Quest '" + quest.Title + "'?");
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

    public void SetQuestItemsPanelActive(bool value)
    {
        layoutGroup.SetActive(value);
    }

    private void DropLayoutGroupChildren()
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
