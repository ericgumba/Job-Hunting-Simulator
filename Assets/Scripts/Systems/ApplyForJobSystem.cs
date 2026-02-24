public sealed class ApplyForJobSystem
{
    private readonly ApplicationTracker tracker;
    private readonly CurrentTimeDate currentTime;

    public ApplyForJobSystem(
        ApplicationTracker tracker,
        CurrentTimeDate currentTime)
    {
        this.tracker = tracker;
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
        currentTime.AdvanceTime();
    }
}
