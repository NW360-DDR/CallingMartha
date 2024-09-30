using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] float sensitivityX = 200;
    [SerializeField] float sensitivityY = 200;

    Camera cam;

    float mouseX;
    float mouseY;

    readonly float multiplier = 0.01f;

    public float xRotation;
    public float yRotation;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponentInChildren<Camera>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        mouseX = Input.GetAxisRaw("Mouse X");
        mouseY = Input.GetAxisRaw("Mouse Y");

        yRotation += mouseX * sensitivityX * multiplier;
        xRotation -= mouseY * sensitivityY * multiplier;

        cam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);

        transform.rotation = Quaternion.Euler(0, yRotation, 0);

        xRotation = Mathf.Clamp(xRotation, -90, 90);
    }
}