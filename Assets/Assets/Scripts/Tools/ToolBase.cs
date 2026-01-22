using UnityEngine;

public abstract class ToolBase : MonoBehaviour
{
    // Called when tool is selected from UI
    public virtual void OnSelect()
    {
        gameObject.SetActive(true);
    }
    
    // Called when the tool is deselected
    public virtual void OnDeselect()
    {
        gameObject.SetActive(false);
    }
    
    // Called when using the tool on a mineral
    public abstract void Use(Mineral target);
}
