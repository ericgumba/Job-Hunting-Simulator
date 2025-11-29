using UnityEngine;
// testing 123
using UnityEngine.UI;
using TMPro;
public class ApplicationTracker : MonoBehaviour
{
    private int totalRejections;
    private int totalInterviews;
    public Button applyButton;
    public TMP_Text rejections;
    public TMP_Text interviews; 
    public GameObject interviewMenuPanel;   // <-- ADD THIS
    public TimeTracker timeTracker;   // <-- ADD THIS
    public PsychologicalState ps;
    private Button[] interviewTimes;              // list of all time buttons (9am - 5pm)

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        applyButton.onClick.AddListener(OnApplyClicked);
        interviewMenuPanel.SetActive(false); // hide menu at start
    
        // Grab ALL buttons under the panel (9am–5pm, or whatever you add)
        interviewTimes = interviewMenuPanel.GetComponentsInChildren<Button>(true);

        // Wire each button dynamically
        foreach (var btn in interviewTimes)
        {
            // Get text from button (e.g., "9 AM", "3 PM")
            TMP_Text label = btn.GetComponentInChildren<TMP_Text>();
            string timeLabel = label.text;

            // Convert that text into minutes (e.g., "3 PM" → 900)
            int minutes = ConvertLabelToMinutes(timeLabel);

            // Add listener that calls ChooseInterviewTime with that minute value
            btn.onClick.AddListener(() => ChooseInterviewTime(minutes));
        }
    }

    void OnApplyClicked()
    {
        calc_results();
    }
    void calc_results()
    {
        float rand = Random.value; 
        float interviewRand = Random.value;
        Debug.Log(interviewRand);
        if (interviewRand < 1.01f)
        {
            totalInterviews++;
            ps.increment_health();
            interviewMenuPanel.SetActive(true);
        }
        else
        {
            totalRejections++;
            ps.decrement_health();
            
        } 
        rejections.text = "Rejections: " + totalRejections;
        interviews.text = "Interviews: " + totalInterviews;
    }

    // Update is called once per frame

    public void ChooseInterviewTime(int minutesFromMidnight)
    {
        string timeStr = timeTracker.CalcTime(minutesFromMidnight);

        Debug.Log($"Interview scheduled for Day {timeTracker.CurrentDay} at {timeStr}");

        // Hide menu after selecting
        interviewMenuPanel.SetActive(false);
    }

    // Converts "9 AM" → 540, "3 PM" → 900, etc.
    int ConvertLabelToMinutes(string label)
    {
        label = label.ToLower().Trim();        // "9 am"
        string[] parts = label.Split(' ');     // ["9", "am"]

        int hour = int.Parse(parts[0]);
        bool pm = parts[1] == "pm";

        if (pm && hour != 12) hour += 12;
        if (!pm && hour == 12) hour = 0;

        return hour * 60 - 480;
    }
    void Update()
    {
        
    }
}
