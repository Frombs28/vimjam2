using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100.0f;
    public Transform playerBody;

    private float yRot;
    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        yRot = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        yRot -= mouseY;
        yRot = Mathf.Clamp(yRot, -90.0f, 90.0f);
        transform.localRotation = Quaternion.Euler(yRot, 0.0f, 0.0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
