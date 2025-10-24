using UnityEngine;

public class Mineral : MonoBehaviour
{
    private static readonly int isShining = Animator.StringToHash("isShining");
    public MineralScriptableObject mineralValues;
    private Animator animator;

    #if UNITY_EDITOR
    private TooltipManager tooltipManager;
    #endif
    
    private void Start()
    {
        if (mineralValues != null)
        {
            GetComponent<SpriteRenderer>().sprite = mineralValues.mineralSprite;
            animator = GetComponent<Animator>();
            gameObject.name = mineralValues.mineralName;
            
            if (mineralValues.animatorController != null)
                animator.runtimeAnimatorController = mineralValues.animatorController;
        }
        
        animator = GetComponent<Animator>();
        
        #if UNITY_EDITOR
        tooltipManager = FindAnyObjectByType<TooltipManager>();
        #endif
    }

    public void SetShining(bool state)
    {
        if (animator)
            animator.SetBool(isShining, state);
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

    private void OnMouseDown()
    {
        if (ToolManager.Instance != null && ToolManager.Instance.HasActiveTool)
        {
            ToolManager.Instance.UseTool(this);
        }
        else
        {
            // Normal drag handling will only run if no tool is selected
        }
    }
}
