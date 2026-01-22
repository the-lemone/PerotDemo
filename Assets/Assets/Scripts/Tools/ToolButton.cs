using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FlashlightButton : MonoBehaviour
{
    [Header("Tool")]
    public ToolBase targetObject;
    public ToolManager toolManager;

    [Header("Sprites")]
    [SerializeField] private Sprite activeSprite;
    [SerializeField] private Sprite inactiveSprite;

    private SpriteRenderer sr;

    private bool isActive;

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

        if (targetObject == null) yield break;

        isActive = !isActive;
        toolManager.ToggleTool(targetObject);
        UpdateSprite();
    }
    
    private void UpdateSprite()
    {
        if (sr == null) return;
        sr.sprite = isActive ? activeSprite : inactiveSprite;
    }
}
