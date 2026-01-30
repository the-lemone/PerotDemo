using System;
using UnityEngine;

public class HardnessDial : MonoBehaviour
{
    [Header("Rotation Settings")]
    public float minAngle = -120f;
    public float maxAngle = 120f;

    public int steps = 10;
    
    private float currentAngle;
    private bool dragging;
    private Camera cam;

    void Awake()
    {
        cam = Camera.main;
    }

    void Start()
    {
        currentAngle = transform.localEulerAngles.z;
        GetHardnessValue();
    }
    
    void OnMouseDown() => dragging = true;
    void OnMouseUp() => dragging = false;

    void Update()
    {
        if (!dragging) return;
        
        Vector3 mouseWorld = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dir = mouseWorld - transform.position;

        float rawAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        float angle = Mathf.DeltaAngle(0, rawAngle - transform.parent.eulerAngles.z);
        
        float stepSize = (maxAngle - minAngle) / (steps - 1);
        
        // Snap angle BEFORE clamping
        float snapped = Mathf.Round((angle - minAngle) / stepSize) * stepSize + minAngle;

        // Direction lock
        bool tryingBelowMin = snapped < minAngle && currentAngle <= minAngle;
        bool tryingAboveMax = snapped > maxAngle && currentAngle >= maxAngle;

        if (tryingBelowMin || tryingAboveMax)
            return; // HARD STOP

        snapped = Mathf.Clamp(snapped, minAngle, maxAngle);

        currentAngle = snapped;
        transform.rotation = Quaternion.Euler(0, 0, currentAngle);
    }

    public int GetHardnessValue()
    {
        float t = Mathf.InverseLerp(minAngle, maxAngle, currentAngle);
        t = 1f - t;
        return Mathf.RoundToInt(Mathf.Lerp(1, steps, t));
    }
    
}
