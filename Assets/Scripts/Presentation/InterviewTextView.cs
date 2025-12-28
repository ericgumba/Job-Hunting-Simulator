using UnityEngine;

public sealed class InterviewsTextView : TrackerTextViewBase
{
    protected override void Refresh()
    {
        if (Text == null || Tracker == null) return;
        Text.text = $"Interviews: {Tracker.TotalInterviews}";
    }
}
