using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppManager : MonoBehaviour
{
    public static AppManager Instance { get; private set; }

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

    public void AddNewQuestItem(string title, RewardType rewardType, int count)
    {
        Debug.LogWarning(nameof(AppManager) + ":" + nameof(AddNewQuestItem));
        //TODO - Create a new quest SO, serialize it, save it, and add it to the UI layout group.
    }
}
