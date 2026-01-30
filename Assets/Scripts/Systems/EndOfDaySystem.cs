using System;
using UnityEngine;

public sealed class EndOfDaySystem
{
    private readonly TimeDateTracker timeDateTracker;
    private readonly ApplicationTracker appTracker;
    private readonly PlayerStatistics playerStats;

    public EndOfDaySystem(
        TimeDateTracker timeDateTracker,
        ApplicationTracker appTracker,
        PlayerStatistics playerStats)
    {
        this.timeDateTracker = timeDateTracker;
        this.appTracker = appTracker;
        this.playerStats = playerStats;
        this.timeDateTracker.EndOfDayReached += OnEndOfDayReached;
    }

    private void OnEndOfDayReached()
    {
        Debug.Log("End of Day Reached. Advancing to next day.");
        ProcessOngoingApplications();
    }

    private void ProcessOngoingApplications()
    {
        while (appTracker.TryGetNextOngoingApplicationType(out var type))
        {
            var passed = UnityEngine.Random.value < GetPassChance(type);
            appTracker.ApplyNextOngoingResult(passed);
        }
    }

    private float GetPassChance(ApplicationTracker.ApplicationType type)
    {
        switch (type)
        {
            case ApplicationTracker.ApplicationType.ResumeSubmission:
                return playerStats.ResumeSubmission;
            case ApplicationTracker.ApplicationType.RecruiterScreening:
                return playerStats.RecruiterScreening;
            case ApplicationTracker.ApplicationType.LevelOne:
                return playerStats.LevelOne;
            case ApplicationTracker.ApplicationType.LevelTwo:
                return playerStats.LevelTwo;
            case ApplicationTracker.ApplicationType.LevelThree:
                return playerStats.LevelThree;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, "Invalid application type");
        }
    }
}
