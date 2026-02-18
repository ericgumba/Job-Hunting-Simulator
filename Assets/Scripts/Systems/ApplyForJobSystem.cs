public sealed class ApplyForJobSystem
{
    private readonly ApplicationTracker tracker;
    private readonly PlayerStatistics playerStats;
    private readonly CurrentTimeDate currentTime;

    public ApplyForJobSystem(
        ApplicationTracker tracker, 
        PlayerStatistics playerStats,
        CurrentTimeDate currentTime)
    {
        this.tracker = tracker;
        this.playerStats = playerStats;
        this.currentTime = currentTime;
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
