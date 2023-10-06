using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableObject : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset;

    void OnMouseDown()
    {
        offset = gameObject.transform.position - GetMouseWorldPos();
        isDragging = true;
    }

    void OnMouseUp() { isDragging = false; }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = -10;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    void Update()
    {
        if (isDragging)
            transform.position = GetMouseWorldPos() + offset;
    }
}
