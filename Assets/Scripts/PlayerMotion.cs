using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.VisualScripting;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class PlayerMotion : MonoBehaviour
{
    public Transform cam;
    public CinemachineCamera cinemachineFreeLook;
    private CinemachineOrbitalFollow orbitalFollow;
    //focus
    public CinemachineCamera virtualCam;
    private CinemachineThirdPersonFollow thirdPersonFollow;
    public GameObject targetCam;
    //focus
    public Transform targetPlayer;
    public Transform follow;
    public float speed;
    public float speedRotation = 10;
    public float groundDistanceUp, groundDistance;
    public float gravity = 9.8f;
    //velocidad que baja al suelo
    public float gravityMultiplayer = 1;
    public float jumpPower = 35;
    public float rotationSpeedCamX, rotationSpeedCamY;
    //slope
    public float maxSlopeAngle = 40f;
    //ROLL
    public float rollPower, dodgePower, rollMultiplayer;
    public bool onGround, isJump;
    public bool stop;
    public bool focus;
    private bool jumpWithDirection;
    
    //roll
    public bool isRoll;
    //cofre
    public bool interacting;
    public LayerMask groundLayer;
    public ItemsCollision chest;
    float slopeAngle;
    //comabte 
    PlayerCombat playerCombat;
    Rigidbody rb;
    Animator anim;
    Vector2 _move, _mlook;
    Vector2 lastMoveInput;
    Vector3 move;
    Vector3 jumpDirection;
    RaycastHit slopeHit;
    //focus
    ZTarget zTarget;
    //roll
    DG.Tweening.Sequence s;
    TutorialManager tutorialActual;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        zTarget = GetComponent<ZTarget>();
        playerCombat = GetComponent<PlayerCombat>();
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position + (Vector3.up * groundDistanceUp), groundDistance);
    }

    private void FixedUpdate()
    {
    //    //slope
    //    //bool onSlope = OnSlope();
    //    //rb.useGravity = !onSlope;
    //    //groundDistanceUp = (onSlope) ? -.2f : .2f;
    //    //
        onGround = Physics.CheckSphere(transform.position + (Vector3.up * groundDistanceUp), groundDistance, groundLayer);
        bool onSlope = OnSlope();
        if (isRoll)
        {
            if (!onGround)
            {
                rb.AddForce(Vector3.down * gravity * gravityMultiplayer, ForceMode.Acceleration);
            }

            return;
        }
        //    rb.useGravity = !onSlope;
        //    if (!onGround && !onSlope)
        //        rb.AddForce(-gravity * gravityMultiplayer * Vector3.up, ForceMode.Acceleration);
        //    if (isJump && onGround)
        //    {
        //        isJump = false;
        //        anim.SetBool("OnAir", false);
        //        rb.linearVelocity = Vector3.zero;
        //        playerCombat.isAttacking = false;
        //    }
        //    else if (!isJump && !onGround)
        //    {
        //        anim.SetBool("OnAir", true);
        //        isJump = true;
        //        //Stopping();
        //        anim.SetTrigger("Fall");
        //    }
        //    if (isJump && !onGround && jumpWithDirection)
        //    {
        //        Debug.Log("MANTIENE DIRECCION | vel antes=" + rb.linearVelocity + " | dir=" + jumpDirection);
        //        rb.linearVelocity = new Vector3(
        //            jumpDirection.x * speed,
        //            rb.linearVelocity.y,
        //            jumpDirection.z * speed
        //        );
        //    }
        if (focus && !interacting)
        {
            UpdateFocus();
        }
        if (playerCombat.isAttacking)
            return;
        if (stop)
            return;
        if (!focus)
        {
            if (_move.x != 0 || _move.y != 0)
            {
                move = cam.forward * _move.y;
                move += cam.right * _move.x;
                move.y = 0;
                move.Normalize();
                Vector3 finalMove = onSlope ? GetSlopeMoveDirection() : move;

                //rb.linearVelocity = move * speed;//(onSlope) ? GetSlopeMoveDirection() * speed : new Vector3(move.x * speed, rb.linearVelocity.y, move.z * speed);
                rb.linearVelocity = new Vector3(finalMove.x * speed, rb.linearVelocity.y, finalMove.z * speed);
                Vector3 dir = cam.forward * _move.y;
                dir += cam.right * _move.x;
                dir.y = 0;
                dir.Normalize();
                Quaternion targetR = Quaternion.LookRotation(move/*dir*/);
                Quaternion playerR = Quaternion.Slerp(transform.rotation, targetR, speedRotation * Time.fixedDeltaTime);
                transform.rotation = playerR;
                transform.rotation = Quaternion.Slerp(transform.rotation, targetR, speedRotation * Time.fixedDeltaTime);
            }
            else
            {
                rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
            }
            if (!onGround)
            {
                rb.AddForce(Vector3.down * gravity * gravityMultiplayer, ForceMode.Acceleration);
            }
        }
        else
        {
            move = cam.forward * _move.y;
            move += cam.right * _move.x;
            move.Normalize();
            move.y = 0;
            rb.linearVelocity = (onSlope) ? GetSlopeMoveDirection() * speed : move * speed;
        }

    }
    ///*
    //public void OnMove(InputValue value)
    //{
    //    _move = value.Get<Vector2>();
    //    if (stop)
    //        return;
    //    anim.SetBool("Move", (_move.x == 0 && _move.y == 0) ? false : true);
    //    anim.SetFloat("Moving", (_move.x == 0 && _move.y == 0) ? 0 : 1);
    //    if (_move.x == 0 && _move.y == 0)
    //        rb.linearVelocity = Vector3.zero;
    //    anim.SetFloat("MoveX", _move.x);
    //    anim.SetFloat("MoveY", _move.y);
    //}*/
    public void OnMove(InputValue value)
    {
        _move = value.Get<Vector2>();
        //if (_move != Vector2.zero)
        //    lastMoveInput = _move;
        //else
        //    lastMoveInput = Vector2.zero;

        if (stop || interacting || playerCombat.isAttacking)
            return;

        anim.SetBool("Move", (_move.x == 0 && _move.y == 0) ? false : true);
        anim.SetFloat("Moving", (_move.x == 0 && _move.y == 0) ? 0 : 1);

        if (_move.x == 0 && _move.y == 0)
            rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);

        anim.SetFloat("MoveX", _move.x);
        anim.SetFloat("MoveY", _move.y);
    }
    public void OnRoll()
    {
        if (TutorialManager.tutorialActivo != null)
            TutorialManager.tutorialActivo.CompletarAccion("roll");

        if (isRoll)
            return;

        isRoll = true;

        Vector2 rollInput = _move;
        Vector3 rollDir;

        if (rollInput.x != 0 || rollInput.y != 0)
        {
            if (Mathf.Abs(rollInput.x) > Mathf.Abs(rollInput.y))
            {
                rollInput.y = 0;
            }
            else if (Mathf.Abs(rollInput.y) > Mathf.Abs(rollInput.x))
            {
                rollInput.x = 0;
            }
            else
            {
                rollInput.y = 0;
            }

            if (rollInput.x != 0)
                rollInput.x = rollInput.x < 0 ? -1f : 1f;

            if (rollInput.y != 0)
                rollInput.y = rollInput.y < 0 ? -1f : 1f;

            anim.SetFloat("MoveX", rollInput.x);
            anim.SetFloat("MoveY", rollInput.y);

            rollDir = cam.forward * rollInput.y;
            rollDir += cam.right * rollInput.x;
        }
        else
        {
            rollDir = cam.forward;
            anim.SetFloat("MoveX", 0);
            anim.SetFloat("MoveY", 1);
        }

        rollDir.y = 0;
        rollDir.Normalize();

        rb.linearVelocity = Vector3.zero;

        float power = rollInput.x != 0 ? dodgePower : rollPower;

        rb.AddForce(rollDir * power * rollMultiplayer, ForceMode.Impulse);

        if (focus)
        {
            anim.SetTrigger("Jumping");
        }
        else
        {
            anim.SetTrigger("Roll");
        }

        s = DOTween.Sequence();
        s.AppendInterval(.5f).OnComplete(() =>
        {
            if (isRoll)
                StopEnd();
        });
    }
    //public void FallEnd()
    //{
    //    StopEnd();
    //}
    public void rollStop()
    {
        isRoll = false;
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
        isRoll = false;
        rb.linearVelocity = Vector3.zero;
        stop = false;
        isFocus();
    }
    public void Oncam(InputValue value)
    {
        if (interacting)
            return;
        _mlook = value.Get<Vector2>();
        orbitalFollow.HorizontalAxis.Value += _mlook.x * rotationSpeedCamX;
        orbitalFollow.VerticalAxis.Value += _mlook.y * rotationSpeedCamY * Time.fixedDeltaTime;
    }

    ////public bool OnSlope()
    ////{
    ////    if(Physics.Raycast(transform.position, Vector3.down, out slopeHit) && onGround)
    ////    {
    ////        slopeAngle = Vector3.Angle(Vector3.up, slopeHit.normal);
    ////        return slopeAngle <= maxSlopeAngle && slopeAngle != 0;
    ////    }
    ////    return false;
    ////}
    public bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, 2f, groundLayer))
        {
            slopeAngle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return slopeAngle > 0 && slopeAngle <= maxSlopeAngle;
        }

        return false;
    }
    Vector3 GetSlopeMoveDirection()
    {
        return Vector3.ProjectOnPlane(move, slopeHit.normal).normalized;
    }
    public void OnFocus(InputValue value)
    {
        if (TutorialManager.tutorialActivo != null)
            TutorialManager.tutorialActivo.CompletarAccion("focus");
        focus = value.isPressed;
        if (stop || isJump)
            return;
        isFocus();
    }
    //Se crea un metodo a parte para poder hacer focus en distintos eventos, como cuando se encuentra en el aire.
    public void isFocus()
    {
        if (focus)
        {
            Debug.Log("Entro al isFOcus");
            if (targetPlayer == null)
            {
                targetPlayer = zTarget.FirstTarget();
            }
            if (targetPlayer == null)
            {
                anim.SetBool("isFocus", false);
                anim.SetTrigger("SwitchWeapon");
                focus = false;
                return;
            }
            TargetActive(true);
            virtualCam.Priority = 10;
            cinemachineFreeLook.Priority = 8;
            anim.SetBool("isFocus", true);
            anim.SetTrigger("Focus");

        }
        else
        {
            if (targetPlayer != null)
                TargetActive(false);
            zTarget.t = null;
            targetPlayer = null;
            virtualCam.Priority = 8;
            cinemachineFreeLook.Priority = 10;
            anim.SetBool("isFocus", false);
            anim.SetTrigger("SwitchWeapon");
        }

    }
    public void OnChangeTargetL()
    {
        if (targetPlayer == null)
            return;
        TargetActive(false);
        targetPlayer = zTarget.NextToLeft();
        TargetActive(true);
        UpdateFocus();
    }
    public void OnChangeTargetR()
    {
        if (targetPlayer == null)
            return;
        TargetActive(false);
        targetPlayer = zTarget.NextToRight();
        TargetActive(true);
        UpdateFocus();
    }
    public void UpdateFocus()
    {
        targetCam.transform.LookAt(targetPlayer);
        follow.position = targetCam.transform.position;
        follow.rotation = targetCam.transform.rotation;
        //transform.localEulerAngles = new Vector3(0, follow.localEulerAngles.y, 0);
        transform.rotation = Quaternion.Euler(0, follow.eulerAngles.y, 0);

    }
    void TargetActive(bool b)
    {
        if (targetPlayer.GetComponent<Life>())
            targetPlayer.GetComponent<Life>().targetPoint.SetActive(b);
    }

    ////roll
    //public void rollStop()
    //{
    //    rb.linearVelocity = Vector3.zero;
    //}
    ////cofre
    public void OnUse()
    {
        tutorialActual = TutorialManager.tutorialActivo;

        if (tutorialActual != null && tutorialActual.EsperandoContinuar())
        {
            tutorialActual.CompletarAccion("continuar");
            return;
        }
        if (!Attack())
            return;
        if (chest)
        {
            chest.Open();
            return;
        }
    }
    public void selectTarget(Transform objetive)
    {
        if (targetPlayer != null)
            TargetActive(false);
        targetPlayer = null;
        virtualCam.Priority = 10;
        cinemachineFreeLook.Priority = 8;
        targetCam.transform.LookAt(objetive);
        follow.position = targetCam.transform.position;
        follow.rotation = targetCam.transform.rotation;
        transform.localEulerAngles = new Vector3(0, follow.localEulerAngles.y, 0);
    }
    ////public void noTarget()
    ////{
    ////    targetPlayer = null;
    ////    //UpdateFocus();
    ////    virtualCam.Priority = 8;
    ////    cinemachineFreeLook.Priority = 10;
    ////    isFocus();
    ////}
    public void noTarget()
    {
        if (targetPlayer != null)
            TargetActive(false);

        targetPlayer = null;
        zTarget.t = null;

        virtualCam.Priority = 8;
        cinemachineFreeLook.Priority = 10;

        focus = false;
        anim.SetBool("isFocus", false);
    }
    ////combate
    //public bool Attack()
    //{
    //    return !stop && onGround;
    //}
    public bool Attack()
    {
        //Debug.Log("Attack check | stop: " + stop + " | onGround: " + onGround);
        return !stop && onGround;
    }
}

