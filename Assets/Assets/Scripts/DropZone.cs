using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DropZone : MonoBehaviour
{
    [HideInInspector] public Mineral currentMineral;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        Mineral mineral = other.GetComponent<Mineral>();
        if (mineral != null)
        {
            currentMineral = mineral;
            mineral.transform.position = transform.position; // snap into place
            mineral.transform.SetParent(transform); // parent to slot
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Mineral mineral = other.GetComponent<Mineral>();
        if (mineral != null && mineral == currentMineral)
        {
            currentMineral = null;
            mineral.transform.SetParent(null);
        }
    }
    
}
