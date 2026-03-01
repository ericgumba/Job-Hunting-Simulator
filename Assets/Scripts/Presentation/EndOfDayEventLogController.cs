using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class EndOfDayEventLogController : MonoBehaviour
{
    [SerializeField] private EventLog eventLog;
    [SerializeField] private GameObject eventLogPanel;
    [SerializeField] private float preRevealSeconds = 1f;
    [SerializeField] private float postRevealSeconds = 10f;
    [SerializeField] private PopupCalendarController popupCalendarController;
    private EndOfDaySystem endOfDaySystem;
    private readonly List<Outcome> pendingOutcomes = new List<Outcome>();
    private Coroutine showRoutine;

    private struct Outcome
    {
        public int Day;
        public ApplicationType Type;
        public bool Passed;
        public string Message;
    }

    public void Bind(EndOfDaySystem system)
    {
        if (endOfDaySystem != null)
        {
            endOfDaySystem.ApplicationOutcome -= OnApplicationOutcome;
            endOfDaySystem.EndOfDayReached -= OnEndOfDayReached;
        }

        endOfDaySystem = system;
        if (endOfDaySystem != null)
        {
            endOfDaySystem.ApplicationOutcome += OnApplicationOutcome;
            endOfDaySystem.EndOfDayReached += OnEndOfDayReached;
        }
    }

    private void OnEndOfDayReached()
    {
        if (showRoutine != null)
        {
            StopCoroutine(showRoutine);
        }
        showRoutine = StartCoroutine(ShowEventLogSequence());
    }

    private void OnApplicationOutcome(int day, ApplicationType type, bool passed)
    {
        var result = passed ? "passed" : "failed";
        pendingOutcomes.Add(new Outcome
        {
            Day = day,
            Type = type,
            Passed = passed,
            Message = $"Day {day}: {FormatType(type)} {result}."
        });
    }

    private static string FormatType(ApplicationType type)
    {
        switch (type)
        {
            case ApplicationType.ResumeSubmission:
                return "Resume submission";
            case ApplicationType.RecruiterScreening:
                return "Recruiter screening";
            case ApplicationType.FirstTechnical:
                return "First technical interview";
            case ApplicationType.SecondTechnical:
                return "Second technical interview";
            case ApplicationType.HiringManager:
                return "Hiring manager interview";
            default:
                return type.ToString();
        }
    }

    private void OnDestroy()
    {
        if (endOfDaySystem != null)
        {
            endOfDaySystem.ApplicationOutcome -= OnApplicationOutcome;
            endOfDaySystem.EndOfDayReached -= OnEndOfDayReached;
        }
    }

    private IEnumerator ShowEventLogSequence()
    {
        if (eventLog == null)
        {
            Debug.LogWarning($"{name}: Missing EventLog reference.");
            yield break;
        }

        var panel = eventLogPanel != null ? eventLogPanel : eventLog.gameObject;
        panel.SetActive(true);
        eventLog.Clear();

        yield return new WaitForSeconds(preRevealSeconds);

        var revealDelay = new WaitForSeconds(0.5f);
        foreach (var outcome in pendingOutcomes)
        {
            Debug.Log($"ERICGUMBA Revealing message: {outcome.Message}");
            eventLog.AddMessage(outcome.Message);
            if (outcome.Passed && endOfDaySystem != null)
            {
                endOfDaySystem.TriggerPopupCalendar(outcome.Type);
                if (popupCalendarController != null)
                    yield return new WaitUntil(() => !popupCalendarController.IsVisible);
            }
            yield return revealDelay;
        }
        pendingOutcomes.Clear();

        yield return new WaitForSeconds(postRevealSeconds);

        panel.SetActive(false);
        showRoutine = null;
    }
}
