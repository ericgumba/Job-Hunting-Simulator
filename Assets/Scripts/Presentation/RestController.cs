using UnityEngine;
using UnityEngine.UI;

public class RestController : MonoBehaviour
{
    private Button button;
    private RestSystem restSystem;

    void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClicked);
    }

    public void Bind(RestSystem restSystem)
    {
        this.restSystem = restSystem;
    }

    void OnClicked()
    {
        restSystem.Rest();
    }

    void OnDestroy()
    {
        button.onClick.RemoveListener(OnClicked);
    }
}
