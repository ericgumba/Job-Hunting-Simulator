using UnityEngine;

public sealed class ApplicationsHudView : MonoBehaviour
{
    private TrackerTextViewBase[] textViews;

    private void Awake()
    {
        CacheChildren();
    }

    private void CacheChildren()
    {
        textViews = GetComponentsInChildren<TrackerTextViewBase>(true);
    }

    public void Bind(ApplicationTracker tracker)
    {
        if (textViews == null || textViews.Length == 0)
            CacheChildren();
        foreach (var view in textViews)
        {
            view.Bind(tracker);
        }
    }
}
