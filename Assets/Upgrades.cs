using UnityEngine;

using TMPro;
using UnityEngine.UI;
public class Upgrades : MonoBehaviour
{
    private int interviewLevel = 1;
    private int firstInterviewLevel = 1;
    private int secondInterviewLevel = 1;
    private int thirdInterviewLevel = 1;
    private int finalInterviewLevel = 1;
    private int applicationCountLevel = 1; 
    private int maxMoraleLevel = 1;
    private int moraleStepLevel = 1;

    public Button upgradeInterviewButton;
    public Button statsButton;
    public GameObject statsPanel;

    [SerializeField] private PlayerStatistics ps;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        upgradeInterviewButton.onClick.AddListener(IncreaseInterviewChance);
        statsButton.onClick.AddListener(() => {
            statsPanel.SetActive(!statsPanel.activeSelf);
        });
        statsPanel.SetActive(false);

        // upgradeInterviewButton on hover highlight color change
    }

    void IncreaseMaxMorale()
    {
        if (ps.Experience >= maxMoraleLevel * 10)
        {
            ps.AddExperience(-maxMoraleLevel * 10);
            maxMoraleLevel += 1; 
            ps.MaxMorale += 10; 
        } else {
            Debug.Log("Not enough experience to upgrade max morale.");
        }
        // Implementation for increasing max morale
    }

    void IncreaseFirstInterviewChance()
    {
        if (ps.Experience >= firstInterviewLevel * 10)
        {
            ps.AddExperience(-firstInterviewLevel * 10);
            firstInterviewLevel += 1; 
            // Assuming you have a way to set first interview chance
            ps.FirstInterviewChance = 0.05f * firstInterviewLevel; 
        } else {
            Debug.Log("Not enough experience to upgrade first interview chance.");
        }
        // Implementation for increasing first interview chance
    }
    void IncreaseSecondInterviewChance()
    {
        if (ps.Experience >= secondInterviewLevel * 10)
        {
            ps.AddExperience(-secondInterviewLevel * 10);
            secondInterviewLevel += 1; 
            ps.SecondInterviewChance = 0.05f * secondInterviewLevel; 
        } else {
            Debug.Log("Not enough experience to upgrade second interview chance.");
        }
        // Implementation for increasing second interview chance
    }
    void IncreaseThirdInterviewChance()
    {
        if (ps.Experience >= thirdInterviewLevel * 10)
        {
            ps.AddExperience(-thirdInterviewLevel * 10);
            thirdInterviewLevel += 1; 
            ps.ThirdInterviewChance = 0.05f * thirdInterviewLevel; 
        } else {
            Debug.Log("Not enough experience to upgrade third interview chance.");
        }
        // Implementation for increasing third interview chance
    }
    void IncreaseFinalInterviewChance()
    {
        if (ps.Experience >= finalInterviewLevel * 10)
        {
            ps.AddExperience(-finalInterviewLevel * 10);
            finalInterviewLevel += 1; 
            ps.FinalInterviewChance = 0.1f * finalInterviewLevel; 
        } else {
            Debug.Log("Not enough experience to upgrade final interview chance.");
        }
        // Implementation for increasing final interview chance
    }

    void DecreaseMoraleStep()
    {
        if (ps.Experience >= moraleStepLevel * 10)
        {
            ps.AddExperience(-moraleStepLevel * 10);
            moraleStepLevel += 1; 
            ps.MoraleStep = Mathf.Max(1, ps.MoraleStep - 1); 
        } else {
            Debug.Log("Not enough experience to upgrade morale step.");
        }
        // Implementation for decreasing morale step
    } 

    void IncreaseInterviewChance()
    {
        if (ps.Experience >= interviewLevel * 10)
        {
            ps.AddExperience(-interviewLevel * 10);
            interviewLevel += 1; 
            ps.InterviewChance = 0.01f * interviewLevel; 
        } else {
            Debug.Log("Not enough experience to upgrade interview chance.");
        }
        statsPanel.SetActive(false);
    }
}
