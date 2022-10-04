using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateQuestPanel : MonoBehaviour
{
    [SerializeField]
    private InputField titleInput;

    [SerializeField]
    private Dropdown rewardDropdown;

    [SerializeField]
    private InputField rewardCountInput;

    //UI Elements to show
    [SerializeField]
    private GameObject backButton;

    [SerializeField]
    private GameObject addNewQuestButton;

    [SerializeField]
    private GameObject questItemsGroup;

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

        titleInput.text = string.Empty;
        rewardCountInput.text = string.Empty;
    }
}
