using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    // Start is called before the first frame update
    public float SensX;
    public float SensY;

    public Transform orientation;
    public Transform player;
    public float cameraDistance = 0.5f;
    public float collisionRadius = 0.2f;
    public float collisionOffset = 0.1f;
    public LayerMask collisionMask;

    private Vector3 cameraTargetPosition;


    float xRotate;
    float yRotate;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        
    }
    private void LateUpdate()
    {
        HandleCameraCollision();
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * SensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * SensY;

        yRotate += mouseX;
        xRotate -= mouseY;

        xRotate = Mathf.Clamp(xRotate, -90f, 90f);
        transform.rotation = Quaternion.Euler(xRotate, yRotate,0);
        orientation.rotation = Quaternion.Euler(0, yRotate, 0);
    }

    private void HandleCameraCollision()
    {
        Vector3 desiredCameraPos = player.position - transform.forward * cameraDistance;
        RaycastHit hit;

        if (Physics.SphereCast(player.position, collisionRadius, -transform.forward, out hit, cameraDistance, collisionMask))
        {
            float distanceToHit = Vector3.Distance(player.position, hit.point);
            cameraTargetPosition = player.position - transform.forward * (distanceToHit - collisionOffset);
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
