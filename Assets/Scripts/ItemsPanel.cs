using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ItemsPanel : MonoBehaviour
{
    [SerializeField]
    private GameObject consumeTokenButton;

    [SerializeField]
    private Text tokenCounter;

    public void Refresh()
    {
        tokenCounter.text = SaveSystem.Instance.GetRewardCount(RewardType.LuxuryToken).ToString();
        consumeTokenButton.SetActive(SaveSystem.Instance.HasRewards(RewardType.LuxuryToken, 1));
    }

    private void OnEnable()
    {
        Refresh();
    }
}
