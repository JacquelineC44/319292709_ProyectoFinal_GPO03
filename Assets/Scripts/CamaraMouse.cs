using UnityEngine;

public class CameraMouse: MonoBehaviour
{
    public float sensitivity = 2f;
    public float minPitch = -30f;
    public float maxPitch = 70f;

    float yaw;
    float pitch;

    void Start()
    {
        // Arrancamos desde la rotación actual
        Vector3 e = transform.eulerAngles;
        pitch = e.x;
        yaw = e.y;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // OLD input
        float mx = Input.GetAxis("Mouse X") * sensitivity;
        float my = Input.GetAxis("Mouse Y") * sensitivity;

        yaw += mx;
        pitch -= my;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        transform.rotation = Quaternion.Euler(pitch, yaw, 0f);

        // ESC libera el mouse (opcional)
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}