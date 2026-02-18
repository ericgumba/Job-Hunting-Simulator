using UnityEngine;

public sealed class InterviewSystem
{
    private readonly InterviewTracker interviewTracker;
    private readonly ApplicationTracker applicationTracker;
    private readonly CurrentTimeDate timeDateTracker;
    private readonly PlayerStatistics playerStats;
    public InterviewTracker InterviewTracker => interviewTracker;

    public InterviewSystem(
        InterviewTracker interviewTracker,
        ApplicationTracker applicationTracker,
        CurrentTimeDate timeDateTracker,
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

        applicationTracker.RecordInterviewEvent(interviewDate.Lvl);
        interviewTracker.NotifyTimeChanged(timeDateTracker.Days, timeDateTracker.Hours);
        timeDateTracker.AdvanceTime(); // Advance time after interview 
    }

    void Log_Results()
    {
        Debug.Log($"Total Interviews: {applicationTracker.TotalApplications()} " +
                  $"Passed Lvl 1: {applicationTracker.TotalPassedRecruiterScreenings()} " +
                  $"Failed Lvl 1: {applicationTracker.TotalFailedRecruiterScreenings()} " +
                  $"Passed Lvl 2: {applicationTracker.TotalPassedLvlOneInterviews()} " +
                  $"Failed Lvl 2: {applicationTracker.TotalFailedLvlOneInterviews()} " +
                  $"Passed Lvl 3: {applicationTracker.TotalPassedLvlTwoInterviews()} " +
                  $"Failed Lvl 3: {applicationTracker.TotalFailedLvlTwoInterviews()} " +
                  $"Passed Lvl 4: {applicationTracker.TotalPassedLvlThreeInterviews()} " +
                  $"Failed Lvl 4: {applicationTracker.TotalFailedLvlThreeInterviews()} ");
    }

    private float GetInterviewChance(int level)
    {
        switch (level)
        {
            case 1:
                return playerStats.RecruiterScreening;
            case 2:
                return playerStats.LevelOne;
            case 3:
                return playerStats.LevelTwo;
            case 4:
                return playerStats.LevelThree;
            default:
                Debug.LogWarning($"Unknown interview level {level}. Using base interview chance.");
                return 1;
        }
    }
}
