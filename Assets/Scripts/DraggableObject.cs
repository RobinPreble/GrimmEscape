using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset;

    void OnMouseDown()
    {
        // Calculate the offset between the mouse position and the object position
        offset = transform.position - GetMouseWorldPos();
        isDragging = true;
    }

    void OnMouseUp()
    {
        isDragging = false;
    }

    void Update()
    {
        if (isDragging)
        {
            // Update the object's position along the z-axis based on the mouse movement
            transform.position = new Vector3(transform.position.x, transform.position.y, GetMouseWorldPos().z + offset.z);
        }
    }

    private Vector3 GetMouseWorldPos()
    {
        // Get the current mouse position in world coordinates
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit))
        {
            return hit.point;
        }

        return transform.position; // Fallback if the ray doesn't hit anything
    }
}
