using System;

// Interface
public interface IConfirmInterviewSystem
{
    bool ConfirmInterview(string timeLabel, int offsetDays);
    int CurrentDate();
    bool ContainsInterviewAt(int day, int hour);
}

public sealed class ConfirmInterviewSystem : IConfirmInterviewSystem
{
    private readonly CurrentTimeDate timeDateTracker;
    private readonly InterviewTracker interviewTracker;

    public InterviewTracker InterviewTracker => interviewTracker;

    public ConfirmInterviewSystem(CurrentTimeDate timeDateTracker, InterviewTracker interviewTracker)
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

    public bool ContainsInterviewAt(int day, int hour)
    {
        return interviewTracker.ContainsInterviewAt(day, hour);
    }
}
