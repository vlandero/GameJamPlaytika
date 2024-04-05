using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveArrow : MonoBehaviour
{
    public Vector3 vectorToDisplay;
    public Color arrowColor = Color.red;
    public float rotationSpeed = 50f;
    public GameObject arrowInstance;
    public LayerMask wallMask;
    public bool arrowVisible = true;

    private LineRenderer lineRenderer;
    private float rotationAngleX = 0f;
    private float rotationAngleZ = 0f;

    void Start()
    {
        lineRenderer = arrowInstance.GetComponent<LineRenderer>();
        lineRenderer.startColor = arrowColor;
        lineRenderer.endColor = arrowColor;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        DeactivateArrow();
    }

    void Update()
    {
        if(!arrowVisible)
        {
            return;
        }
        float rotationInputHor = Input.GetAxis("Horizontal");
        float rotationInputVer = Input.GetAxis("Vertical");

        if (rotationInputHor != 0 || rotationInputVer != 0)
        {
            rotationAngleX += rotationSpeed * Time.deltaTime * rotationInputHor * (1 / GameManager.slowMotionTimeScale);
            rotationAngleX = Mathf.Clamp(rotationAngleX, -45, 45);
            rotationAngleZ += rotationSpeed * Time.deltaTime * rotationInputVer * (1 / GameManager.slowMotionTimeScale);
            rotationAngleZ = Mathf.Clamp(rotationAngleZ, -45, 45);

            arrowInstance.transform.rotation = Quaternion.Euler(rotationAngleX, 0, rotationAngleZ);
        }

        if (Physics.Raycast(transform.position, arrowInstance.transform.up, out RaycastHit hit, Mathf.Infinity, wallMask))
        {
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, hit.point);
            vectorToDisplay = (hit.point - transform.position).normalized;
        }
        else
        {
            lineRenderer.SetPosition(0, Vector3.zero);
            lineRenderer.SetPosition(1, Vector3.zero);
        }
    }

    public void ActivateArrow()
    {
        arrowInstance.SetActive(true);
        arrowVisible = true;
    }

    public void DeactivateArrow()
    {
        arrowInstance.SetActive(false);
        arrowVisible = false;
    }
}
