using UnityEngine;

public class RespawnZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Mineral mineral = other.GetComponent<Mineral>();
        if (mineral != null)
            mineral.transform.SetParent(transform);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Mineral mineral = other.GetComponent<Mineral>();
        if (mineral != null)
            mineral.transform.SetParent(null);
    }
}
