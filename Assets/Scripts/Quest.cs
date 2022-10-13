using System;

[Serializable]
public class Quest
{
    public Guid Id;
    public int Completions;
    public string Title;
    public Reward[] Rewards;
    public bool IsRepeatable;
    public bool IsCooldownActive;

    //Cooldown
    public DateTime LastCompleted;
    public TimeSpan CooldownDuration;

    public Quest()
    {
        Id = Guid.NewGuid();
        Completions = 0;
        IsRepeatable = false;
        IsCooldownActive = false;
    }

    public bool HasCooldownElapsed()
    {
        return IsCooldownActive && DateTime.Now < (LastCompleted + CooldownDuration);
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
