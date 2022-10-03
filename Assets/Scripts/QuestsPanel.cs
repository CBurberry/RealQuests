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
