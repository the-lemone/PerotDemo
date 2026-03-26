using TMPro;
using UnityEditor.TerrainTools;
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
        Vector2 toolBase = objectToFollow.transform.position;
        Vector2 desiredPos = toolBase + offset;
        panelRect.position = desiredPos;
    }

    public void ShowTooltip()
    {
        if (!structureTool || structureTool.mineralsInRange.Count == 0)
        {
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
            
            sb.AppendLine($"Mineral: {values.mineralName} | Structure: {minStructure}");
            //sb.AppendLine($"Structure: {minStructure}");
        }
        
        tooltipText.text = sb.ToString();
    }

    public void ScannerActive()
    {
        structureTool = FindAnyObjectByType<StructureTool>();
    }
}
