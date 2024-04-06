using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerMovement : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private Transform target;
    private Vector3 previousPosition;

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
        }

        if(Input.GetMouseButton(1))
        {
            Vector3 direction = previousPosition - cam.ScreenToViewportPoint(Input.mousePosition);
            cam.transform.position = target.position;

            cam.transform.Rotate(new Vector3(x: 1, y: 0, z: 0), angle: direction.y * 180);
            cam.transform.Rotate(new Vector3(x: 0, y: 1, z: 0), -direction.x * 180, Space.World);
            cam.transform.Translate(new Vector3(x: 0, y: 0, z: -25));

            previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
        }
        
    }
}
