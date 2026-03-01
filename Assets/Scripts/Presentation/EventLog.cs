using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EventLog : MonoBehaviour
{
    public Transform contentParent;   // Drag Content here
    public GameObject textPrefab;     // Drag EventLogText prefab
    public ScrollRect scrollRect;

    public void AddMessage(string message)
    {
        GameObject newText = Instantiate(textPrefab, contentParent);
        newText.GetComponent<TMP_Text>().text = message;

        Canvas.ForceUpdateCanvases();
        scrollRect.verticalNormalizedPosition = 0f; // Auto-scroll to bottom
    }

    public void Clear()
    {
        for (int i = contentParent.childCount - 1; i >= 0; i--)
        {
            Destroy(contentParent.GetChild(i).gameObject);
        }
    }
}
