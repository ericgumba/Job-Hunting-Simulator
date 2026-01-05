using UnityEngine;

public class TimeDateHudView : MonoBehaviour
{
    private TimeTextViewBase[] textViews;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        // Find ALL TimeTextViewBase components under this HUD (children)
        textViews = GetComponentsInChildren<TimeTextViewBase>(true);
    }

    public void Bind(TimeDateTracker tracker)
    {
        foreach (var view in textViews)
        {
            view.Bind(tracker);
        }
    }
}
