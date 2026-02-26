using UnityEngine;

public sealed class FirstTechnicalTextView : TrackerTextViewBase
{
    protected override void Refresh()
    {
        if (Text == null || Tracker == null) return;
        Text.text =
            $"First Technical - Ongoing: {Tracker.TotalOngoingFirstTechnicalInterviews()}, " +
            $"Passed: {Tracker.TotalPassedFirstTechnicalInterviews()}, " +
            $"Failed: {Tracker.TotalFailedFirstTechnicalInterviews()}";
    }
}
