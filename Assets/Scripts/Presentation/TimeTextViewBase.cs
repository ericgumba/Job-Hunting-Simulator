using TMPro;
using UnityEngine;

public abstract class TimeTextViewBase : MonoBehaviour
{
    protected TMP_Text Text { get; private set; }
    protected TimeDateTracker Tracker { get; private set; }
    protected virtual void Awake()
    {
        Text = GetComponent<TMP_Text>();
        if (Text == null)
            Debug.LogError($"{name}: Missing TMP_Text component.");
        Refresh();
        
    }

    public void Bind(TimeDateTracker tracker)
    {
        // Unbind previous (in case Bind is called again)
        if (Tracker != null)
            Tracker.Changed -= Refresh;

        Tracker = tracker;

        if (Tracker != null)
            Tracker.Changed += Refresh;

        Refresh();
    }
    protected abstract void Refresh();

    // Update is called once per frame
    void Update()
    {
        
    }
}
