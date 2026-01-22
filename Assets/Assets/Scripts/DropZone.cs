using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DropZone : MonoBehaviour
{
    [HideInInspector] public Mineral currentMineral;
    [SerializeField] private float exitDistance; // tune this as needed
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        Mineral mineral = other.GetComponent<Mineral>();
        if (mineral != null)
        {
            currentMineral = mineral;
            
            mineral.CurrentZone = this;
            mineral.transform.SetParent(transform); // parent to slot

            if(GetComponentInChildren<Mineral>())
            {
                mineral.LerpTo(transform.position);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Mineral mineral = other.GetComponent<Mineral>();
        if (mineral != null && mineral == currentMineral)
        {
            currentMineral.CurrentZone = null;
            currentMineral.transform.SetParent(null);
            currentMineral = null;
        }
    }

    public void ForceRelease()
    {
        if (currentMineral != null)
        {
            
        }
    }
}
