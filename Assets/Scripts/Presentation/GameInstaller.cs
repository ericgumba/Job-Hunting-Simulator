using System.Diagnostics;
using UnityEngine;

public class GameInstaller : MonoBehaviour
{
    [Header("Scene references")]
    [SerializeField] private ApplyButtonController applyButtonController;
    [SerializeField] private ApplicationsHudView applicationsHudView;


    [Header("Config")]
    [SerializeField] private PlayerStatsConfig playerStatsConfig;

    private ApplicationTracker tracker;
    private ApplyForJobSystem applySystem;
    private PlayerStatistics playerStats;

    void Awake()
    {
        // Create the single source of truth
        var cfg = new PlayerStatsConfigData(playerStatsConfig);
        playerStats = new PlayerStatistics(cfg);
        

        tracker = new ApplicationTracker();
        applySystem = new ApplyForJobSystem(tracker, playerStats);
        // Bind UI to the same instances
        applyButtonController.Bind(applySystem);
        applicationsHudView.Bind(tracker);
    }
}
