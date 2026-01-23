using UnityEngine;

public class Mineral : MonoBehaviour
{
    public MineralScriptableObject mineralValues;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    
    [SerializeField] private float _snapSpeed = 10f;
    private bool _isSnapping;
    private Vector3 snapTarget;
    private OmniTool omniTool;

    public GameObject clickIcon;
    public DropZone CurrentZone { get; set; }
    public bool CanBeDragged { get; private set; } = true;
    public bool IsUnderScanner { get; private set; }

    public void Awake()
    {
        omniTool = FindAnyObjectByType<OmniTool>();
    }
    
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
    }

    private void Update()
    {
        HandleDropZoneLogic();
        HandleSnapMovement();
    }

    private void OnMouseDown()
    {
        if (!IsUnderScanner) return;
        omniTool.SelectMineral(this);
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

    public void SetUnderScanner(bool value)
    {
        IsUnderScanner = value;
        CanBeDragged = !value; // Scanner overrides dragging

        if (clickIcon)
            clickIcon.SetActive(value);
    }
}
