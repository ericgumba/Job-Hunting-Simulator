using UnityEngine;

public sealed class ResumeSubmissionsTextView : TrackerTextViewBase
{
    protected override void Refresh()
    {
        if (Text == null || Tracker == null) return;
        Text.text =
            $"Resume Submissions - Ongoing: {Tracker.TotalOngoingResumeSubmissions()}, " +
            $"Passed: {Tracker.TotalPassedResumeSubmissions()}, " +
            $"Failed: {Tracker.TotalFailedResumeSubmissions()}";
    }
}
