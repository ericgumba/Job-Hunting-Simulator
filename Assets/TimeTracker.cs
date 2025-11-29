
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TimeTracker : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public Button applyButton;
    public TMP_Text clock;    // or use Text if you're using the old UI
    private int totalMinutes = 0;
    public TMP_Text date; 
    private int totalDays = 0;
    // Expose read-only properties so other scripts can use the time
    public int CurrentMinutes => totalMinutes;
    public int CurrentDay => totalDays;
    public string CurrentTimeString => CalcTime(totalMinutes);
    void Start()
    {
        applyButton.onClick.AddListener(OnApplyClicked);

    }
    public string CalcTime(int mins)
    {
        int startTime = 480;                 // 8:00 AM in minutes
        int totalMinutes = startTime + mins;

        // Wrap around 24h if needed
        totalMinutes %= 1440;

        int hour = totalMinutes / 60;
        int minute = totalMinutes % 60;

        string suffix = hour >= 12 ? "pm" : "am";

        // Convert to 12-hour format
        int hour12 = hour % 12;
        if (hour12 == 0) hour12 = 12;

        return $"{hour12}:{minute:D2}{suffix}";
    }

    void OnApplyClicked()
    {
        totalMinutes += 60;
        string currentTime = CalcTime(totalMinutes);
        clock.text = currentTime;
        if (CalcTime(totalMinutes) == "12:00am") { nextDay(); }
    }

    void nextDay()
    {
        totalMinutes = 0;
        totalDays++;
        date.text = $"Day {totalDays}";
        clock.text = CalcTime(totalMinutes);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
