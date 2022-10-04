using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsumeTokenButton : MonoBehaviour
{
    [SerializeField]
    private ItemsPanel itemsPanel;

    public void OnClick()
    {
        //Consume item
        SaveSystem.Instance.ConsumeReward(RewardType.LuxuryToken, 1);
        Debug.Log("Token consumed.");

        //Refresh counter
        itemsPanel.Refresh();
    }
}
