using TMPro;
using UnityEngine;

public class TooltipManager : MonoBehaviour
{
    #if UNITY_EDITOR
    public GameObject tooltipPanel;
    public TextMeshProUGUI tooltipText;
    
    private RectTransform panelRect;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tooltipPanel.SetActive(false);
        panelRect = tooltipPanel.GetComponent<RectTransform>();
    }

    void Update()
    {
        if (tooltipPanel.activeSelf)
        {
            // Make tooltip follow mouse
            Vector2 mousePos = Input.mousePosition;
            
            Vector2 offset = new Vector2(15f, -15f);
            Vector2 desiredPos = mousePos + offset;
            
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

        // Add stats here
        tooltipText.text = $"{values.mineralName}\n" +
                           "Hardness: " + $"{values.hardness}\n" + 
                           "Luster: " + $"{values.luster}\n" + 
                           "Density: " + $"{values.density}\n";
    }

    public void HideTooltip()
    {
        tooltipPanel.SetActive(false);
    }
    #endif
}
