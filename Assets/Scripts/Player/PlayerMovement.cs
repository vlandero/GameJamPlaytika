using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.1f;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float jumpHeight = 3f;
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private Transform orientation;
    [SerializeField] private Transform[] groundChecks;

    private Vector3 moveDirection;

    private Vector3 velocity;
    private bool isGrounded;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        MovePlayer();
        ApplyGravity();
        ApplySlopeGravity();
    }

    private void MovePlayer()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        moveDirection = orientation.forward * z + orientation.right * x;
        moveDirection = Vector3.ClampMagnitude(moveDirection, 1f);
        controller.Move(movementSpeed * Time.deltaTime * moveDirection);
    }

    private void ApplyGravity()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0) velocity.y = -2f;

        if (Input.GetKey(KeyCode.Space) && isGrounded) velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

    private void ApplySlopeGravity()
    {
        foreach (Transform groundCheck in groundChecks)
        {
            RaycastHit hit;
            if (Physics.Raycast(groundCheck.position, Vector3.down, out hit, groundDistance, groundMask))
            {
                float slopeAngle = Vector3.Angle(hit.normal, Vector3.up);
                if (slopeAngle > controller.slopeLimit)
                {
                    Vector3 slideDirection = -hit.normal;
                    controller.Move(slideDirection * gravity * Time.deltaTime);
                }
            }
        }
    }

}
