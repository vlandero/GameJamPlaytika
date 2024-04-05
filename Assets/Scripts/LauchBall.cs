using UnityEngine;

public class LauchBall : MonoBehaviour
{
    public Vector3 launchDirection;
    public LayerMask wallMask;
    public GameObject hitMarkerPrefab;
    public float markerDuration = 1f;

    private void Start()
    {
        launchDirection = Vector3.up;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = GameManager.instance.PlatformCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, wallMask))
            {
                launchDirection = (hit.point - transform.position).normalized;

                GameObject marker = Instantiate(hitMarkerPrefab, hit.point, Quaternion.identity);

                Destroy(marker, markerDuration);
            }
        }
    }

    public void DestroyAllMarkers()
    {
        GameObject[] markers = GameObject.FindGameObjectsWithTag("HitMarker");
        foreach (GameObject marker in markers)
        {
            Destroy(marker);
        }
    }
}
