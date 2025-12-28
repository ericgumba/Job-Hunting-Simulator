using UnityEngine;

public sealed class ApplicationsHudView : MonoBehaviour
{
    private TrackerTextViewBase[] textViews;

    private void Awake()
    {
        // Find ALL TrackerTextViewBase components under this HUD (children)
        textViews = GetComponentsInChildren<TrackerTextViewBase>(true);
    }

    public void Bind(ApplicationTracker tracker)
    {
        foreach (var view in textViews)
        {
            view.Bind(tracker);
        }
    }
}
