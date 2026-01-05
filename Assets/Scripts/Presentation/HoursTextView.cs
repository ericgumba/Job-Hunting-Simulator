using UnityEngine;

public sealed class HoursTextView : TimeTextViewBase
{
    protected override void Refresh()
    {
        if (Text == null || Tracker == null) return;
        Text.text = $"Time: {Tracker.CurrentTime}";
    }
}