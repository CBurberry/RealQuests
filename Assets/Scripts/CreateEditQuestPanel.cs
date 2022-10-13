using System;
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
    [SerializeField]
    private Toggle isRepeatableToggle;
    [SerializeField]
    private CooldownEntry cooldownEntry;
    [SerializeField]
    private GameObject backButton;
    [SerializeField]
    private GameObject addNewQuestButton;
    [SerializeField]
    private QuestsPanel questsPanel;
    [SerializeField]
    private QuestSelectionPanel questSelectionPanel;

    private void OnEnable()
    {
        if (WindowMode == Mode.Create)
        {
            headerText.text = "Create New Quest";
            titleInput.text = string.Empty;
            rewardCountInput.text = string.Empty;
            isRepeatableToggle.isOn = false;

            createButton.gameObject.SetActive(true);
            editButton.gameObject.SetActive(false);
        }
        else if (WindowMode == Mode.Edit) 
        {
            headerText.text = "Edit Quest";
            var selectedQuest = questSelectionPanel.Quest;
            titleInput.text = selectedQuest.Title;
            rewardCountInput.text = selectedQuest.Rewards[0].Count.ToString();
            isRepeatableToggle.isOn = selectedQuest.IsRepeatable;

            if (isRepeatableToggle.isOn) 
            {
                cooldownEntry.SetEntryValues((uint)selectedQuest.CooldownDuration.Days, (uint)selectedQuest.CooldownDuration.Hours);
            }

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

        if (isRepeatableToggle.isOn && !cooldownEntry.IsInputValid())
        {
            return;
        }

        backButton.SetActive(true);
        addNewQuestButton.SetActive(true);
        questsPanel.SetActiveQuestItemsPanelActive(true);
        questsPanel.SetInactiveQuestItemsPanelActive(true);
        gameObject.SetActive(false);

        //Create new quest
        Quest quest = new Quest();
        quest.Title = titleInput.text;
        quest.Rewards = new Reward[]
        {
            new Reward
            {
                Type = RewardType.LuxuryToken,
                Count = count
            }
        };
        quest.IsRepeatable = isRepeatableToggle.isOn;

        if (isRepeatableToggle.isOn) 
        {
            quest.CooldownDuration = cooldownEntry.GetCooldownTimeSpan();
        }

        AppManager.Instance.AddNewQuestItem(quest);
    }

    public void EditQuest()
    {
        if (isRepeatableToggle.isOn && !cooldownEntry.IsInputValid())
        {
            return;
        }

        backButton.SetActive(true);
        addNewQuestButton.SetActive(true);
        questsPanel.SetActiveQuestItemsPanelActive(true);
        questsPanel.SetInactiveQuestItemsPanelActive(true);
        gameObject.SetActive(false);

        var quest = questSelectionPanel.Quest.DeepClone();
        quest.Title = titleInput.text;
        quest.Rewards[0].Count = int.Parse(rewardCountInput.text);
        quest.IsRepeatable = isRepeatableToggle.isOn;

        if (isRepeatableToggle.isOn)
        {
            quest.CooldownDuration = cooldownEntry.GetCooldownTimeSpan();
        }

        AppManager.Instance.EditQuestItem(questSelectionPanel.Quest, quest);
    }
}
