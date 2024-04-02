using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPCam : MonoBehaviour
{
    [Header("References")]
    public Transform orientation;
    public Transform player;
    public Transform playerObj;
    public Rigidbody rb;

    public float rotationSpeed;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {

        Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        playerObj.forward = viewDir.normalized;

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 inputDir = playerObj.forward * verticalInput + playerObj.right * horizontalInput;

        if (inputDir != Vector3.zero)
        {
            Debug.Log("Moving");
            playerObj.forward = Vector3.Slerp(playerObj.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
        }

    }
}
