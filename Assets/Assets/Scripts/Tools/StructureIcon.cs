using UnityEngine;

public class StructureIcon : MonoBehaviour
{
    public Sprite[] structureSprite;

    private SpriteRenderer sr;
    private Mineral mineral;
    private StructureTool structureTool;
    
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        mineral = GetComponentInParent<Mineral>();
        structureTool = FindFirstObjectByType<StructureTool>();
    }

    private void Update()
    {
        if (!structureTool || !mineral) return;

        int index = mineral.mineralValues.crystalStructure - 1;
        sr.sprite = structureSprite[index];
    }
}