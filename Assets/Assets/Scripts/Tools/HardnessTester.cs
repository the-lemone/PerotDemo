using System;
using UnityEngine;

public class HardnessTester : ToolBase
{
    public HardnessTool[] referenceTools;
    public HardnessTool currentReferenceTool;
    private Camera mainCam;
    private Vector3 mousePos;
    
    [Header("Button References")]
    public HardnessButton[] buttons;  // You will assign these in Inspector
    
    [Header("Visuals")]
    public GameObject scratchEffectPrefab; // Particle/overlay
    public GameObject noScratchEffectPrefab; // Maybe a spark or nothing happens

    private void Awake()
    {
        mainCam = Camera.main;
    }
    
    public void Start()
    {
        // Default: select the first reference tool
        if (referenceTools.Length > 0)
        {
            SelectReferenceTool(0);
        }
    }

    public void Update()
    {
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;
    }

    public override void Use(Mineral target)
    {
        if (!target || !target.mineralValues) return;

        if (currentReferenceTool == null)
            return;
        
        float mineralHardness = target.mineralValues.hardness;
        bool scratched = currentReferenceTool.hardnessValue > mineralHardness;
        
        // Spawn visual feedback
        GameObject effectPrefab = scratched ? scratchEffectPrefab : noScratchEffectPrefab;
        if (effectPrefab != null)
        {
            Instantiate(effectPrefab, mousePos, Quaternion.identity);
            Debug.Log($"{currentReferenceTool.toolName} {(scratched ? "scratches" : "does not scratch")} {target.mineralValues.mineralName}");
        }
    }
    
    public void SelectReferenceTool(int index)
    {
        if (index >= 0 && index < referenceTools.Length)
        {
            currentReferenceTool = referenceTools[index];
            
            Debug.Log("Selected reference tool: " + currentReferenceTool.toolName);
        }
    }

    public void UpdateAllSprites()
    {
        foreach (var button in buttons)
        {
            if (button != null)
                button.UpdateSprite();
        }
    }
}

[Serializable]
public class HardnessTool
{
    public string toolName;
    public float hardnessValue; // 1-10 (Mohs scale)
    public Sprite toolSprite; // For UI and maybe world representation
}
