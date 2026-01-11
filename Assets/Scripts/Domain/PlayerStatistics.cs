using System;

public sealed class PlayerStatistics
{
    private readonly PlayerStatsConfigData config;

    public int Experience { get; private set; }
    public int Morale { get; private set; }

    public event Action Changed;

    public float InterviewChance => config.interviewChance;
    public float FirstInterviewChance => config.firstInterviewChance;
    public float SecondInterviewChance => config.secondInterviewChance;
    public float ThirdInterviewChance => config.thirdInterviewChance;
    public float FinalInterviewChance => config.finalInterviewChance;

    public int MaxMorale => config.maxMorale;
    public int MoraleStep => config.moraleStep;

    public PlayerStatistics()
    {
        // default config values
        this.config = new PlayerStatsConfigData(new PlayerStatsConfig());
        Morale = config.startingMorale;
        Experience = 0;
    }
    public PlayerStatistics(PlayerStatsConfigData config)
    {
        this.config = config;
        Morale = config.startingMorale;
        Experience = 0;
    }

    public void AddExperience(int amount)
    {
        if (amount <= 0) return;
        Experience += amount;
        Changed?.Invoke();
    }

    public void ChangeMorale(int delta)
    {
        if (delta == 0) return;
        Morale = Clamp(Morale + delta, 0, config.maxMorale);
        Changed?.Invoke();
    }

    private static int Clamp(int v, int min, int max)
        => v < min ? min : (v > max ? max : v);
}

/// <summary>
/// Plain data copy of ScriptableObject values.
/// This keeps PlayerStatistics Unity-free.
/// </summary>
public readonly struct PlayerStatsConfigData
{
    public readonly float interviewChance, firstInterviewChance, secondInterviewChance, thirdInterviewChance, finalInterviewChance;
    public readonly int startingMorale, maxMorale, moraleStep;

    public PlayerStatsConfigData(PlayerStatsConfig cfg)
    {
        interviewChance = cfg.interviewChance;
        firstInterviewChance = cfg.firstInterviewChance;
        secondInterviewChance = cfg.secondInterviewChance;
        thirdInterviewChance = cfg.thirdInterviewChance;
        finalInterviewChance = cfg.finalInterviewChance;

        startingMorale = cfg.startingMorale;
        maxMorale = cfg.maxMorale;
        moraleStep = cfg.moraleStep;
    }
}
