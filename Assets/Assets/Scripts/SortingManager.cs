using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SortingManager : MonoBehaviour
{
    public SortingRule[] rules;
    public Transform[] slots;

    public void CheckArrangement()
    {
        // Gather minerals in slot order
        List<Mineral> arranged = new List<Mineral>();
        foreach (Transform slot in slots)
        {
            if (slot.childCount > 0)
            {
                Mineral m = slot.GetChild(0).GetComponent<Mineral>();
                if (m) arranged.Add(m);
            }
        }
        
        // Now test against each rule
        foreach (var rule in rules)
        {
            if (MatchesRule(arranged, rule))
            {
                //Debug.Log($"Player discovered {rule.name}");
            }
        }
    }

    public bool MatchesRule(List<Mineral> minerals, SortingRule rule)
    {
        if (minerals.Count < 2) return false;
        
        // Extract attribute values
        List<float> values = minerals.Select(m =>
        {
            switch (rule.attribute)
            {
                case SortingRule.AttributeType.Hardness: return m.mineralValues.hardness;
                case SortingRule.AttributeType.crystalStructure: return m.mineralValues.crystalStructure;
                case SortingRule.AttributeType.Color: return m.mineralValues.color;
                default: return 0f;
            }
        }).ToList();
        
        // Check order
        for (int i = 0; i < values.Count - 1; i++)
        {
            if (rule.ascending && values[i] > values[i + 1]) return false;
            if (!rule.ascending && values[i] < values[i + 1]) return false;
        }

        return true;
    }
}
