using System;

public sealed class ConfirmInterviewSystem
{
    private readonly TimeDateTracker timeDateTracker;
    private readonly InterviewTracker interviewTracker;

    public InterviewTracker InterviewTracker => interviewTracker;

    public ConfirmInterviewSystem(TimeDateTracker timeDateTracker, InterviewTracker interviewTracker)
    {
        this.timeDateTracker = timeDateTracker;
        this.interviewTracker = interviewTracker;
    }

    public int CurrentDate() {
        return timeDateTracker.Days;
    }

    public bool ConfirmInterview(string timeLabel, int offsetDays)
    {
        var hour = int.Parse(timeLabel);

        var interviewDate = new InterviewTracker.InterviewDate(
            timeDateTracker.Days + offsetDays,
            hour);
        return interviewTracker.TryAddInterviewDate(interviewDate);
    }
}
