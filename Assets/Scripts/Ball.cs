using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float ballSpeed = 5f;
    private Vector3 ballDirection;
    private Rigidbody rb;

    private Vector3 previousPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ballDirection = Random.insideUnitSphere.normalized;
        previousPosition = transform.position;
    }

    void FixedUpdate()
    {
        Vector3 currentPosition = transform.position;

        previousPosition = currentPosition;
        rb.MovePosition(rb.position + ballSpeed * Time.fixedDeltaTime * ballDirection);
    }

    public bool IsMovingUp()
    {
        return transform.position.y > previousPosition.y;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("wall") || collision.gameObject.CompareTag("brick"))
        {
            Vector3 normal = collision.contacts[0].normal;
            ballDirection = Vector3.Reflect(ballDirection, normal);
            if (collision.gameObject.CompareTag("brick"))
            {
                collision.gameObject.GetComponent<Brick>().HandleHit();
            }
        }
        if (collision.gameObject.CompareTag("platform"))
        {
            //ballDirection = GameManager.instance.GetPlatformArrowDirection();
            StartCoroutine(Bounce());
        }
    }

    private IEnumerator Bounce()
    {
        GameManager.instance.SeePlatformPerspective();
        GameManager.instance.frozen = true;
        Time.timeScale = 0;
        WaitUntil wait = new WaitUntil(() => Input.GetMouseButtonDown(1));
        yield return wait;
        GameManager.instance.frozen = false;
        GameManager.instance.lauchBall.DestroyAllMarkers();
        GameManager.instance.SeeSidePerspective();
        ballDirection = GameManager.instance.lauchBall.launchDirection;
        Time.timeScale = 1;
    }
}