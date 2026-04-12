using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.VisualScripting;

public class PlayerMotion : MonoBehaviour
{
    public Transform cam;
    public CinemachineCamera cinemachineFreeLook;
    private CinemachineOrbitalFollow orbitalFollow;
    public GameObject targetCam;
    public float speed;
    public float speedRotation = 10;
    //verificar las colisiones (altura y tamaño)
    public float groundDistanceUp, groundDistance;
    public float gravity = 9.8f;
    public float gravityMultiplayer = 1;
    public float jumpPower = 35;
    public float rotationSpeedCamX, rotationSpeedCamY;
    //angulo de inclinacion de rampas permitidas
    public float maxSlopeAngle = 40f;
    public float playerHeight = 0.2f;
    public bool onGround, isJump;       
    public bool stop;
    public LayerMask groundLayer;
    float slopeAngle;
    Rigidbody rb;
    Animator anim;
    Vector2 _move, _mlook;
    Vector3 move;
    RaycastHit slopeHit;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        orbitalFollow = cinemachineFreeLook.GetComponent<CinemachineOrbitalFollow>();
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position + (Vector3.up * groundDistanceUp), groundDistance);
    }
    //// fixedUpdate es mas constante
    void FixedUpdate()
    {
    //    //dtectar suelo
        onGround = Physics.CheckSphere(transform.position + (Vector3.up * groundDistance), groundDistance, groundLayer);
    //    //detectar pendiente
        bool onSlope = OnSlope();
    //    // Considerar rampa válida como suelo
        bool grounded = onGround; //|| onSlope;
        rb.useGravity = !onSlope;
    //    //comprobar que aun este en el suelo, altura de la esfera este mas baja
        groundDistanceUp = (onSlope) ? -.2f : .2f;        
        if (!onGround)
            rb.AddForce(-gravity * gravityMultiplayer * Vector3.up, ForceMode.Acceleration);

        if(isJump && onGround)
        {
            isJump = false;
            anim.SetBool("OnAir", false);
            rb.linearVelocity = Vector3.zero;
        }
        else if(!isJump && !onGround)
        {
            anim.SetBool("OnAir", true);
            isJump=true;
            Stopping();
            anim.SetTrigger("Fall");
        }
        if(_move.x != 0 || _move.y != 0)
        {
            move = cam.forward * _move.y;
            move += cam.right * _move.x;
            move.Normalize();
            move.y = 0;
            rb.linearVelocity = (onSlope) ? GetSlopeMoveDirection() * speed : move * speed;
            Vector3 dir = cam.forward * _move.y;
            dir += cam.right * _move.x;
            dir.Normalize();
            dir.y = 0;
            Quaternion targetR = Quaternion.LookRotation(dir);
            Quaternion playerR = Quaternion.Slerp(transform.rotation, targetR, speedRotation * Time.fixedDeltaTime);
            transform.rotation = playerR;
        }

    }
    ////para las rampas
    public bool OnSlope()
    {
        //crea un rayo imaginario
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight))
        {
            slopeAngle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return slopeAngle <= maxSlopeAngle && slopeAngle != 0;
        }
        return false;
    }
    Vector3 GetSlopeMoveDirection()
    {
        return Vector3.ProjectOnPlane(move, slopeHit.normal).normalized;
    }
    public void OnMove(InputValue value)
    {
        //movimiento en vector2 de las teclas, este se queda antes por si salta pueda ir a una direccion
        _move = value.Get<Vector2>();
        if (stop)
            return;
        anim.SetBool("Move", (_move.x == 0 && _move.y == 0) ? false : true);
        anim.SetFloat("Moving", (_move.x == 0 && _move.y == 0) ? 0 : 1);
        if (_move.x == 0 && _move.y == 0)
            rb.linearVelocity = Vector3.zero;
        anim.SetFloat("MoveX", _move.x);
        anim.SetFloat("MoveY", _move.y);
    }

    public void OnJump()
    {
    //    if (!onGround) 
    //        return;
        Stopping();
        isJump = true;
        Vector2 moveDir = _move;
        anim.SetTrigger("Jumping");
        if(moveDir != Vector2.zero)
        {
            Vector3 dir = cam.forward * moveDir.y;
            dir += cam.right * moveDir.x;
            dir.Normalize();
            dir.y = 0;
            Quaternion targetR = Quaternion.LookRotation(dir);
            transform.rotation = targetR;
            rb.AddForce((transform.forward + Vector3.up) * jumpPower, ForceMode.Impulse);
        }
        else
        {
            rb.AddForce(Vector3.up*jumpPower, ForceMode.Impulse);   
        }
        anim.SetBool("OnAir", true);
    }
    public void FallEnd()
    {
        StopEnd();
    }
    void Stopping()
    {
        if (onGround)
            rb.linearVelocity = Vector3.zero;
        stop = true;
        anim.SetFloat("MoveX", 0);
        anim.SetFloat("MoveY", 0);
        anim.SetFloat("Moving", 0);
        anim.SetBool("Move", false);
    }
    public void StopEnd()
    {
        anim.SetBool("Move", (_move.x == 0 && _move.y == 0) ? false : true);
        anim.SetFloat("Moving", (_move.x == 0 && _move.y == 0) ? 0 : 1);
        anim.SetFloat("MoveX", _move.x);
        anim.SetFloat("MoveY", _move.y);
        rb.linearVelocity = Vector3.zero ;
        stop = false;
    }
    public void Oncam(InputValue value)
    {
        _mlook = value.Get<Vector2>();
        orbitalFollow.HorizontalAxis.Value += _mlook.x * rotationSpeedCamX;
        orbitalFollow.VerticalAxis.Value += _mlook.y * rotationSpeedCamY * Time.fixedDeltaTime;
    }
    void TargetActive(bool b)
    {
        //if(targe)
    }
}

