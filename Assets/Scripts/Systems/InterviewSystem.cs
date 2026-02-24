using UnityEngine;


// This system manages the scheduling and execution of interviews based on the current time and player statistics.
public sealed class InterviewSystem
{
    private readonly ScheduledInterviews interviewTracker;
    private readonly ApplicationTracker applicationTracker;
    private readonly CurrentTimeDate timeDateTracker;
    private readonly PlayerStatistics playerStats;
    public ScheduledInterviews ScheduledInterviews => interviewTracker;

    public InterviewSystem(
        ScheduledInterviews interviewTracker,
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
