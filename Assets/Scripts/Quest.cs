using System;

public class Quest
{
    public Guid Id;
    public int Completions;
    public string Title;
    public Reward[] Rewards;
    public bool IsRepeatable;

    public Quest()
    {
        Id = Guid.NewGuid();
        Completions = 0;
        IsRepeatable = false;
    }
}
