using UnityEngine;

public class LusterTester : ToolBase
{
    public LusterOverlay[] overlays; // Assign in inspector
    public LusterOverlay currentOverlay; // optional, if you want to select a specific overlay effect
    public override void Use(Mineral target)
    {
        if (!target || target.mineralValues == null) return;

        int mineralLusterValue = Mathf.RoundToInt(target.mineralValues.luster); // numeric luster value
        
        // Find overlay that matches or is closest
        LusterOverlay effectToShow = null;
        foreach (var overlay in overlays)
        {
            if (overlay.value == mineralLusterValue)
            {
                effectToShow = overlay;
                break;
            }
        }
        
        // If nothing exact, pick the closest
        if (effectToShow == null && overlays.Length > 0)
        {
            effectToShow = overlays[0];
            int closestDiff = Mathf.Abs(mineralLusterValue - overlays[0].value);
            for (int i = 1; i < overlays.Length; i++)
            {
                int diff = Mathf.Abs(mineralLusterValue - overlays[i].value);
                if (diff < closestDiff)
                {
                    closestDiff = diff;
                    effectToShow = overlays[i];
                }
            }
        }
        
        // Spawn overlay effect on the mineral
        if (effectToShow != null && effectToShow.overlayPrefab != null)
        {
            Instantiate(effectToShow.overlayPrefab, target.transform.position, Quaternion.identity, target.transform);
        }
    }

}

[System.Serializable]
public class LusterOverlay
{
    public string lusterName;
    public int value; // 1-7
    public GameObject overlayPrefab;
}
