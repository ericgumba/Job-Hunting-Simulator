using UnityEngine;

public sealed class PassedLevelOneTextView : TrackerTextViewBase
{
    protected override void Refresh()
    {
        if (Text == null || Tracker == null) return;
        Text.text = $"Stage One: Passed {Tracker.TotalPassedLvlOneInterviews} Failed {Tracker.TotalFailedLvlOneInterviews}";
    }
}
