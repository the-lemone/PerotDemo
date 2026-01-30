using TMPro;
using UnityEngine;

public class TooltipManager : MonoBehaviour
{
    public GameObject tooltipPanel;
    public OmniTool omniTool;
    public GameObject objectToFollow;
    public TextMeshProUGUI tooltipText;
    public Vector2 offset;
    
    private RectTransform panelRect;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tooltipPanel.SetActive(false);
        panelRect = tooltipPanel.GetComponent<RectTransform>();
        omniTool = FindAnyObjectByType<OmniTool>();
        objectToFollow = GameObject.FindGameObjectWithTag("Reader");
    }

    void Update()
    {
        if (!omniTool) return;
        if (!objectToFollow) return;
        
        if (!omniTool.CurrentMineral)
        {
            tooltipPanel.SetActive(false);
            return;
        }
        
        ShowTooltip(omniTool.CurrentMineral);
        
        if(tooltipPanel.activeSelf)
            HandlePanel();
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

    public void ShowTooltip(Mineral mineral)
    {
        if (!mineral || !mineral.mineralValues)
        {
            Debug.Log("Mineral not found");
            return;
        }

        tooltipPanel.SetActive(true);
        var values = mineral.mineralValues;
        string minColor = null;

        if (values.color == 0) minColor = "White";
        else if (values.color == 1) minColor = "Red";
        else if (values.color == 2) minColor = "Orange";
        else if (values.color == 3) minColor = "Yellow";
        else if (values.color == 4) minColor = "Green";
        else if (values.color == 5) minColor = "Blue";
        else if (values.color == 6) minColor = "Indigo";
        else if (values.color == 7) minColor = "Violet";

        // Add stats here
        tooltipText.text = $"{values.mineralName}\n" +
                           "Hardness: " + $"{values.hardness}\n" + 
                           "Structure: " + $"{values.crystalStructure}\n" + 
                           "Color: " + $"{minColor}\n";
    }
}
