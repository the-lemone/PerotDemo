using UnityEngine;

public class Drag : MonoBehaviour
{
    private bool dragging;
    private Vector3 offset;
    private Vector3 originalPosition;

    void Start()
    {
        originalPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (dragging)
        {
            // Move object, take into account original offset
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            transform.position = mousePosition + offset;
        }
    }

    private void OnMouseDown()
    {
        // Record the difference between the objects center, and the clicked point on the camera plane
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        offset = transform.position - mousePosition;
        dragging = true;
    }

    private void OnMouseUp()
    {
        // Stop dragging
        dragging = false;
        
        // Check which DropZone we are over
        Collider2D[] hits = Physics2D.OverlapPointAll(transform.position);
        foreach (var hit in hits)
        {
            DropZone zone = hit.GetComponent<DropZone>();
            if (zone != null)
            {
                // Tell us we are dropped here
                zone.currentMineral = GetComponent<Mineral>();
            }
        }
        
        // Check sorting after release
        SortingManager manager = FindFirstObjectByType<SortingManager>();
        if (manager)
        {
            manager.CheckArrangement();
        }
    }
}
