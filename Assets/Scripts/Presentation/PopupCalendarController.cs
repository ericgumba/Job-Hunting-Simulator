using UnityEngine;
using UnityEngine.UI;

public class PopupCalendarController : MonoBehaviour
{

    private ApplicationTracker tracker;
    private ConfirmInterviewSystem confirmInterviewSystem;
    [SerializeField] private Button incrementButton;
    [SerializeField] private Button decrementButton;
    [SerializeField] private Button[] timeButtons;
    [SerializeField] private int dayOffset = 1;
    [SerializeField] private TMPro.TMP_Text Text;

    private void Awake()
    {
        if (incrementButton != null)
        {
            Debug.Log("Adding listener to increment button");
            incrementButton.onClick.AddListener(IncrementDay);
        }
        if (decrementButton != null)
        {
            Debug.Log("Adding listener to decrement button");
            decrementButton.onClick.AddListener(DecrementDay);
        }

        if (timeButtons != null)
        {
            foreach (var button in timeButtons)
            {
                if (button == null)
                    continue;
                var cachedButton = button;
                cachedButton.onClick.AddListener(() => OnTimeButtonClicked(cachedButton));
            }
        }

        UpdateButtonStates();
        UpdateDayText();
    }

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

    private void IncrementDay()
    {
        Debug.Log("Incrementing day");
        dayOffset++;
        UpdateDayText();
        UpdateButtonStates();
    }

    private void DecrementDay()
    {
        Debug.Log("Decrementing day");
        dayOffset--;
        UpdateButtonStates();
        UpdateDayText();
    }

    private void OnTimeButtonClicked(Button button)
    {
        Debug.Log($"Clicked time button: {button.name}");
        if (confirmInterviewSystem != null && tracker != null)
        {
            Debug.Log($"Attempting to confirm interview... {dayOffset}");
            bool confirmed = confirmInterviewSystem.ConfirmInterview(button.name, dayOffset);
            if (confirmed)
            {
                Debug.Log("Interview confirmed!");
                gameObject.SetActive(false);
            }
            else
            {
                Debug.Log("Failed to confirm interview.");
            }
        }
    }

    private void UpdateButtonStates()
    {
        if (decrementButton != null)
            decrementButton.interactable = dayOffset > 1;
        if (incrementButton != null)
            incrementButton.interactable = dayOffset <= 14;
    }

    void OnDestroy()
    {
        if (tracker != null)
            tracker.InterviewRecorded -= Show;
    }
    private void UpdateDayText()
    {
        if (Text != null)
        {
            Text.text = $"(Day {dayOffset + confirmInterviewSystem.CurrentDate()})";
        }
    }

}