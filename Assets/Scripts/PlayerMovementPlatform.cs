using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;


public class PlayerMovementPlatform : MonoBehaviour
{
    public float movementSpeed = 10f; // Adjust this speed according to your needs
    public GameObject groundPlane; // Assign your ground plane object in the Unity editor
    private Vector3 targetPosition;
    [SerializeField] private LayerMask groundLayer;

    private void Start()
    {
        targetPosition = transform.position;
    }

    private void FixedUpdate()
    {
        MovePlayer(targetPosition);
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Check for left mouse button click
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;



            if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer)) // Cast a ray from the mouse position, only detecting objects on the "Ground" layer
            {
                // Check if the ray hits the ground plane
                if (hit.transform.CompareTag("ground"))
                {
                    // Get the position clicked by the mouse
                    targetPosition = hit.point;
                    targetPosition = ClampTargetPosition(targetPosition);
                    targetPosition.y = transform.position.y; // Maintain current Y position
                    
                }
            }

        }
    }

    private Vector3 ClampTargetPosition(Vector3 targetPosition)
    {
        // Get the bounds of the ground plane
        Renderer groundRenderer = groundPlane.GetComponent<Renderer>();
        Bounds bounds = groundRenderer.bounds;

        // Clamp the target position within the bounds
        var halfPlayerScale = transform.localScale / 2;
        float clampedX = Mathf.Clamp(targetPosition.x, bounds.min.x + halfPlayerScale.x + .3f, bounds.max.x - halfPlayerScale.x - .3f);
        float clampedZ = Mathf.Clamp(targetPosition.z, bounds.min.z + halfPlayerScale.z + .3f, bounds.max.z - halfPlayerScale.z - .3f);

        Debug.Log(clampedZ + " " + clampedX);

        // Return the clamped target position
        return new Vector3(clampedX, targetPosition.y, clampedZ);
    }


    void MovePlayer(Vector3 targetPosition)
    {
        if (!(Mathf.Abs(targetPosition.x - transform.position.x) > 1f || Mathf.Abs(targetPosition.z - transform.position.z) > 1f)) return;

        Vector3 moveDirection = (targetPosition - transform.position).normalized;

        transform.position += moveDirection * movementSpeed * Time.deltaTime;
    }
}