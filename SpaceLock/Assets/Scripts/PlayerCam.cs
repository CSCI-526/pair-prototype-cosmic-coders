using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    // Start is called before the first frame update
    public float SensX;
    public float SensY;

    public Transform orientation;

    float xRotate;
    float yRotate;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

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
}
