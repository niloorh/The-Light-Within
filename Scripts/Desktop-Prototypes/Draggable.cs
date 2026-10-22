using System;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    public float forceMultiplier = 5f;
    public Vector3 mouseVector = Vector3.right;

    private Rigidbody rb;
    private bool isDragging = false;
    private Vector3 offset;

    void Start()
    {
        //Debug.Log("In mouse funciton");
        rb = GetComponent<Rigidbody>();
    }

    void OnMouseDown()
    {
        //Debug.Log("In mouse funciton");
        isDragging = true;
        offset = transform.position - GetMouseWorldPos();
    }

    void OnMouseUp()
    {
        isDragging = false;
    }

    void FixedUpdate()
    {
        if (isDragging)
        {
            Vector3 targetPos = GetMouseWorldPos() + offset;
            //Debug.Log("Target: " + targetPos);
            // Only allow Z movement
            //rb.MovePosition(newPos);
            Vector3 force = new Vector3(
                (targetPos.x - transform.position.x) * mouseVector.x,
                (targetPos.y - transform.position.y) * mouseVector.y,
                (targetPos.z - transform.position.z) * mouseVector.z);
            //Debug.Log("Force: " + force);
            rb.AddForce(force.normalized * forceMultiplier);
        }
    }

    Vector3 GetMouseWorldPos()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, transform.position);
        plane.Raycast(ray, out float distance);
        return ray.GetPoint(distance);
    }
}
