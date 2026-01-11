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
    private PlayerStatistics playerStats;
    private TimeDateTracker timeDateTracker;
    private InterviewTracker interviewTracker;

    void Awake()
    {
        // Create the single source of truth
        var cfg = new PlayerStatsConfigData(playerStatsConfig);

        // Domain
        playerStats = new PlayerStatistics(cfg);
        timeDateTracker = new TimeDateTracker();
        tracker = new ApplicationTracker();
        interviewTracker = new InterviewTracker();

        // Systems
        applySystem = new ApplyForJobSystem(tracker, playerStats, timeDateTracker);
        restSystem = new RestSystem(timeDateTracker);
        confirmInterviewSystem = new ConfirmInterviewSystem(timeDateTracker, interviewTracker);
        // Bind UI to the same instances

        // Presentation
        applyButtonController.Bind(applySystem);
        restController.Bind(restSystem);
        applicationsHudView.Bind(tracker);
        timeDateHudView.Bind(timeDateTracker);
        popupCalendarController.Bind(tracker);
        popupCalendarController.Bind(confirmInterviewSystem);
        upcomingInterviewController.Bind(interviewTracker);

        
    }
}
