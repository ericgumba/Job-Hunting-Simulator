using UnityEngine;

using TMPro;
using UnityEngine.UI;
public class Upgrades : MonoBehaviour
{
    private int interviewLevel = 1;
    private int applicationCountLevel = 1;
    private int moraleLevel = 1;
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
