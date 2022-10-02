using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppManager : MonoBehaviour
{
    public enum Panels : int
    {
        Entry = 0,
        Quests,
        Items
    }

    public static AppManager Instance { get; private set; }

    //Panels references from scene, align index of array with the enum value when setting references.
    [SerializeField]
    private GameObject[] panels;

    private GameObject activePanel;

    //Singleton pattern
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else 
        {
            Debug.LogWarning("Duplicate instances of " + typeof(AppManager) + " detected! Deleting duplicate.");
            Destroy(this);
        }
    }

    private void Start()
    {
        SetPanelActive(0);
    }

    public void AddNewQuestItem(string title, RewardType rewardType, int count)
    {
        Debug.LogWarning(nameof(AppManager) + ":" + nameof(AddNewQuestItem));
        //TODO - Create a new quest SO, serialize it, save it, and add it to the UI layout group.
    }

    public void SetPanelActive(int targetIndex)
    {
        activePanel?.SetActive(false);
        var target = panels[targetIndex];
        activePanel = target;
        target.SetActive(true);
    }
}
