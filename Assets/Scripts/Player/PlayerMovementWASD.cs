using UnityEngine;

public class PlayerMovementWASD : MonoBehaviour
{
    public float movementSpeed = 5f;
    public GameObject groundPlane;
    public Transform meshTransform;

    private Camera sideCamera;

    private void Start()
    {
        sideCamera = GameManager.instance.SideCamera;
    }

    void FixedUpdate()
    {
        Vector3 forwardDirection = sideCamera.transform.forward;
        Vector3 rightDirection = sideCamera.transform.right;

        forwardDirection.y = 0f;
        rightDirection.y = 0f;

        forwardDirection.Normalize();
        rightDirection.Normalize();

        Vector3 movementDirection = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            movementDirection += forwardDirection;
        }
        if (Input.GetKey(KeyCode.S))
        {
            movementDirection -= forwardDirection;
        }
        if (Input.GetKey(KeyCode.A))
        {
            movementDirection -= rightDirection;
        }
        if (Input.GetKey(KeyCode.D))
        {
            movementDirection += rightDirection;
        }

        movementDirection.Normalize();

        Vector3 targetPosition = transform.position + movementDirection * movementSpeed * Time.deltaTime;

        transform.position = ClampTargetPosition(targetPosition);
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
}
