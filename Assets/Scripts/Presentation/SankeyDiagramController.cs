using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public sealed class SankeyDiagramController : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private RectTransform contentParent;
    [SerializeField] private Button closeButton;

    [Header("Layout")]
    [SerializeField] private float rowHeight = 36f;
    [SerializeField] private float labelWidth = 180f;
    [SerializeField] private float maxBarWidth = 320f;
    [SerializeField] private float barHeight = 16f;
    [SerializeField] private float gap = 8f;

    [Header("Colors")]
    [SerializeField] private Color passedColor = new Color(0.2f, 0.7f, 0.3f, 1f);
    [SerializeField] private Color failedColor = new Color(0.8f, 0.2f, 0.2f, 1f);

    private ApplicationTracker tracker;
    private readonly List<GameObject> rows = new List<GameObject>();

    private void Awake()
    {
        if (closeButton != null)
            closeButton.onClick.AddListener(Hide);
        Hide();
    }

    public void Bind(ApplicationTracker applicationTracker)
    {
        tracker = applicationTracker;
    }

    public void Show()
    {
        if (tracker == null)
        {
            Debug.LogWarning($"{name}: Missing ApplicationTracker reference.");
            return;
        }

        var target = panel != null ? panel : gameObject;
        target.SetActive(true);
        Canvas.ForceUpdateCanvases();

        BuildDiagram();
    }

    public void Hide()
    {
        var target = panel != null ? panel : gameObject;
        target.SetActive(false);
    }

    private void BuildDiagram()
    {
        ClearRows();
        if (contentParent == null)
        {
            Debug.LogWarning($"{name}: Missing contentParent reference.");
            return;
        }

        var stages = new[]
        {
            new Stage("Resume Submission",
                tracker.TotalPassedResumeSubmissions(),
                tracker.TotalFailedResumeSubmissions()),
            new Stage("Recruiter Screening",
                tracker.TotalPassedRecruiterScreenings(),
                tracker.TotalFailedRecruiterScreenings()),
            new Stage("First Technical",
                tracker.TotalPassedFirstTechnicalInterviews(),
                tracker.TotalFailedFirstTechnicalInterviews()),
            new Stage("Second Technical",
                tracker.TotalPassedSecondTechnicalInterviews(),
                tracker.TotalFailedSecondTechnicalInterviews()),
            new Stage("Hiring Manager",
                tracker.TotalPassedHiringManagerInterviews(),
                tracker.TotalFailedHiringManagerInterviews())
        };

        int maxTotal = 0;
        foreach (var stage in stages)
        {
            if (stage.Total > maxTotal)
                maxTotal = stage.Total;
        }

        float contentWidth = contentParent.rect.width;
        if (contentWidth <= 0f)
            contentWidth = labelWidth + maxBarWidth + 120f;

        contentParent.sizeDelta = new Vector2(contentParent.sizeDelta.x, stages.Length * rowHeight);

        for (int i = 0; i < stages.Length; i++)
        {
            var row = CreateRow(stages[i], i, maxTotal, contentWidth);
            rows.Add(row);
        }
    }

    private GameObject CreateRow(Stage stage, int index, int maxTotal, float contentWidth)
    {
        var row = new GameObject($"Row_{stage.Name}", typeof(RectTransform));
        row.transform.SetParent(contentParent, false);

        var rowRect = row.GetComponent<RectTransform>();
        rowRect.anchorMin = new Vector2(0f, 1f);
        rowRect.anchorMax = new Vector2(0f, 1f);
        rowRect.pivot = new Vector2(0f, 1f);
        rowRect.sizeDelta = new Vector2(contentWidth, rowHeight);
        rowRect.anchoredPosition = new Vector2(0f, -index * rowHeight);

        var label = CreateText($"{stage.Name}", row.transform);
        var labelRect = label.GetComponent<RectTransform>();
        labelRect.anchorMin = new Vector2(0f, 0.5f);
        labelRect.anchorMax = new Vector2(0f, 0.5f);
        labelRect.pivot = new Vector2(0f, 0.5f);
        labelRect.sizeDelta = new Vector2(labelWidth, rowHeight);
        labelRect.anchoredPosition = new Vector2(0f, 0f);

        var barContainer = new GameObject("BarContainer", typeof(RectTransform));
        barContainer.transform.SetParent(row.transform, false);
        var barRect = barContainer.GetComponent<RectTransform>();
        barRect.anchorMin = new Vector2(0f, 0.5f);
        barRect.anchorMax = new Vector2(0f, 0.5f);
        barRect.pivot = new Vector2(0f, 0.5f);
        barRect.sizeDelta = new Vector2(maxBarWidth, barHeight);
        barRect.anchoredPosition = new Vector2(labelWidth + gap, 0f);

        float totalWidth = maxTotal > 0 ? maxBarWidth * (stage.Total / (float)maxTotal) : 0f;
        float passedWidth = stage.Total > 0 ? totalWidth * (stage.Passed / (float)stage.Total) : 0f;
        float failedWidth = totalWidth - passedWidth;

        if (passedWidth > 0f)
            CreateBarSegment("Passed", barContainer.transform, passedWidth, passedColor, 0f);
        if (failedWidth > 0f)
            CreateBarSegment("Failed", barContainer.transform, failedWidth, failedColor, passedWidth);

        var counts = CreateText($"{stage.Passed} / {stage.Failed}", row.transform);
        var countsRect = counts.GetComponent<RectTransform>();
        countsRect.anchorMin = new Vector2(0f, 0.5f);
        countsRect.anchorMax = new Vector2(0f, 0.5f);
        countsRect.pivot = new Vector2(0f, 0.5f);
        countsRect.sizeDelta = new Vector2(120f, rowHeight);
        countsRect.anchoredPosition = new Vector2(labelWidth + gap + maxBarWidth + gap, 0f);

        return row;
    }

    private void CreateBarSegment(string name, Transform parent, float width, Color color, float xOffset)
    {
        var segment = new GameObject(name, typeof(RectTransform), typeof(Image));
        segment.transform.SetParent(parent, false);
        var rect = segment.GetComponent<RectTransform>();
        rect.anchorMin = new Vector2(0f, 0.5f);
        rect.anchorMax = new Vector2(0f, 0.5f);
        rect.pivot = new Vector2(0f, 0.5f);
        rect.sizeDelta = new Vector2(width, barHeight);
        rect.anchoredPosition = new Vector2(xOffset, 0f);

        var image = segment.GetComponent<Image>();
        image.color = color;
    }

    private TMP_Text CreateText(string text, Transform parent)
    {
        var go = new GameObject("Text", typeof(RectTransform), typeof(TextMeshProUGUI));
        go.transform.SetParent(parent, false);
        var tmp = go.GetComponent<TextMeshProUGUI>();
        tmp.text = text;
        tmp.fontSize = 18;
        tmp.alignment = TextAlignmentOptions.MidlineLeft;
        tmp.font = TMP_Settings.defaultFontAsset;
        return tmp;
    }

    private void ClearRows()
    {
        for (int i = 0; i < rows.Count; i++)
        {
            if (rows[i] != null)
                Destroy(rows[i]);
        }
        rows.Clear();
    }

    private struct Stage
    {
        public string Name;
        public int Passed;
        public int Failed;
        public int Total => Passed + Failed;

        public Stage(string name, int passed, int failed)
        {
            Name = name;
            Passed = passed;
            Failed = failed;
        }
    }
}
