using UnityEngine;

public sealed class LvlThreeTextView : TrackerTextViewBase
{
    protected override void Refresh()
    {
        if (Text == null || Tracker == null) return;
        Text.text = $"Stage Three: Passed {Tracker.TotalPassedLvlThreeInterviews()} Failed {Tracker.TotalFailedLvlThreeInterviews()}";
    }
}
