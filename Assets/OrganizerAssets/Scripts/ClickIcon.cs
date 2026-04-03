using UnityEngine;

public class ClickIcon : MonoBehaviour
{
    public Sprite unselectedSprite;
    public Sprite selectedSprite;

    private SpriteRenderer sr;
    private Mineral mineral;
    private OmniTool omniTool;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        mineral = GetComponentInParent<Mineral>();
        omniTool = FindFirstObjectByType<OmniTool>();
    }

    private void Update()
    {
        if (!omniTool || !mineral) return;

        bool isSelected = omniTool.CurrentMineral == mineral;
        sr.sprite = isSelected ? selectedSprite : unselectedSprite;
    }
}
