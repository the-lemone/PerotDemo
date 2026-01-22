using UnityEngine;

public class DrawerDrag : MonoBehaviour
{
    private bool dragging;
    private Vector3 offset;

    [Header("Y-Movement Limits")]
    [SerializeField] private float minX;
    [SerializeField] private float maxX;

    private float startY, startZ;

    private void Start()
    {
        startY = transform.position.y;
        startZ = transform.position.z;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (dragging)
        {
            // Move object, take into account original offset
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;

            Vector3 target = mousePosition + offset;

            float clampedX = Mathf.Clamp(target.x, minX, maxX);
            
            transform.position = new Vector3(clampedX, startY, startZ);
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
    }
}