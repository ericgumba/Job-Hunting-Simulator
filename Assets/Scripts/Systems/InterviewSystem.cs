using UnityEngine;

public sealed class InterviewSystem
{
    private readonly InterviewTracker interviewTracker;
    private readonly ApplicationTracker applicationTracker;
    private readonly TimeDateTracker timeDateTracker;
    private readonly PlayerStatistics playerStats;
    public InterviewTracker InterviewTracker => interviewTracker;

    public InterviewSystem(
        InterviewTracker interviewTracker,
        ApplicationTracker applicationTracker,
        TimeDateTracker timeDateTracker,
        PlayerStatistics playerStats)
    {
        this.interviewTracker = interviewTracker;
        this.applicationTracker = applicationTracker;
        this.timeDateTracker = timeDateTracker;
        this.playerStats = playerStats;

        timeDateTracker.Changed += TryPerformInterview;
    }

    public void TryPerformInterview()
    {
        var nextInterview = interviewTracker.PeekNextInterviewDate();
        if (!nextInterview.HasValue)
        {
            return;
        }

        var interviewDate = nextInterview.Value;
        if (interviewDate.Day != timeDateTracker.Days ||
            interviewDate.Hour != timeDateTracker.Hours)
        {
            return;
        }

        var passed = Random.value < GetInterviewChance(interviewDate.Lvl);
        applicationTracker.RecordInterviewEvent(passed, interviewDate.Lvl);
        interviewTracker.NotifyTimeChanged(timeDateTracker.Days, timeDateTracker.Hours);
    }

    private float GetInterviewChance(int level)
    {
        switch (level)
        {
            case 1:
                return playerStats.FirstInterviewChance;
            case 2:
                return playerStats.SecondInterviewChance;
            case 3:
                return playerStats.ThirdInterviewChance;
            case 4:
                return playerStats.FinalInterviewChance;
            default:
                Debug.LogWarning($"Unknown interview level {level}. Using base interview chance.");
                return playerStats.InterviewChance;
        }
    }
}
