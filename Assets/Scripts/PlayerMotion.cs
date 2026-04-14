using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.VisualScripting;
using System.Collections;
using System.Collections.Generic;

public class PlayerMotion : MonoBehaviour
{
    public Transform cam;
    public CinemachineCamera cinemachineFreeLook;
    private CinemachineOrbitalFollow orbitalFollow;
    public GameObject targetCam;
    public float speed;
    public float speedRotation = 10;
    public float groundDistanceUp, groundDistance;
    public float gravity = 9.8f;
    //velocidad que baja al suelo
    public float gravityMultiplayer = 1;
    public float jumpPower = 35;
    public float rotationSpeedCamX, rotationSpeedCamY;
    public bool onGround, isJump;
    public bool stop;
    private bool jumpWithDirection;
    public LayerMask groundLayer;
    Rigidbody rb;
    Animator anim;
    Vector2 _move, _mlook;
    Vector2 lastMoveInput;
    Vector3 move;
    Vector3 jumpDirection;
   


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();        
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position + (Vector3.up * groundDistanceUp), groundDistance);
    }

    private void FixedUpdate()
    {
        onGround = Physics.CheckSphere(transform.position + (Vector3.up * groundDistanceUp), groundDistance, groundLayer);
        if (!onGround)
            rb.AddForce(-gravity * gravityMultiplayer * Vector3.up, ForceMode.Acceleration);
        if (isJump && onGround)
        {
            Debug.Log("CORTA SALTO | onGround=" + onGround + " | vel=" + rb.linearVelocity);
            isJump = false;
            anim.SetBool("OnAir", false);
            rb.linearVelocity = Vector3.zero;
        }
        else if(!isJump && !onGround)
        {
            anim.SetBool("OnAir", true);
            isJump = true;
            Stopping();
            anim.SetTrigger("Fall");
        }
        if (isJump && !onGround && jumpWithDirection)
        {
            Debug.Log("MANTIENE DIRECCION | vel antes=" + rb.linearVelocity + " | dir=" + jumpDirection);
            rb.linearVelocity = new Vector3(
                jumpDirection.x * speed,
                rb.linearVelocity.y,
                jumpDirection.z * speed
            );
        }
        if (stop)
            return;
        if(_move.x != 0 || _move.y != 0)
        {
            move = cam.forward * _move.y;
            move += cam.right * _move.x;
            move.Normalize();
            move.y = 0;
            rb.linearVelocity = new Vector3(move.x * speed, rb.linearVelocity.y, move.z * speed);
            Vector3 dir = cam.forward * _move.y;
            dir += cam.right * _move.x;
            dir.y = 0;
            dir.Normalize();            
            Quaternion targetR = Quaternion.LookRotation(dir);
            Quaternion playerR = Quaternion.Slerp(transform.rotation, targetR, speedRotation * Time.fixedDeltaTime);
            transform.rotation = playerR;
        }
    }
    /*
    public void OnMove(InputValue value)
    {
        _move = value.Get<Vector2>();
        if (stop)
            return;
        anim.SetBool("Move", (_move.x == 0 && _move.y == 0) ? false : true);
        anim.SetFloat("Moving", (_move.x == 0 && _move.y == 0) ? 0 : 1);
        if (_move.x == 0 && _move.y == 0)
            rb.linearVelocity = Vector3.zero;
        anim.SetFloat("MoveX", _move.x);
        anim.SetFloat("MoveY", _move.y);
    }*/
    public void OnMove(InputValue value)
    {
        _move = value.Get<Vector2>();
        if (_move != Vector2.zero)
            lastMoveInput = _move;
        else
            lastMoveInput = Vector2.zero;

        if (stop)
            return;

        anim.SetBool("Move", (_move.x == 0 && _move.y == 0) ? false : true);
        anim.SetFloat("Moving", (_move.x == 0 && _move.y == 0) ? 0 : 1);

        if (_move.x == 0 && _move.y == 0)
            rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);

        anim.SetFloat("MoveX", _move.x);
        anim.SetFloat("MoveY", _move.y);
    }
    /*
    public void OnJump()
    {
        if (!onGround)
            return;

        Vector2 moveDir = _move;

        jumpWithDirection = false;
        jumpDirection = Vector3.zero;

        if (moveDir != Vector2.zero)
        {
            Vector3 dir = cam.forward * moveDir.y;
            dir += cam.right * moveDir.x;
            dir.y = 0;
            dir.Normalize();

            if (dir != Vector3.zero)
            {
                jumpWithDirection = true;
                jumpDirection = dir;
                transform.rotation = Quaternion.LookRotation(dir);
            }
        }

        Stopping();
        isJump = true;
        anim.SetTrigger("Jumping");
        anim.SetBool("OnAir", true);

        if (jumpWithDirection)
        {
            rb.linearVelocity = Vector3.zero;
            rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        }
        else
        {
            rb.linearVelocity = Vector3.zero;
            rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        }
    }*/
    //public void OnJump()
    //{
    //    if (!onGround)
    //    {
    //        return;
    //    }

    //    Vector2 moveDir = _move;

    //    jumpWithDirection = false;
    //    jumpDirection = Vector3.zero;

    //    Stopping();
    //    isJump = true;
    //    anim.SetTrigger("Jumping");
    //    anim.SetBool("OnAir", true);

    //    rb.linearVelocity = Vector3.zero;

    //    if (moveDir != Vector2.zero)
    //    {
    //        Debug.Log("DECISION: intento de salto con direccion");

    //        Vector3 dir = cam.forward * moveDir.y;
    //        dir += cam.right * moveDir.x;
    //        dir.y = 0;
    //        dir.Normalize();

    //        Debug.Log("dir calculada: " + dir);

    //        if (dir != Vector3.zero)
    //        {
    //            Debug.Log("RESULTADO: SALTO CON DIRECCION");

    //            jumpWithDirection = true;
    //            jumpDirection = dir;

    //            Quaternion targetR = Quaternion.LookRotation(dir);
    //            transform.rotation = targetR;

    //            rb.AddForce((dir + Vector3.up) * jumpPower, ForceMode.Impulse);
    //            return;
    //        }
    //        else
    //        {
    //            Debug.Log("RESULTADO: moveDir tenia valor, pero dir quedo en cero");
    //        }
    //    }
    //    else
    //    {
    //        Debug.Log("DECISION: moveDir es cero");
    //    }

    //    Debug.Log("RESULTADO: SALTO VERTICAL");
    //    rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
    //}
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
        rb.linearVelocity = Vector3.zero;
        stop = false;
    }
    public void Oncam(InputValue value)
    {
        _mlook = value.Get<Vector2>();
        orbitalFollow.HorizontalAxis.Value += _mlook.x * rotationSpeedCamX;
        orbitalFollow.VerticalAxis.Value += _mlook.y * rotationSpeedCamY * Time.fixedDeltaTime;
    }

}

