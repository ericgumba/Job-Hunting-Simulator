public sealed class ApplyForJobSystem
{
    private readonly ApplicationTracker tracker;
    private readonly CurrentTimeDate currentTime;
    private readonly PlayerStatistics playerStats;

    public ApplyForJobSystem(
        ApplicationTracker tracker,
        CurrentTimeDate currentTime,
        PlayerStatistics playerStats)
    {
        this.tracker = tracker;
        this.currentTime = currentTime;
        this.playerStats = playerStats;
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
        playerStats.AddExperience(1);
        currentTime.AdvanceTime();
    }
}
