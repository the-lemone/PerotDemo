using System.Collections.Generic;
using UnityEngine;

public class LusterTester : ToolBase
{
    private CircleCollider2D detectionCircle;
    private Camera mainCam;

    // Keep track of minerals currently illuminated
    private readonly HashSet<Mineral> litMinerals = new HashSet<Mineral>();

    void Awake()
    {
        detectionCircle = GetComponent<CircleCollider2D>();
        detectionCircle.isTrigger = true;
        mainCam = Camera.main;
    }

    void Update()
    {
        // Follow the mouse position
        Vector3 mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;
        transform.position = mousePos;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Mineral mineral = other.GetComponent<Mineral>();
        if (mineral && !litMinerals.Contains(mineral))
        {
            litMinerals.Add(mineral);
            mineral.SetShining(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Mineral mineral = other.GetComponent<Mineral>();
        if (mineral && litMinerals.Contains(mineral))
        {
            litMinerals.Remove(mineral);
            mineral.SetShining(false);
        }
    }

    public override void OnSelect()
    {
        gameObject.SetActive(true);
        detectionCircle.enabled = true;
    }

    public override void OnDeselect()
    {
        // Turn off shining for all minerals when tool is deselected
        foreach (var mineral in litMinerals)
        {
            if(mineral)
                mineral.SetShining(false);
        }

        litMinerals.Clear();
        
        detectionCircle.enabled = false;
        gameObject.SetActive(false);
    }

    public override void Use(Mineral target)
    {
        
    }
}
