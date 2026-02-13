using TMPro;
using UnityEngine;

public class TooltipManager : MonoBehaviour
{
    public GameObject tooltipPanel;
    public StructureTool structureTool;
    public GameObject objectToFollow;
    public TextMeshProUGUI tooltipText;
    public Vector2 offset;
    
    private RectTransform panelRect;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tooltipPanel.SetActive(false);
        panelRect = tooltipPanel.GetComponent<RectTransform>();
        structureTool = FindAnyObjectByType<StructureTool>();
        objectToFollow = GameObject.FindGameObjectWithTag("Reader");
    }

    void Update()
    {
        if (!structureTool) return;
        if (!objectToFollow) return;
        
        HandlePanel();
        ShowTooltip();
    }

    private void HandlePanel()
    {
        Vector2 tabletReader = Camera.main.WorldToScreenPoint(objectToFollow.transform.position);
            
        Vector2 desiredPos = tabletReader + offset;
            
        // Clamp position to keep tooltip fully visible
        Vector2 panelSize = panelRect.sizeDelta * panelRect.lossyScale; // account for scaling
        float pivotX = panelRect.pivot.x;
        float pivotY = panelRect.pivot.y;

        // Horizontal clamp
        if (desiredPos.x + (1f - pivotX) * panelSize.x > Screen.width)
            desiredPos.x = Screen.width - (1f - pivotX) * panelSize.x;
        if (desiredPos.x - pivotX * panelSize.x < 0)
            desiredPos.x = pivotX * panelSize.x;

        // Vertical clamp
        if (desiredPos.y + (1f - pivotY) * panelSize.y > Screen.height)
            desiredPos.y = Screen.height - (1f - pivotY) * panelSize.y;
        if (desiredPos.y - pivotY * panelSize.y < 0)
            desiredPos.y = pivotY * panelSize.y;

        panelRect.position = desiredPos;
    }

    public void ShowTooltip()
    {
        if (!structureTool || structureTool.mineralsInRange.Count == 0)
        {
            Debug.Log("Mineral not found");
            tooltipPanel.SetActive(false);
            return;
        }
        tooltipPanel.SetActive(true);

        System.Text.StringBuilder sb = new();
        
        foreach (var minerals in structureTool.mineralsInRange)
        {
            var values = minerals.mineralValues;
            int s = values.crystalStructure;
            string minStructure = null;

            if (s == 1) minStructure = "Cubic";
            else if (s == 2) minStructure = "Tetragonal";
            else if (s == 3) minStructure = "Hexagonal";
            else if (s == 4) minStructure = "Rhombohedral";
            else if (s == 5) minStructure = "Orthorhombic";
            else if (s == 6) minStructure = "Monoclinic";
            else if (s == 7) minStructure = "Triclinic";
            
            //sb.AppendLine($"{values.mineralName} | Structure: {minStructure}");
            sb.AppendLine($"Structure: {minStructure}");
        }
        
        tooltipText.text = sb.ToString();
    }
}
