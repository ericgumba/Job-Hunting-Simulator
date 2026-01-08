
using UnityEngine;

public class PopupCalendarController : MonoBehaviour
{

    private ApplicationTracker tracker;
    private ConfirmInterviewSystem confirmInterviewSystem;

    public void Bind(ApplicationTracker tracker)
    {
        this.tracker = tracker;
        tracker.InterviewRecorded += Show;
    }

    public void Bind(ConfirmInterviewSystem confirmInterviewSystem)
    {
        this.confirmInterviewSystem = confirmInterviewSystem;
        // No-op
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

