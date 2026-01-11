using UnityEngine;

public sealed class PassedLevelFourTextView : TrackerTextViewBase
{
    protected override void Refresh()
    {
        if (Text == null || Tracker == null) return;
        Text.text = $"Stage Four: Passed {Tracker.TotalPassedLvlFourInterviews} Failed {Tracker.TotalFailedLvlFourInterviews}";
    }
}
