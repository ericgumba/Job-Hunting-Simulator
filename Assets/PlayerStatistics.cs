using UnityEngine;

using TMPro;

public class PlayerStatistics : MonoBehaviour
{
    private float interviewChance = .005f;
    private int startingMorale = 100;
    private int moraleStep = 10;
    private int exp = 0;

    public int Experience => exp;

    public TMP_Text exp_label; 

    void Start()
    {
        update_xp();
    }
    void update_xp()
    {
        exp_label.text = "XP: " + exp;
    }
    public void AddExperience(int amount)
    {
        exp += amount;
        update_xp();
    }

    public float InterviewChance => interviewChance;
    public int StartingMorale => startingMorale;
    public int MoraleStep => moraleStep;
}
