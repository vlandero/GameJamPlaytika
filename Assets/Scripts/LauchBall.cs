using UnityEngine;

public class LauchBall : MonoBehaviour
{
    public Vector3 launchDirection;
    public LayerMask wallMask;

    private void Start()
    {
        launchDirection = Vector3.up;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = GameManager.instance.PlatformCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, wallMask))
            {
                launchDirection = (hit.point - transform.position).normalized;
            }
        }
    }
}
