using UnityEngine;


// This system manages the scheduling and execution of interviews based on the current time and player statistics.
public sealed class InterviewSystem
{
    private readonly ScheduledInterviews interviewTracker;
    private readonly ApplicationTracker applicationTracker;
    private readonly CurrentTimeDate timeDateTracker;
    private readonly PlayerStatistics playerStats;
    public ScheduledInterviews ScheduledInterviews => interviewTracker;
    public event System.Action<ApplicationType> InterviewTriggered;
    private bool interviewInProgress;

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
        if (interviewInProgress)
            return;

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

        applicationTracker.RecordInterviewEvent(interviewDate.Type);
        playerStats.AddExperience(GetInterviewXp(interviewDate.Type));
        InterviewTriggered?.Invoke(interviewDate.Type);
        interviewTracker.NotifyTimeChanged(timeDateTracker.Days, timeDateTracker.Hours);
        interviewInProgress = true;
    }

    public void CompleteInterview()
    {
        if (!interviewInProgress)
            return;
        interviewInProgress = false;
        timeDateTracker.AdvanceTime(); // Advance time after interview completes
    }

    private static int GetInterviewXp(ApplicationType type)
    {
        switch (type)
        {
            case ApplicationType.RecruiterScreening:
                return 3;
            case ApplicationType.FirstTechnical:
                return 6;
            case ApplicationType.SecondTechnical:
                return 9;
            case ApplicationType.HiringManager:
                return 12;
            default:
                return 0;
        }
    }

    private float GetInterviewChance(int level)
    {
        switch (level)
        {
            case 1:
                return playerStats.RecruiterScreening;
            case 2:
                return playerStats.FirstTechnical;
            case 3:
                return playerStats.SecondTechnical;
            case 4:
                return playerStats.HiringManager;
            default:
                Debug.LogWarning($"Unknown interview level {level}. Using base interview chance.");
                return 1;
        }
    }
}
