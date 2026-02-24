using UnityEngine;

public sealed class FirstTechnicalTextView : TrackerTextViewBase
{
    protected override void Refresh()
    {
        if (Text == null || Tracker == null) return;
        Text.text = $"Stage One: Passed {Tracker.TotalPassedFirstTechnicalInterviews()} Failed {Tracker.TotalFailedFirstTechnicalInterviews()}";
    }
}
