using System.Collections;
using UnityEngine;

public class HardnessButton : MonoBehaviour
{
    [Header("Tool")]
    public HardnessTester hardnessTester;
    public int referenceIndex;

    [Header("Sprites")]
    [SerializeField] private Sprite activeSprite;
    [SerializeField] private Sprite inactiveSprite;

    private SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        UpdateSprite();
    }
    
    private void OnMouseDown()
    {
        StartCoroutine(ToggleNextFrame());
    }

    private IEnumerator ToggleNextFrame()
    {
        yield return null; // wait 1 frame

        if (hardnessTester == null) yield break;
        
        if (hardnessTester.currentReferenceTool ==
            hardnessTester.referenceTools[referenceIndex])
        {
            // Reset to index 0
            hardnessTester.SelectReferenceTool(0);
        }
        else
        {
            // Select this tool
            hardnessTester.SelectReferenceTool(referenceIndex);
        }
        
        hardnessTester.UpdateAllSprites();
    }
    
    public void UpdateSprite()
    {
        if (sr == null || hardnessTester == null) return;

        bool isActive = hardnessTester.currentReferenceTool == 
                        hardnessTester.referenceTools[referenceIndex];

        Debug.Log(isActive + " " + gameObject.name);
        sr.sprite = isActive ? activeSprite : inactiveSprite;
    }
}
