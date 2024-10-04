using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public float rotationSpeed = 2f;

    [Header("References")]
   
    private Rigidbody rb;

    [Header("Jump Settings")]
    public float jumpCooldown = 1f;
    private float jumpCooldownTimer;

    [Header("Camera")]
    public PlayerCam cameraHandler;
    public Camera playerCamera;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false; // Disable gravity for zero-G environment      
    }

    private void Update()
    {
        // Handle rotation
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
        transform.Rotate(Vector3.up * mouseX);

        float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed;
        //playerCamera.transform.Rotate(Vector3.left * mouseY);

        // Handle jump input
        if (Input.GetKeyDown(KeyCode.Space) && jumpCooldownTimer <= 0)
        {
            Jump();
        }

        // Update jump cooldown
        if (jumpCooldownTimer > 0)
        {
            jumpCooldownTimer -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        // Handle movement
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        Vector3 movement = (transform.right * moveHorizontal + transform.forward * moveVertical).normalized;
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        cameraHandler.UpdateCameraPosition();
    }

    private void Jump()
    {
        // Apply jump force in the direction the player is looking
        Vector3 jumpDirection = playerCamera.transform.forward;
        rb.AddForce(jumpDirection * jumpForce, ForceMode.Impulse);

        // Set cooldown
        jumpCooldownTimer = jumpCooldown;
    }

    // Method to check if the player is attached to an object
    public bool IsAttachedToObject()
    {
        return transform.parent != null;
    }

    // Method to detach from the current object
    public void DetachFromObject()
    {
        if (IsAttachedToObject())
        {
            transform.SetParent(null);
            rb.isKinematic = false;
        }
    }
}
