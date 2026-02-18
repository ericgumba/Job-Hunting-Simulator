using UnityEngine;
using TMPro;

public class UpcomingInterviewController : MonoBehaviour
{
    private ScheduledInterviews interviewTracker;
    protected TMP_Text upcomingInterviewText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Awake()
    {
        upcomingInterviewText = GetComponent<TMP_Text>();
        if (upcomingInterviewText == null)
            Debug.LogError($"{name}: Missing TMP_Text component.");
        Refresh();
    }

    public void Bind(ScheduledInterviews tracker)
    {
        this.interviewTracker = tracker;
        tracker.Changed += Refresh;
        Refresh();
    }
    protected virtual void Refresh()
    {
        if (interviewTracker == null || upcomingInterviewText == null)
            return;

        var nextInterview = interviewTracker.PeekNextInterviewDate();
        if (nextInterview.HasValue)
        {
            upcomingInterviewText.text = $"Upcoming Interview: Day {nextInterview.Value.Day} at {nextInterview.Value.Hour}:00";
        }
        else
        {
            upcomingInterviewText.text = "No Upcoming Interviews";
        }
    }
}