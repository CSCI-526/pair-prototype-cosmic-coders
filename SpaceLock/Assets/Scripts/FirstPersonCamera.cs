using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    public Transform player;
    public float mouseSensitivity = 2f;
    public float cameraDistance = 0.5f;
    public float minVerticalAngle = -90f;
    public float maxVerticalAngle = 90f;

    [Header("Camera Collision")]
    public float collisionRadius = 0.2f;
    public float collisionOffset = 0.1f;
    public LayerMask collisionMask;

    private float verticalRotation = 0f;
    private Vector3 cameraTargetPosition;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        HandleRotation();
    }

    private void LateUpdate()
    {
        HandleCameraCollision();
    }


    private void HandleCameraCollision()
    {
        Vector3 desiredCameraPos = player.position - player.forward * cameraDistance;
        RaycastHit hit;

        if (Physics.SphereCast(player.position, collisionRadius, -player.forward, out hit, cameraDistance, collisionMask))
        {
            float distanceToHit = Vector3.Distance(player.position, hit.point);
            cameraTargetPosition = player.position - player.forward * (distanceToHit - collisionOffset);
        }
        else
        {
            cameraTargetPosition = desiredCameraPos;
        }

        transform.position = cameraTargetPosition;
    }

    public void UpdateCameraPosition()
    {
        HandleCameraCollision();
    }
}
