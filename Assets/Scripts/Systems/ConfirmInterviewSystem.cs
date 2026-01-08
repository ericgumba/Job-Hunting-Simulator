using System;

public sealed class ConfirmInterviewSystem
{
    private readonly TimeDateTracker timeDateTracker;
    private readonly InterviewTracker interviewTracker;

    public InterviewTracker InterviewTracker => interviewTracker;

    public ConfirmInterviewSystem(TimeDateTracker timeDateTracker)
    {
        this.timeDateTracker = timeDateTracker;
        interviewTracker = new InterviewTracker(timeDateTracker);
    }

    public bool ConfirmInterview(string timeLabel, int offsetDays)
    {
        if (!TryParseHour(timeLabel, out int hour))
        {
            return false;
        }

        var interviewDate = new InterviewTracker.InterviewDate(
            timeDateTracker.Days + offsetDays,
            hour);
        return interviewTracker.TryAddInterviewDate(interviewDate);
    }

    private static bool TryParseHour(string timeLabel, out int hour)
    {
        hour = 0;
        if (string.IsNullOrWhiteSpace(timeLabel))
        {
            return false;
        }

        var parts = timeLabel.Trim().Split(' ');
        if (parts.Length != 2)
        {
            return false;
        }

        if (!int.TryParse(parts[0], out int hour12))
        {
            return false;
        }

        if (hour12 < 1 || hour12 > 12)
        {
            return false;
        }

        var meridiem = parts[1].Trim().ToUpperInvariant();
        if (meridiem != "AM" && meridiem != "PM")
        {
            return false;
        }

        if (hour12 == 12)
        {
            hour12 = 0;
        }

        hour = meridiem == "AM" ? hour12 : hour12 + 12;
        return true;
    }
}
