using System;

public sealed class PlayerStatistics
{
    private readonly PlayerStatsConfigData config;

    public int Experience { get; private set; }
    public int Morale { get; private set; }

    public event Action Changed;

    public float ResumeSubmission => config.ResumeSubmission;
    public float RecruiterScreening => config.RecruiterScreening;
    public float FirstTechnical => config.FirstTechnical;
    public float SecondTechnical => config.SecondTechnical;
    public float HiringManager => config.HiringManager;
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
/// 
public readonly struct PlayerStatsConfigData
{
    public readonly float ResumeSubmission, RecruiterScreening, FirstTechnical, SecondTechnical, HiringManager;
    public readonly int startingMorale, maxMorale, moraleStep;

    public PlayerStatsConfigData(PlayerStatsConfig cfg)
    {
        ResumeSubmission = cfg.ResumeSubmission;
        RecruiterScreening = cfg.RecruiterScreening;
        FirstTechnical = cfg.FirstTechnical;
        SecondTechnical = cfg.SecondTechnical;
        HiringManager = cfg.HiringManager;

        startingMorale = cfg.startingMorale;
        maxMorale = cfg.maxMorale;
        moraleStep = cfg.moraleStep;
    }
}
