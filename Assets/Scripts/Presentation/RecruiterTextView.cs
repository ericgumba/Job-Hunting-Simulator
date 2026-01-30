using UnityEngine;

public sealed class RecruiterScreeningTextView : TrackerTextViewBase
{
    protected override void Refresh()
    {
        if (Text == null || Tracker == null) return;
        Text.text = $"Stage One: Passed {Tracker.TotalPassedRecruiterScreenings()} Failed {Tracker.TotalPassedRecruiterScreenings()}";
    }
}