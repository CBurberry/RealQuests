using System.Collections;
using System.Collections.Generic;
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

    //Data references
    [SerializeField]
    private Quest quest;

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
        //TODO
        Debug.LogWarning("QuestItem: '" + titleText.text + "' clicked!");
    }

    private void DropLayoutGroupChildren()
    {
        int i = 0;

        //Array to hold all child obj
        GameObject[] allChildren = new GameObject[rewardsLayoutGroup.gameObject.transform.childCount];

        //Find all child obj and store to that array
        foreach (Transform child in transform)
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
