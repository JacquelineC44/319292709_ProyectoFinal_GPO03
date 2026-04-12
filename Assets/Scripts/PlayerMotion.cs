using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.VisualScripting;
using System.Collections;
using System.Collections.Generic;

public class PlayerMotion : MonoBehaviour
{
    public Transform cam;
    public float speed;
    public float speedRotation = 10;
    Rigidbody rb;
    Animator anim;
    Vector2 _move;
    Vector3 move;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();        
    }

    private void FixedUpdate()
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
            transform.rotation = playerR;
        }
    }

    public void OnMove(InputValue value)
    {
        _move = value.Get<Vector2>();
        anim.SetBool("Move", (_move.x == 0 && _move.y == 0) ? false : true);
        anim.SetFloat("Moving", (_move.x == 0 && _move.y == 0) ? 0 : 1);
        if (_move.x == 0 && _move.y == 0)
            rb.linearVelocity = Vector3.zero;
        anim.SetFloat("MoveX", _move.x);
        anim.SetFloat("MoveY", _move.y);
    }


}

