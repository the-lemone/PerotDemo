using UnityEngine;

public class Mineral : MonoBehaviour
{
    public MineralScriptableObject mineralValues;

    #if UNITY_EDITOR
    private TooltipManager tooltipManager;
    #endif
    
    private void Start()
    {
        if (mineralValues != null)
        {
            GetComponent<SpriteRenderer>().sprite = mineralValues.mineralSprite;
            gameObject.name = mineralValues.mineralName;
        }
        
        #if UNITY_EDITOR
        tooltipManager = FindAnyObjectByType<TooltipManager>();
        #endif
    }

    #if UNITY_EDITOR
    private void OnMouseEnter()
    {
        tooltipManager.ShowTooltip(this);
    }

    private void OnMouseExit()
    {
        tooltipManager.HideTooltip();
    }
    #endif
}
