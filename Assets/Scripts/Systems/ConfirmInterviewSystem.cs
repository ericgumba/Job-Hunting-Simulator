using System;

// Interface
public interface IConfirmInterviewSystem
{
    bool ConfirmInterview(string timeLabel, int offsetDays, ApplicationType type);
    int CurrentDate();
    bool ContainsInterviewAt(int day, int hour);
}

public sealed class ConfirmInterviewSystem : IConfirmInterviewSystem
{
    private readonly CurrentTimeDate timeDateTracker;
    private readonly ScheduledInterviews interviewTracker;

    public ScheduledInterviews ScheduledInterviews => interviewTracker;

    public ConfirmInterviewSystem(CurrentTimeDate timeDateTracker, ScheduledInterviews interviewTracker)
    {
        this.timeDateTracker = timeDateTracker;
        this.interviewTracker = interviewTracker;
    }

    public int CurrentDate() {
        return timeDateTracker.Days;
    }

    public bool ConfirmInterview(string timeLabel, int offsetDays, ApplicationType type)
    {
        var hour = int.Parse(timeLabel);
        

        var interviewDate = new ScheduledInterviews.InterviewDate(
            timeDateTracker.Days + offsetDays,
            hour,
            type);
        return interviewTracker.TryAddInterviewDate(interviewDate);
    }

    public bool ContainsInterviewAt(int day, int hour)
    {
        return interviewTracker.ContainsInterviewAt(day, hour);
    }
}
