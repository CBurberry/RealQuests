using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class QuestItemButton : MonoBehaviour
{
    //UI references
    [SerializeField]
    private Text titleText;
    [SerializeField]
    private LayoutGroup rewardsLayoutGroup;
    [SerializeField]
    private Text cooldownTimerText;

    //Prefab references
    [SerializeField]
    private GameObject rewardTextElement;

    //References
    private QuestsPanel questsPanel;
    private Quest quest;

    private const string timerFormat = @"dd\:hh\:mm";

    //Update once every half minute
    private float checkDelay = 30f;
    private float checkTimer = 0f;

    private void Start()
    {
        if (Application.isPlaying) 
        {
            var gameObject = GameObject.Find("QuestsPanel");
            questsPanel = gameObject.GetComponent<QuestsPanel>();
        }
    }

    private void Update()
    {
        if (quest.IsCooldownActive == false) 
        {
            return;
        }

        //Set the update to only occur once a minute to save on processing.
        checkTimer += Time.deltaTime;
        if (checkTimer > checkDelay)
        {
            checkTimer = 0f;
            UpdateCooldownTimer();
            CheckCooldownComplete();
        }
    }

    public void SetData(Quest quest)
    {
        this.quest = quest;
        titleText.text = quest.Title;

        DropLayoutGroupChildren();

        foreach (Reward reward in quest.Rewards)
        {
            Text rewardText = Instantiate(rewardTextElement, rewardsLayoutGroup.transform).GetComponent<Text>();
            rewardText.text = reward.Type.ToString() + " x" + reward.Count;
        }

        cooldownTimerText.gameObject.SetActive(quest.HasCooldownElapsed());
        if (quest.HasCooldownElapsed()) 
        {
            cooldownTimerText.text = quest.GetElapsedCooldown().ToString(timerFormat);
        }
    }

    public void OnClick()
    {
        questsPanel.SetSelectedQuest(quest);
        questsPanel.SetFooterButtonsActive(false);
        questsPanel.SetActiveQuestItemsPanelActive(false);
        questsPanel.SetInactiveQuestItemsPanelActive(false);
        questsPanel.SetQuestSelectionPanelActive(true);
    }

    private void DropLayoutGroupChildren()
    {
        int i = 0;

        //Array to hold all child obj
        GameObject[] allChildren = new GameObject[rewardsLayoutGroup.gameObject.transform.childCount];

        //Find all child obj and store to that array
        foreach (Transform child in rewardsLayoutGroup.gameObject.transform)
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

    private void UpdateCooldownTimer()
    {
        if (quest == null)
        {
            return;
        }

        if (!quest.HasCooldownElapsed())
        {
            return;
        }

        cooldownTimerText.text = quest.GetElapsedCooldown().ToString(timerFormat);
    }

    private void CheckCooldownComplete()
    {
        if (quest.IsRepeatable && !quest.HasCooldownElapsed()) 
        {
            AppManager.Instance.RactivateRepeatableQuest(quest);
        }
    }
}
