using UnityEngine;

public class LauchBall : MonoBehaviour
{
    public Vector3 launchDirection;
    public Vector3 launchPosition;
    public LayerMask wallMask;
    public GameObject hitMark;

    private void Start()
    {
        launchDirection = Vector3.up;
        launchPosition = transform.position;
        hitMark.SetActive(false);
    }

    private void Update()
    {
        hitMark.SetActive(true);
        Ray ray = GameManager.instance.PlatformCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, wallMask))
        {
            launchDirection = (hit.point - GameManager.instance.PlatformCamera.transform.position).normalized;
            launchPosition = GameManager.instance.PlatformCamera.transform.position;
            hitMark.transform.position = hit.point;
            hitMark.transform.up = hit.normal;
        }
    }
}
