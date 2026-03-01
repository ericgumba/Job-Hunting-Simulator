using System;
using UnityEngine;

public sealed class EndOfDaySystem
{
    private readonly CurrentTimeDate currentTimeDate;
    private readonly ApplicationTracker appTracker;
    private readonly PlayerStatistics playerStats;

    public event Action EndOfDayReached;
    public event Action<ApplicationType> notifyPopupCalendar;
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
        EndOfDayReached?.Invoke();
        ProcessOngoingApplications();
    }

    private void ProcessOngoingApplications()
    {
        while (appTracker.TryGetNextOngoingApplicationType(out var type))
        {
            var passed = UnityEngine.Random.value < GetPassChance(type);
            appTracker.PassFailApplication(passed);
            ApplicationOutcome?.Invoke(currentTimeDate.Days, type, passed);
        }
    }

    public void TriggerPopupCalendar(ApplicationType type)
    {
        notifyPopupCalendar?.Invoke(type);
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
                return playerStats.LevelOne;
            case ApplicationType.SecondTechnical:
                return playerStats.LevelTwo;
            case ApplicationType.HiringManager:
                return playerStats.LevelThree;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, "Invalid application type");
        }
    }
}
