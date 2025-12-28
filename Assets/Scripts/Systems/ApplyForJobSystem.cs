public sealed class ApplyForJobSystem
{
    private readonly ApplicationTracker tracker;
    private readonly PlayerStatistics playerStats;

    public ApplyForJobSystem(
        ApplicationTracker tracker, PlayerStatistics playerStats)
    {
        this.tracker = tracker;
        this.playerStats = playerStats;
    }

    public string GetStats()
    {
        return $"totalInterviews: {tracker.TotalInterviews} totalRejections: {tracker.TotalRejections}";
    }

    /// <summary>
    /// Executes the "apply for job" action.
    /// Returns true if interview, false if rejection.
    /// </summary>
    public bool Apply()
    {
        bool gotInterview = UnityEngine.Random.value < playerStats.InterviewChance;
        
        if (gotInterview)
            tracker.RecordInterview();
        else
            tracker.RecordRejection();

        return gotInterview;
    }
}
