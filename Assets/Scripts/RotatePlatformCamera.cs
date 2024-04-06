using UnityEngine;

public class RotatePlatformCamera : MonoBehaviour
{
    public float rotationSpeed = 5.0f;
    public float maxRotationAngle = 45.0f;

    private Vector3 lastMousePosition;

    void Update()
    {
        if (Input.GetMouseButtonDown(2))
        {
            lastMousePosition = Input.mousePosition;
        }
        else if (Input.GetMouseButton(2))
        {
            Vector3 mouseDelta = Input.mousePosition - lastMousePosition;

            RotateCamera(mouseDelta);

            lastMousePosition = Input.mousePosition;
        }
    }

    void RotateCamera(Vector3 mouseDelta)
    {
        float rotationX = mouseDelta.y * rotationSpeed * Time.deltaTime;
        float rotationY = -mouseDelta.x * rotationSpeed * Time.deltaTime;

        float clampedRotationX = Mathf.Clamp(rotationX, -maxRotationAngle, maxRotationAngle);
        transform.Rotate(Vector3.right, clampedRotationX);

        float clampedRotationY = Mathf.Clamp(rotationY, -maxRotationAngle, maxRotationAngle);
        transform.Rotate(Vector3.up, clampedRotationY);
    }

    public void ResetRotation()
    {
        transform.localRotation = Quaternion.identity;
        transform.Rotate(new Vector3(-90, 0, 0));
    }
}
