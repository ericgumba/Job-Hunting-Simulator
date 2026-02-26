using UnityEngine;

public sealed class SecondTechnicalTextView : TrackerTextViewBase
{
    protected override void Refresh()
    {
        if (Text == null || Tracker == null) return;
        Text.text =
            $"Second Technical - Ongoing: {Tracker.TotalOngoingSecondTechnicalInterviews()}, " +
            $"Passed: {Tracker.TotalPassedSecondTechnicalInterviews()}, " +
            $"Failed: {Tracker.TotalFailedSecondTechnicalInterviews()}";
    }
}
