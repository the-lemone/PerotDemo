using System.Collections.Generic;
using UnityEngine;

public class RespawnZone : MonoBehaviour
{
    private readonly HashSet<Mineral> mineralsInside = new();

    private void OnTriggerEnter2D(Collider2D other)
    {
        Mineral mineral = other.GetComponent<Mineral>();
        if (mineral == null) return;

        mineralsInside.Add(mineral);
        mineral.SetRespawnZone(this);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Mineral mineral = other.GetComponent<Mineral>();
        if (mineral == null) return;

        mineralsInside.Remove(mineral);
        mineral.ClearRespawnZone(this);
    }

    public Vector3 GetRespawnPoint()
    {
        return transform.position;
    }
}
