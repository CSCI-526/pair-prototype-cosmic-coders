using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public GameObject player;
    public Vector3 offset = new Vector3(0, 5, -10);
    public float rotationSpeed = 2.5f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;

    void Start() {
    if (player == null) {
        Debug.LogError("Player GameObject is not assigned.");
    }

    transform.position = player.transform.position + offset;

    yaw = player.transform.eulerAngles.y;
    pitch = player.transform.eulerAngles.x;
    }

    void LateUpdate() {
        if (player != null) {

            yaw += rotationSpeed * Input.GetAxis("Mouse X");
            pitch -= rotationSpeed * Input.GetAxis("Mouse Y");

            pitch = Mathf.Clamp(pitch, -45f, 45f);

            Quaternion rotation = Quaternion.Euler(pitch, yaw, 0.0f);

            Vector3 position = player.transform.position + offset;

            transform.position = position;
            transform.rotation = rotation;
        }
    }
}
