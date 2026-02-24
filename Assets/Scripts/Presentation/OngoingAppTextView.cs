using UnityEngine;

public sealed class OngoingAppTextView : TrackerTextViewBase
{
    protected override void Refresh()
    {
        if (Text == null || Tracker == null) return;
        Text.text = $"Ongoing Applications: {Tracker.TotalOngoingResumeSubmissions()}";
    }
}
