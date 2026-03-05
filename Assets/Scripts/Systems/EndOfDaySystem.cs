using System;
using UnityEngine;

public sealed class EndOfDaySystem
{
    private readonly CurrentTimeDate currentTimeDate;
    private readonly ApplicationTracker appTracker;
    private readonly PlayerStatistics playerStats;
    private bool debug = false;

    public event Action EndOfDayReached;
    public event Action<int, ApplicationType, bool> ApplicationOutcome;

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
        EndOfDayReached?.Invoke();
    }

    private void ProcessOngoingApplications()
    {
        while (appTracker.TryGetNextOngoingApplicationType(out var type))
        {
            var passed = UnityEngine.Random.value < GetPassChance(type);
            if (debug) passed = true;
            appTracker.PassFailApplication(passed);
            ApplicationOutcome?.Invoke(currentTimeDate.Days, type, passed);
        }
    }


    private float GetPassChance(ApplicationType type)
    {
        switch (type)
        {
            case ApplicationType.ResumeSubmission:
                return playerStats.ResumeSubmission;
            case ApplicationType.RecruiterScreening:
                return playerStats.RecruiterScreening;
            case ApplicationType.FirstTechnical:
                return playerStats.FirstTechnical;
            case ApplicationType.SecondTechnical:
                return playerStats.SecondTechnical;
            case ApplicationType.HiringManager:
                return playerStats.HiringManager;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, "Invalid application type");
        }
    }
}
