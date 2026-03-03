using TMPro;
using UnityEngine;
using UnityEngine.UI;

public sealed class SankeyButtonController : MonoBehaviour
{
    [SerializeField] private SankeyDiagramController sankeyDiagramController; 
    private ApplicationTracker tracker;
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        if (button != null)
            button.onClick.AddListener(OnClicked); 
    }

    public void Bind(ApplicationTracker applicationTracker)
    {
        tracker = applicationTracker;
    }

    private void OnClicked()
    { 
        if (sankeyDiagramController == null)
        {
            Debug.LogWarning($"{name}: Missing SankeyDiagramController reference.");
            return;
        }

        sankeyDiagramController.Bind(tracker);
        sankeyDiagramController.Show();
    }
}
