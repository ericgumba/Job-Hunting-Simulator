public sealed class ApplyForJobSystem
{
    private readonly ApplicationTracker tracker;
    private readonly PlayerStatistics playerStats;
    private readonly CurrentTimeDate timeDateTracker;

    public ApplyForJobSystem(
        ApplicationTracker tracker, 
        PlayerStatistics playerStats,
        CurrentTimeDate timeDateTracker)
    {
        this.tracker = tracker;
        this.playerStats = playerStats;
        this.timeDateTracker = timeDateTracker;
    }

    public string GetStats()
    {
        return $"totalInterviews: {tracker.TotalPassedResumeSubmissions()} totalRejections: {tracker.TotalFailedResumeSubmissions()}";
    }

    /// <summary>
    /// Executes the "apply for job" action.
    /// Returns true if interview, false if rejection.
    /// </summary>
    public void Apply()
    {
        tracker.RecordResumeSubmission();
    }
}
