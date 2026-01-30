using UnityEngine;

public class RespawnZone : MonoBehaviour
{
    private bool isQuitting;
    
    private void OnApplicationQuit()
    {
        isQuitting = true;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isQuitting) return;
        
        Mineral mineral = other.GetComponent<Mineral>();
        if (mineral != null)
            mineral.transform.SetParent(transform);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (isQuitting) return;
        
        Mineral mineral = other.GetComponent<Mineral>();
        if (mineral != null)
            mineral.transform.SetParent(null);
    }
}
