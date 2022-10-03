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
        gameObject.SetActive(false);

        titleInput.text = string.Empty;
        rewardCountInput.text = string.Empty;

        AppManager.Instance.AddNewQuestItem(titleInput.text, RewardType.LuxuryToken, count);
    }
}
