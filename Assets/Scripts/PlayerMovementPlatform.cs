using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;


public class PlayerMovementPlatform : MonoBehaviour
{
    public float movementSpeed = 10f;
    public GameObject groundPlane;
    public Transform meshTransform;

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
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = GameManager.instance.SideCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
            {
                if (hit.transform.CompareTag("ground"))
                {
                    targetPosition = hit.point;
                    targetPosition = ClampTargetPosition(targetPosition);
                    targetPosition.y = transform.position.y;
                    
                }
            }

        }
    }

    private Vector3 ClampTargetPosition(Vector3 targetPosition)
    {
        Renderer groundRenderer = groundPlane.GetComponent<Renderer>();
        Bounds bounds = groundRenderer.bounds;

        var halfPlayerScale = meshTransform.localScale / 2;
        float clampedX = Mathf.Clamp(targetPosition.x, bounds.min.x + halfPlayerScale.x + .3f, bounds.max.x - halfPlayerScale.x - .3f);
        float clampedZ = Mathf.Clamp(targetPosition.z, bounds.min.z + halfPlayerScale.z + .3f, bounds.max.z - halfPlayerScale.z - .3f);

        return new Vector3(clampedX, targetPosition.y, clampedZ);
    }


    void MovePlayer(Vector3 targetPosition)
    {
        if (!(Mathf.Abs(targetPosition.x - transform.position.x) > 1f || Mathf.Abs(targetPosition.z - transform.position.z) > 1f)) return;

        Vector3 moveDirection = (targetPosition - transform.position).normalized;

        transform.position += moveDirection * movementSpeed * Time.deltaTime;
    }
}