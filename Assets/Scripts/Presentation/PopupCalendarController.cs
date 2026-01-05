
using UnityEngine;

public class PopupCalendarController : MonoBehaviour
{

    private ApplicationTracker tracker;

    public void Bind(ApplicationTracker tracker)
    {
        this.tracker = tracker;
        tracker.InterviewRecorded += Show;
    }

    private void Show()
    {
        Debug.Log("Showing popup calendar");
        gameObject.SetActive(true);
    }

    void OnDestroy()
    {
        if (tracker != null)
            tracker.InterviewRecorded -= Show;
    }
}

