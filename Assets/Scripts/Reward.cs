using System.Collections.Generic;
using UnityEngine;

public enum RewardType
{
    LuxuryToken = 0
}

[CreateAssetMenu(fileName = "Reward", menuName = "ScriptableObjects/Reward", order = 1)]
public class Reward : ScriptableObject
{
    public RewardType Type;
    public int Count;

    //TODO: Use HTML notations to signify any required text effects or spacing.
    public static readonly Dictionary<RewardType, string> Descriptions = new Dictionary<RewardType, string>
    {
        { 
            RewardType.LuxuryToken, 
            "A token that describes a set amount of leisure time or limited activities. Each token is counted as 30 minutes of leisure time." 
        },
    };
}
