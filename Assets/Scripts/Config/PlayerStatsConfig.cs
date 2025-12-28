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
    [Range(0f, 1f)] public float interviewChance = 0.01f;
    [Range(0f, 1f)] public float firstInterviewChance = 0.05f;
    [Range(0f, 1f)] public float secondInterviewChance = 0.05f;
    [Range(0f, 1f)] public float thirdInterviewChance = 0.05f;
    [Range(0f, 1f)] public float finalInterviewChance = 0.10f;

    [Header("Morale")]
    public int startingMorale = 100;
    public int maxMorale = 100;
    public int moraleStep = 10;
}
