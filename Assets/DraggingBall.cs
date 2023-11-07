using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DraggingBall : MonoBehaviour
{
    private Vector3 offset;
    private float zCoordinate;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main; // Cache the main camera
    }

    void OnMouseDown()
    {
        // Calculate the z coordinate of the object in the screen space
        zCoordinate = mainCamera.WorldToScreenPoint(gameObject.transform.position).z;

        // Store offset = gameobject world pos - mouse world pos
        offset = gameObject.transform.position - GetMouseWorldPos();
    }

    void OnMouseDrag()
    {
        // Move the object to the new mouse position
        transform.position = GetMouseWorldPos() + offset;
    }

    private Vector3 GetMouseWorldPos()
    {
        // Pixel coordinates (x,y)
        Vector3 mousePoint = Input.mousePosition;

        // z coordinate of game object on screen
        mousePoint.z = zCoordinate;

        // Convert it to world points
        return mainCamera.ScreenToWorldPoint(mousePoint);
    }
}


