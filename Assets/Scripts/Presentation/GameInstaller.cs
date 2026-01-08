using System.Diagnostics;
using UnityEngine;

public class GameInstaller : MonoBehaviour
{
    [Header("Scene references")]
    [SerializeField] private ApplyButtonController applyButtonController;
    [SerializeField] private ApplicationsHudView applicationsHudView;
    [SerializeField] private TimeDateHudView timeDateHudView;
    [SerializeField] private PopupCalendarController popupCalendarController;

    [Header("Config")]
    [SerializeField] private PlayerStatsConfig playerStatsConfig;

    private ApplicationTracker tracker;
    private ApplyForJobSystem applySystem;
    private ConfirmInterviewSystem confirmInterviewSystem;
    private PlayerStatistics playerStats;
    private TimeDateTracker timeDateTracker;

    void Awake()
    {
        // Create the single source of truth
        var cfg = new PlayerStatsConfigData(playerStatsConfig);

        // Domain
        playerStats = new PlayerStatistics(cfg);
        timeDateTracker = new TimeDateTracker();
        tracker = new ApplicationTracker();

        // Systems
        applySystem = new ApplyForJobSystem(tracker, playerStats, timeDateTracker);
        confirmInterviewSystem = new ConfirmInterviewSystem(timeDateTracker);
        // Bind UI to the same instances

        // Presentation
        applyButtonController.Bind(applySystem);
        applicationsHudView.Bind(tracker);
        timeDateHudView.Bind(timeDateTracker);
        popupCalendarController.Bind(tracker);
        popupCalendarController.Bind(confirmInterviewSystem);

        
    }
}
