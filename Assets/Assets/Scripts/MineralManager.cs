using UnityEngine;
using System.Collections.Generic;

public class MineralManager : MonoBehaviour
{
    public static MineralManager instance;
    
    [Header("All available minerals (Scriptable Objects)")]
    public List<MineralScriptableObject>allMinerals;
    
    [Header("Mineral slots in the scene")]
    public List<Mineral> minerals = new List<Mineral>();

    public BoxCollider2D spawnZone;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        LoadAllMinerals();
    }

    private void Start()
    {
        RespawnMinerals();
    }
    
    private void LoadAllMinerals()
    {
        allMinerals = new List<MineralScriptableObject>(
            Resources.LoadAll<MineralScriptableObject>("Minerals"));
    }

    public void RefreshMinerals()
    {
        if (allMinerals.Count < minerals.Count)
        {
            Debug.LogError("There aren't enough unique minerals");
            return;
        }
        
        List<MineralScriptableObject> pool = new List<MineralScriptableObject>(allMinerals);
        
        foreach (var slot in minerals)
        {
            // Pick a random mineral
            int index = Random.Range(0, pool.Count);
            MineralScriptableObject chosen = pool[index];
            
            // Assign it
            slot.AssignMineral(chosen);
            
            // Remove so we don't get duplicates
            pool.RemoveAt(index);
        }
    }
    
    public void RespawnMinerals()
    {
        foreach (var mineral in minerals)
        {
            Vector2 newPos = GetRandomPointInBox(spawnZone);
            mineral.transform.position = newPos;
            
            mineral.CurrentZone = null;
            mineral.transform.SetParent(null);
        }
        RefreshMinerals();
    }
    
    private Vector2 GetRandomPointInBox(BoxCollider2D box)
    {
        Vector2 size = box.size;
        Vector2 center = (Vector2)box.transform.position + box.offset;

        float x = Random.Range(center.x - size.x / 2f, center.x + size.x / 2f);
        float y = Random.Range(center.y - size.y / 2f, center.y + size.y / 2f);

        return new Vector2(x, y);
    }
}
