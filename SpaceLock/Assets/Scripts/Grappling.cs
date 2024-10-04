using System.Collections;
using UnityEngine;

public class Grappling : MonoBehaviour
{
    [Header("References")]
    public Transform cam;
    public Transform gunTip;
    public LayerMask whatIsGrappleable;
    public LineRenderer lr;
    private PlayerMovement playerMovement;
    private Rigidbody rb;

    [Header("Grappling")]
    public float maxGrappleDistance = 100f;
    public float grappleDelayTime = 0.1f;
    public float grappleSpeed = 10f;

    private Vector3 grapplePoint;

    [Header("Cooldown")]
    public float grapplingCd = 2f;
    private float grapplingCdTimer;

    [Header("Input")]
    public KeyCode grappleKey = KeyCode.Mouse1;

    public PlayerCam cameraHandler;

    private bool grappling;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(grappleKey)) StartGrapple();

        if (grapplingCdTimer > 0)
            grapplingCdTimer -= Time.deltaTime;
    }

    private void LateUpdate()
    {
        if (grappling)
            lr.SetPosition(0, gunTip.position);
    }

    private void StartGrapple()
    {
        if (grapplingCdTimer > 0) return;

        grappling = true;

        playerMovement.enabled = false;
        //rb.velocity = Vector3.zero;

        RaycastHit hit;
        if (Physics.Raycast(cam.position, cam.forward, out hit, maxGrappleDistance, whatIsGrappleable))
        {
            grapplePoint = hit.point;
            Invoke(nameof(ExecuteGrapple), grappleDelayTime);
        }
        else
        {
            grapplePoint = cam.position + cam.forward * maxGrappleDistance;
            Invoke(nameof(StopGrapple), grappleDelayTime);
        }

        lr.enabled = true;
        lr.SetPosition(1, grapplePoint);
    }

    private void ExecuteGrapple()
    {
        StartCoroutine(MovePlayerToGrapplePoint());
    }

    private IEnumerator MovePlayerToGrapplePoint()
    {
        float startTime = Time.time;

        while (Vector3.Distance(transform.position, grapplePoint) > 0.5f)
        {
            float journeyLength = Vector3.Distance(transform.position, grapplePoint);
            float distanceCovered = (Time.time - startTime) * grappleSpeed;
            float fractionOfJourney = distanceCovered / journeyLength;

            rb.MovePosition(Vector3.Lerp(transform.position, grapplePoint, fractionOfJourney));

            yield return null;
        }

        AttachToObject();
        StopGrapple();
    }

    private void AttachToObject()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 1f, whatIsGrappleable);
        if (colliders.Length > 0)
        {
            transform.SetParent(colliders[0].transform);
            rb.isKinematic = true;
        }
        cameraHandler.UpdateCameraPosition();
    }

    public void StopGrapple()
    {
        grappling = false;
        grapplingCdTimer = grapplingCd;
        lr.enabled = false;

        // Only re-enable player movement if not attached to an object
        if (!playerMovement.IsAttachedToObject())
        {
            playerMovement.enabled = true;
            rb.isKinematic = false;
        }
        StartCoroutine(SmoothCameraTransition());
    }

    public bool IsGrappling()
    {
        return grappling;
    }

    public Vector3 GetGrapplePoint()
    {
        return grapplePoint;
    }

    private IEnumerator SmoothCameraTransition()
    {
        Vector3 startPos = cameraHandler.transform.position;
        Vector3 endPos = transform.position - transform.forward * cameraHandler.cameraDistance;
        float journeyLength = Vector3.Distance(startPos, endPos);
        float startTime = Time.time;

        while (Vector3.Distance(cameraHandler.transform.position, endPos) > 0.01f)
        {
            float distCovered = (Time.time - startTime) * 5f; // Adjust 5f to change transition speed
            float fractionOfJourney = distCovered / journeyLength;
            cameraHandler.transform.position = Vector3.Lerp(startPos, endPos, fractionOfJourney);
            yield return null;
        }

        cameraHandler.UpdateCameraPosition();
    }
}