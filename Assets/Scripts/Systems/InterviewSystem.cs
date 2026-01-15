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
        Debug.Log("InterviewSystem initialized.");

        timeDateTracker.Changed += TryPerformInterview;
    }

    public void TryPerformInterview()
    {
        Debug.Log("Checking for interviews to perform...");
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
        timeDateTracker.AdvanceTime(); // Advance time after interview
        Log_Results();
    }

    void Log_Results()
    {
        Debug.Log($"Total Interviews: {applicationTracker.TotalInterviews} " +
                  $"Passed Lvl 1: {applicationTracker.TotalPassedLvlOneInterviews} " +
                  $"Failed Lvl 1: {applicationTracker.TotalFailedLvlOneInterviews} " +
                  $"Passed Lvl 2: {applicationTracker.TotalPassedLvlTwoInterviews} " +
                  $"Failed Lvl 2: {applicationTracker.TotalFailedLvlTwoInterviews} " +
                  $"Passed Lvl 3: {applicationTracker.TotalPassedLvlThreeInterviews} " +
                  $"Failed Lvl 3: {applicationTracker.TotalFailedLvlThreeInterviews} " +
                  $"Passed Lvl 4: {applicationTracker.TotalPassedLvlFourInterviews} " +
                  $"Failed Lvl 4: {applicationTracker.TotalFailedLvlFourInterviews} ");
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
