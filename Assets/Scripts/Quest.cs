using System;

[Serializable]
public class Quest
{
    public Guid Id;
    public int Completions;
    public string Title;
    public Reward[] Rewards;
    public bool IsRepeatable;

    //Cooldown
    public DateTime LastCompleted;
    public TimeSpan CooldownDuration;

    public Quest()
    {
        Id = Guid.NewGuid();
        Completions = 0;
        IsRepeatable = false;
    }

    public bool IsInCooldown()
    {
        return IsRepeatable && DateTime.Now < (LastCompleted + CooldownDuration);
    }

    public TimeSpan GetElapsedCooldown()
    {
        var cooldownEnd = (LastCompleted + CooldownDuration);
        if (cooldownEnd > DateTime.Now)
        {
            return cooldownEnd - DateTime.Now;
        }
        else 
        {
            return TimeSpan.Zero;
        }
    }
}
