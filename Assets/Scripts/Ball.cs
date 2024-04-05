using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float ballSpeed = 5f;
    private Vector3 ballDirection;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ballDirection = Random.insideUnitSphere.normalized;
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + ballSpeed * Time.fixedDeltaTime * ballDirection);
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
    }
}