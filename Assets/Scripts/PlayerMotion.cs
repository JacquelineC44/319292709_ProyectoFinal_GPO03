using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Cinemachine;

public class PlayerMotion : MonoBehaviour
{
    public Transform cam;
    public CinemachineCamera cinemachineFreeLook;
    public GameObject targetCam;
    public float speed;
    public float speedRotation = 10;
    public float rotationSpeedCamX, rotationSpeedCamY;
    Rigidbody rb;
    Animator anim;
    Vector2 _move, _mlook;
    Vector3 move;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(_move.x != 0 || _move.y != 0)
        {
            move = cam.forward * _move.y;
            move += cam.right * _move.x;
            move.Normalize();
            move.y = 0;
            rb.linearVelocity = move * speed;
            Vector3 dir = cam.forward * _move.y;
            dir += cam.right * _move.x;
            dir.Normalize();
            dir.y = 0;
            Quaternion targetR = Quaternion.LookRotation(dir);
            Quaternion playerR = Quaternion.Slerp(transform.rotation, targetR, speedRotation * Time.fixedDeltaTime);
        }
        
    }
    public void OnMove(InputValue Value)
    {
        _move = Value.Get<Vector2>();
        anim.SetBool("Move", (_move.x == 0 && _move.y == 0) ? false : true);
        anim.SetFloat("Moving", (_move.x == 0 && _move.y == 0) ? 0 : 1);
        if (_move.x == 0 && _move.y == 0)
            rb.linearVelocity = Vector3.zero;
        anim.SetFloat("MoveX", _move.x);
        anim.SetFloat("MoveY", _move.y);
    }
    public void Oncam(InputValue value)
    {
        _mlook = value.Get<Vector2>();
    }
}
