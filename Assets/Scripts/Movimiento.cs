using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Movimiento : MonoBehaviour
{
    public float velocidad;
    public float velocidadrot = 10f;
    public float gravedad = -20f;
    CharacterController controller;
    float velocidadY = 0f;
    public Transform cam;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        Vector2 moveInput = new Vector2(x, y);

        Vector3 move = Vector3.zero;
        if (moveInput.sqrMagnitude > 0.001f && cam != null)
        {
            Vector3 forward = cam.forward;
            Vector3 right = cam.right;

            forward.y = 0f;
            right.y = 0f;
            forward.Normalize();
            right.Normalize();

            move = forward * moveInput.y + right * moveInput.x;
            move.Normalize();

            Quaternion targetR = Quaternion.LookRotation(move);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetR, velocidadrot * Time.deltaTime);

            if (controller.isGrounded && velocidadY < 0f)
                velocidadY = -2f;
            velocidadY += gravedad * Time.deltaTime;
            Vector3 veloz = move * velocidad;
            veloz.y = velocidadY;
            controller.Move(veloz * Time.deltaTime);

        }

    }
}

