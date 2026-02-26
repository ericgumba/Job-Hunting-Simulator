using UnityEngine;

public sealed class HiringManagerTextView : TrackerTextViewBase
{
    protected override void Refresh()
    {
        if (Text == null || Tracker == null) return;
        Text.text =
            $"Hiring Manager - Ongoing: {Tracker.TotalOngoingHiringManagerInterviews()}, " +
            $"Passed: {Tracker.TotalPassedHiringManagerInterviews()}, " +
            $"Failed: {Tracker.TotalFailedHiringManagerInterviews()}";
    }
}
