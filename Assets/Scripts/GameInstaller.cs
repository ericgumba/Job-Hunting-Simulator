using System.Diagnostics;
using UnityEngine;

public class GameInstaller : MonoBehaviour
{
    [Header("Scene references")]
    [SerializeField] private ApplyButtonController applyButtonController;
    [SerializeField] private RestController restController;
    [SerializeField] private ApplicationsHudView applicationsHudView;
    [SerializeField] private TimeDateHudView timeDateHudView;
    [SerializeField] private PopupCalendarController popupCalendarController;
    [SerializeField] private UpcomingInterviewController upcomingInterviewController;

    [Header("Config")]
    [SerializeField] private PlayerStatsConfig playerStatsConfig;

    private ApplicationTracker tracker;
    private ApplyForJobSystem applySystem;
    private RestSystem restSystem;
    private IConfirmInterviewSystem confirmInterviewSystem;
    private InterviewSystem interviewSystem;
    private PlayerStatistics playerStats;
    private CurrentTimeDate timeDateTracker;
    private ScheduledInterviews interviewTracker;
    private EndOfDaySystem endOfDaySystem;

    void Awake()
    {
        // Create the single source of truth
        var cfg = new PlayerStatsConfigData(playerStatsConfig);

        // Domain
        playerStats = new PlayerStatistics(cfg);
        timeDateTracker = new CurrentTimeDate();
        tracker = new ApplicationTracker();
        interviewTracker = new ScheduledInterviews();
        tracker.Bind(interviewTracker);

        // Systems
        applySystem = new ApplyForJobSystem(tracker, timeDateTracker);
        restSystem = new RestSystem(timeDateTracker);
        confirmInterviewSystem = new ConfirmInterviewSystem(timeDateTracker, interviewTracker);
        interviewSystem = new InterviewSystem(interviewTracker, tracker, timeDateTracker, playerStats);
        endOfDaySystem = new EndOfDaySystem(timeDateTracker, tracker, playerStats);

        // Presentation
        applyButtonController.Bind(applySystem);
        restController.Bind(restSystem);
        applicationsHudView.Bind(tracker);
        timeDateHudView.Bind(timeDateTracker);
        popupCalendarController.Bind(endOfDaySystem);
        popupCalendarController.Bind(confirmInterviewSystem);
        upcomingInterviewController.Bind(interviewTracker);

    }
}
