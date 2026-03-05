using UnityEngine;
using UnityEngine.UI;
using TMPro;

public sealed class InterviewPopupController : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private Button optionAButton;
    [SerializeField] private Button optionBButton;
    [SerializeField] private Button optionCButton;
    [SerializeField] private Button optionDButton;
    [SerializeField] private TMP_Text optionALabel;
    [SerializeField] private TMP_Text optionBLabel;
    [SerializeField] private TMP_Text optionCLabel;
    [SerializeField] private TMP_Text optionDLabel;

    private InterviewSystem interviewSystem;

    private void Awake()
    {
        if (optionAButton != null)
            optionAButton.onClick.AddListener(OnOptionA);
        if (optionBButton != null)
            optionBButton.onClick.AddListener(OnOptionB);
        if (optionCButton != null)
            optionCButton.onClick.AddListener(OnOptionC);
        if (optionDButton != null)
            optionDButton.onClick.AddListener(OnOptionD);

        if (optionALabel != null)
            optionALabel.text = "A";
        if (optionBLabel != null)
            optionBLabel.text = "B";
        if (optionCLabel != null)
            optionCLabel.text = "C";
        if (optionDLabel != null)
            optionDLabel.text = "D";

        Hide();
    }

    public void Bind(InterviewSystem system)
    {
        if (interviewSystem != null)
            interviewSystem.InterviewTriggered -= OnInterviewTriggered;

        interviewSystem = system;

        if (interviewSystem != null)
            interviewSystem.InterviewTriggered += OnInterviewTriggered;
    }

    private void OnInterviewTriggered(ApplicationType type)
    {
        Show();
    }

    private void OnOptionA()
    {
        CompleteInterview();
    }

    private void OnOptionB()
    {
        CompleteInterview();
    }

    private void OnOptionC()
    {
        CompleteInterview();
    }

    private void OnOptionD()
    {
        CompleteInterview();
    }

    private void Show()
    {
        var target = panel != null ? panel : gameObject;
        ActivateHierarchy(target.transform);
        var rect = target.GetComponent<RectTransform>();
        if (rect != null)
            rect.SetAsLastSibling();

        var group = target.GetComponent<CanvasGroup>();
        if (group != null)
        {
            group.alpha = 1f;
            group.interactable = true;
            group.blocksRaycasts = true;
        }
    }

    private void Hide()
    {
        var target = panel != null ? panel : gameObject;
        target.SetActive(false);
    }

    private static void ActivateHierarchy(Transform transform)
    {
        var current = transform;
        while (current != null)
        {
            if (!current.gameObject.activeSelf)
                current.gameObject.SetActive(true);
            current = current.parent;
        }
    }

    private void CompleteInterview()
    {
        if (interviewSystem != null)
            interviewSystem.CompleteInterview();
        Hide();
    }

    private void OnDestroy()
    {
        if (interviewSystem != null)
            interviewSystem.InterviewTriggered -= OnInterviewTriggered;
    }
}
