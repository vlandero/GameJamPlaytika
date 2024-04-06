using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float ballSpeed = 5f;
    private Vector3 ballDirection;
    public Rigidbody rb;

    private Vector3 previousPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ballDirection = Vector3.down;
        previousPosition = transform.position;
    }

    void FixedUpdate()
    {
        if (GameManager.instance.frozen)
        {
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
        else
        {
            rb.constraints = RigidbodyConstraints.FreezeRotation;
        }
        rb.velocity = Vector3.zero;
        Vector3 currentPosition = transform.position;

        previousPosition = currentPosition;
        rb.velocity = ballSpeed * ballDirection * Time.deltaTime;
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
        yield return new WaitForSeconds(1f);
        WaitUntil wait = new WaitUntil(() => Input.GetMouseButtonDown(0));
        yield return wait;
        GameManager.instance.SeeSidePerspective();
        ballDirection = GameManager.instance.lauchBall.launchDirection;
    }
}