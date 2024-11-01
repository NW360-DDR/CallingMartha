using UnityEngine;

public class CameraScript : MonoBehaviour
{
    readonly private float sensitivityX = 200;
    readonly private float sensitivityY = 200;

    Camera cam;

    float mouseX;
    float mouseY;

    readonly float multiplier = 0.01f;

    private float xRotation;
    private float yRotation;

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