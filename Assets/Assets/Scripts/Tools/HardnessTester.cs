using System;
using UnityEngine;

public class HardnessTester : ToolBase
{
    public HardnessTool[] referenceTools;
    public HardnessTool currentReferenceTool;
    
    [Header("Visuals")]
    public GameObject scratchEffectPrefab; // Particle/overlay
    public GameObject noScratchEffectPrefab; // Maybe a spark or nothing happens
    private ToolBase _toolBaseImplementation;

    public void Start()
    {
        currentReferenceTool = null;
    }

    public override void Use(Mineral target)
    {
        if (!target || !target.mineralValues) return;

        float mineralHardness = target.mineralValues.hardness;

        if (currentReferenceTool == null)
        {
            Debug.Log("No hardness tool selected!");
            return;
        }
        
        bool scratched = currentReferenceTool.hardnessValue > mineralHardness;
        
        // Spawn visual feedback
        GameObject effectPrefab = scratched ? scratchEffectPrefab : noScratchEffectPrefab;
        if (effectPrefab != null)
        {
            Instantiate(effectPrefab, target.transform.position, Quaternion.identity, target.transform);
        }
        
        Debug.Log($"{currentReferenceTool.toolName} {(scratched ? "scratches" : "does not scratch")} {target.mineralValues.mineralName}");
    }
    
    public void SelectReferenceTool(int index)
    {
        if (index >= 0 && index < referenceTools.Length)
        {
            currentReferenceTool = referenceTools[index];
            Debug.Log("Selected reference tool: " + currentReferenceTool.toolName);
        }
    }
}

[System.Serializable]
public class HardnessTool
{
    public string toolName;
    public float hardnessValue; // 1-10 (Mohs scale)
    public Sprite toolSprite; // For UI and maybe world representation
}
