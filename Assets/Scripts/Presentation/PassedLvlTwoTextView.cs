using UnityEngine;

public sealed class PassedLevelTwoTextView : TrackerTextViewBase
{
    protected override void Refresh()
    {
        if (Text == null || Tracker == null) return;
        Text.text = $"Stage Two: Passed {Tracker.TotalPassedLvlTwoInterviews} Failed {Tracker.TotalFailedLvlTwoInterviews}";
    }
}
