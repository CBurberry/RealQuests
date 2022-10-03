using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestsPanel : MonoBehaviour
{
    [SerializeField]
    private GameObject layoutGroup;

    public void RefreshQuests()
    {
        Debug.Log(nameof(RefreshQuests));
        //TODO - recreate all elements of the quests panel scrollview from data
    }
}
