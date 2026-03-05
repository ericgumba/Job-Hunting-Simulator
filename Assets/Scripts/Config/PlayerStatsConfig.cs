// using UnityEngine;

// [CreateAssetMenu(fileName = "PlayerStatsConfig", menuName = "Scriptable Objects/PlayerStatsConfig")]
// public class PlayerStatsConfig : ScriptableObject
// {
    
// }
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Player Stats Config", fileName = "PlayerStatsConfig")]
public sealed class PlayerStatsConfig : ScriptableObject
{
    [Header("Interview chances")]

        // ResumeSubmission,
        // RecruiterScreening,
        // FirstTechnical,
        // SecondTechnical,
        // HiringManager
    [Range(0f, 1f)] public float ResumeSubmission = 0.01f;
    [Range(0f, 1f)] public float RecruiterScreening = 0.05f;
    [Range(0f, 1f)] public float FirstTechnical = 0.05f;
    [Range(0f, 1f)] public float SecondTechnical = 0.05f;
    [Range(0f, 1f)] public float HiringManager = 0.10f;
    [Header("Morale")]
    public int startingMorale = 100;
    public int maxMorale = 100;
    public int moraleStep = 10;
}
