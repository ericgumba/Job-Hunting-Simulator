using UnityEngine;
using UnityEngine.UI;

public class ApplyButtonController : MonoBehaviour
{
    private Button button;

    // Domain + system
    private ApplicationTracker tracker;
    private ApplyForJobSystem applySystem;


    void Awake()
    {
        button = GetComponent<Button>();
 
        button.onClick.AddListener(OnClicked);
    }

    public void Bind(ApplyForJobSystem applySystem_)
    {
        applySystem = applySystem_;
    }

    void OnClicked()
    {
        bool gotInterview = applySystem.Apply();
        Debug.Log(applySystem.GetStats());
        // Later:
        // - show popup
        // - update HUD
        // - modify morale
    }

    void OnDestroy()
    {
        button.onClick.RemoveListener(OnClicked);
    }
}
