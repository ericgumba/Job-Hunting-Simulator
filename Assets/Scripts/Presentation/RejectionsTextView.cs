using UnityEngine;

public sealed class RejectionsTextView : TrackerTextViewBase
{
    protected override void Refresh()
    {
        if (Text == null || Tracker == null) return;
        Text.text = $"Rejections: {Tracker.TotalFailedResumeSubmissions()}";
    }
}
