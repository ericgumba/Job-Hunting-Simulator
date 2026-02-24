using System;
using UnityEngine;

public sealed class EndOfDaySystem
{
    private readonly CurrentTimeDate currentTimeDate;
    private readonly ApplicationTracker appTracker;
    private readonly PlayerStatistics playerStats;

    public EndOfDaySystem(
        CurrentTimeDate currentTimeDate,
        ApplicationTracker appTracker,
        PlayerStatistics playerStats)
    {
        this.currentTimeDate = currentTimeDate;
        this.appTracker = appTracker;
        this.playerStats = playerStats;
        this.currentTimeDate.EndOfDayReached += OnEndOfDayReached;
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
            case ApplicationTracker.ApplicationType.FirstTechnical:
                return playerStats.LevelOne;
            case ApplicationTracker.ApplicationType.SecondTechnical:
                return playerStats.LevelTwo;
            case ApplicationTracker.ApplicationType.HiringManager:
                return playerStats.LevelThree;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, "Invalid application type");
        }
    }
}
