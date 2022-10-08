using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateEditQuestPanel : MonoBehaviour
{
    [Serializable]
    public enum Mode : int
    {
        Create = 0,
        Edit
    }

    public Mode WindowMode;

    [SerializeField]
    private Text headerText;

    [SerializeField]
    private InputField titleInput;

    [SerializeField]
    private Dropdown rewardDropdown;

    [SerializeField]
    private InputField rewardCountInput;

    [SerializeField]
    private Button createButton;

    [SerializeField]
    private Button editButton;

    //UI Elements to show
    [SerializeField]
    private GameObject backButton;

    [SerializeField]
    private GameObject addNewQuestButton;

    [SerializeField]
    private GameObject questItemsGroup;

    [SerializeField]
    private QuestSelectionPanel questSelectionPanel;

    private void OnEnable()
    {
        if (WindowMode == Mode.Create)
        {
            headerText.text = "Create New Quest";
            titleInput.text = string.Empty;
            rewardCountInput.text = string.Empty;

            createButton.gameObject.SetActive(true);
            editButton.gameObject.SetActive(false);
        }
        else if (WindowMode == Mode.Edit) 
        {
            headerText.text = "Edit Quest";
            var selectedQuest = questSelectionPanel.Quest;
            titleInput.text = selectedQuest.Title;
            rewardCountInput.text = selectedQuest.Rewards[0].Count.ToString();

            createButton.gameObject.SetActive(false);
            editButton.gameObject.SetActive(true);
        }
    }

    public void SetWindowMode(int mode)
    {
        WindowMode = (Mode)mode;
    }

    public void CreateQuest()
    {
        if (string.IsNullOrWhiteSpace(titleInput.text)) 
        {
            return;
        }

        int count;
        if (!int.TryParse(rewardCountInput.text, out count) || count <= 0) 
        {
            return;
        }

        backButton.SetActive(true);
        addNewQuestButton.SetActive(true);
        questItemsGroup.SetActive(true);
        gameObject.SetActive(false);

        AppManager.Instance.AddNewQuestItem(titleInput.text, RewardType.LuxuryToken, count);
    }

    public void EditQuest()
    {
        backButton.SetActive(true);
        addNewQuestButton.SetActive(true);
        questItemsGroup.SetActive(true);
        gameObject.SetActive(false);

        var quest = questSelectionPanel.Quest;
        quest.Title = titleInput.text;
        quest.Rewards[0].Count = int.Parse(rewardCountInput.text);
        AppManager.Instance.EditQuestItem(quest.Id, quest);
    }
}
