using UnityEngine;

public sealed class RecruiterScreeningTextView : TrackerTextViewBase
{
    protected override void Refresh()
    {
        if (Text == null || Tracker == null) return;
        Text.text =
            $"Recruiter Screening - Ongoing: {Tracker.TotalOngoingRecruiterScreenings()}, " +
            $"Passed: {Tracker.TotalPassedRecruiterScreenings()}, " +
            $"Failed: {Tracker.TotalFailedRecruiterScreenings()}";
    }
}
