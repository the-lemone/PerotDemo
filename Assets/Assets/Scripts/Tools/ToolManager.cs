using System.Collections.Generic;
using UnityEngine;

public class ToolManager : MonoBehaviour
{
    public static ToolManager Instance { get; private set; }

    private ToolBase activeTool;
    public bool HasActiveTool => activeTool != null;

    void Awake()
    {
        if (Instance && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
    }

    public void ToggleTool(ToolBase tool)
    {
        // Deselect old tool
        if(activeTool == tool)
        {
            // Clear hardness reference if it was active
            if (tool is HardnessTester)
            {
                var hardnessTester = tool as HardnessTester;
                hardnessTester.currentReferenceTool = null;
            }
            
            tool.OnDeselect();
            activeTool = null;
            
            ToggleDragging(true);
            return;
        }
        
        // Disable the old one
        if (activeTool != null)
        {
            activeTool.gameObject.SetActive(false);
            activeTool.OnDeselect();
        }
        
        // Enable new one
        activeTool = tool;
        tool.OnSelect();

        ToggleDragging(false);
    }

    public void TogglePanel(GameObject panel)
    {
        if(panel.activeSelf)
            panel.SetActive(false);
        else
            panel.SetActive(true);
    }
    
    public void UseTool(Mineral mineral)
    {
        if (activeTool)
            activeTool.Use(mineral);
    }

    private void ToggleDragging(bool toggled)
    {
        foreach (var drag in FindObjectsByType<Drag>(0))
        {
            drag.enabled = toggled;
        }
    }
}
