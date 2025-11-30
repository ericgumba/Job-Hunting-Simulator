using UnityEngine;

public class PlayerStatistics : MonoBehaviour
{
    private float interviewChance = .005f;
    private int startingMorale = 100;
    private int moraleStep = 10;

    public float InterviewChance => interviewChance;
    public int StartingMorale => startingMorale;
    public int MoraleStep => moraleStep;
}
