using UnityEngine;

public sealed class DateTextView : TimeTextViewBase
{
    protected override void Refresh()
    {
        if (Text == null || Tracker == null) return;
        Text.text = $"Day {Tracker.Days}";
    }
}