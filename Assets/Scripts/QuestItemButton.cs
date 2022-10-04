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

    //Prefab references
    [SerializeField]
    private GameObject rewardTextElement;

    //References
    private QuestsPanel questsPanel;
    private Quest quest;

    private void Start()
    {
        if (Application.isPlaying) 
        {
            var gameObject = GameObject.Find("QuestsPanel");
            questsPanel = gameObject.GetComponent<QuestsPanel>();
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
    }

    public void OnClick()
    {
        questsPanel.SetSelectedQuest(quest);
        questsPanel.SetFooterButtonsActive(false);
        questsPanel.SetQuestItemsPanelActive(false);
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
}
