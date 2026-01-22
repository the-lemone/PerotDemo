using UnityEngine;

public class Mineral : MonoBehaviour
{
    private static readonly int isShining = Animator.StringToHash("isShining");
    public MineralScriptableObject mineralValues;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    
    [SerializeField] private float _snapSpeed = 10f;
    private bool _isSnapping;
    private Vector3 snapTarget;
    
    public DropZone CurrentZone { get; set; }
    
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

    private void Update()
    {
        HandleDropZoneLogic();
        HandleSnapMovement();
    }

    private void HandleSnapMovement()
    {
        if (!_isSnapping) return;

        // Stop snapping when close enough
        if (Vector3.Distance(transform.position, snapTarget) < 0.01f)
        {
            transform.position = snapTarget;
            _isSnapping = false;
        }
        
        transform.position = Vector3.Lerp(transform.position, snapTarget, _snapSpeed * Time.deltaTime);
    }
    
    private void HandleDropZoneLogic()
    {
        if (!CurrentZone)
        {
            snapTarget = transform.position;
            return;
        }
        
        // Otherwise keep snapping back into place
        LerpTo(CurrentZone.transform.position);
        
    }
    
    public void LerpTo(Vector3 target)
    {
        snapTarget = target;
        _isSnapping = true;
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

    public void AssignMineral(MineralScriptableObject newValues)
    {
        mineralValues = newValues;
        
        CurrentZone = null;
        transform.SetParent(null);
        
        // Update animator if needed
        if (animator == null)
            animator = GetComponent<Animator>();
        
        if (mineralValues.animatorController)
            animator.runtimeAnimatorController = mineralValues.animatorController;
        else if (!mineralValues.animatorController)
            animator.runtimeAnimatorController = null;
        
        // Update sprite + name
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();

        spriteRenderer.sprite = mineralValues.mineralSprite;
        spriteRenderer.color = mineralValues.spriteColor;
        gameObject.name = mineralValues.mineralName;
    }
}
