using System;
using UnityEngine;
using UnityEngine.UI;

public class PopupCalendarController : MonoBehaviour
{

    private IConfirmInterviewSystem confirmInterviewSystem;
    private ApplicationType currentApplicationType;
    public bool IsVisible => gameObject.activeSelf;
    public event Action Closed;
    [SerializeField] private Button incrementButton;
    [SerializeField] private Button decrementButton;
    [SerializeField] private Button[] timeButtons;
    private int dayOffset = 1;
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

    public void Bind(IConfirmInterviewSystem confirmInterviewSystem)
    {
        this.confirmInterviewSystem = confirmInterviewSystem;
        // No-op
    }

    public void ShowForType(ApplicationType type)
    {
        Debug.Log("Showing popup calendar");
        currentApplicationType = type;
        gameObject.SetActive(true);
        UpdateDayText();
        UpdateButtonStates();
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
        if (confirmInterviewSystem != null)
        {
            Debug.Log($"Attempting to confirm interview... {dayOffset}");
            bool confirmed = confirmInterviewSystem.ConfirmInterview(button.name, dayOffset, currentApplicationType);
            if (confirmed)
            {
                Debug.Log("Interview confirmed!");
                gameObject.SetActive(false);
                Closed?.Invoke();
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

        foreach (var button in timeButtons) {
            if (button == null)
                continue;

            var timeLabel = button.name;
            int hour = int.Parse(timeLabel);

            if (confirmInterviewSystem != null)
            {
                bool canSchedule = !confirmInterviewSystem.ContainsInterviewAt(confirmInterviewSystem.CurrentDate() + dayOffset, hour);
                button.interactable = canSchedule;
            }
        }
    }

    void OnDestroy()
    {
        // No-op
    }

    private void UpdateDayText()
    {
        if (Text != null && confirmInterviewSystem != null)
        {
            Text.text = $"(Day {dayOffset + confirmInterviewSystem.CurrentDate()})";
        }
    }
}
