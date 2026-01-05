public sealed class ApplyForJobSystem
{
    private readonly ApplicationTracker tracker;
    private readonly PlayerStatistics playerStats;
    private readonly TimeDateTracker timeDateTracker;

    public ApplyForJobSystem(
        ApplicationTracker tracker, 
        PlayerStatistics playerStats,
        TimeDateTracker timeDateTracker)
    {
        this.tracker = tracker;
        this.playerStats = playerStats;
        this.timeDateTracker = timeDateTracker;
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
        // bool gotInterview = UnityEngine.Random.value < playerStats.InterviewChance;
        bool gotInterview = UnityEngine.Random.value < 0.1f; // TEMPORARY   
        
        if (gotInterview)
            tracker.RecordInterview();
        else
            tracker.RecordRejection();
        
        timeDateTracker.AdvanceTime();

        return gotInterview;
    }
}
