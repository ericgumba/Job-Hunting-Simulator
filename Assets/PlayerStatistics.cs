using UnityEngine;

public class PlayerStatistics : MonoBehaviour
{
    private float interviewChance = .005f;
    private int startingMorale = 100;
    private int moraleStep = 10;
    private int exp = 0;

    public int Experience
    {
        get => exp;
        set => exp = value;
    }

    public void AddExperience(int amount)
    {
        exp += amount;
    }

    public float InterviewChance => interviewChance;
    public int StartingMorale => startingMorale;
    public int MoraleStep => moraleStep;
}
