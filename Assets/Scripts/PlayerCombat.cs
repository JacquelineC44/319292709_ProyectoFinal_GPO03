using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public items weaponActual;
    public items itemActual;
    public CapsuleCollider swordCollision;
    public LayerMask enemyMask;
    public float focusAtkImpulse;
    public float combo;
    public bool isAttacking;
    PlayerMotion playerMotion;
    Inventario inventory;
    ZTarget ztar;
    Animator anim;
    Rigidbody rb;
    bool heavyAtk;

    private void Awake()
    {
        playerMotion = GetComponent<PlayerMotion>();
        inventory = GetComponent<Inventario>();
        ztar = GetComponent<ZTarget>();
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();

    }
    public void OnAttackL()
    {
        if (weaponActual == null)
            return;
        if(!isAttacking && playerMotion.Attack())
        {
            isAttacking = true;
            StopCoroutine("moveAgain");
            StopCoroutine("comboEnd");
            rb.linearVelocity = Vector3.zero;
            playerMotion.Stopping();
            anim.SetFloat("Combo", combo);
            anim.SetInteger("Attack", 1);
            anim.SetTrigger("Atk");
            if(combo == 2)
            {
                combo = 0;
            }
            else
            {
                combo++;
            }
            if(playerMotion.targetPlayer != null)
            {
                if (Vector3.Distance(transform.position, playerMotion.targetPlayer.position) > 1f)
                    rb.AddForce(playerMotion.cam.forward * focusAtkImpulse, ForceMode.Impulse);
            }
            StartCoroutine("moveAgain");
            StartCoroutine("comboEnd");
        }
    }
    public void OnAttackP()
    {
        if (weaponActual == null)
            return;
        if(!isAttacking && playerMotion.Attack())
        {
            isAttacking= true;
            StopCoroutine("moveAgain");
            StopCoroutine("comboEnd");
            rb.linearVelocity = Vector3.zero;
            playerMotion.Stopping();
            heavyAtk = true;
            anim.SetInteger("Attack", 2);
            anim.SetTrigger("Atk");
            if(playerMotion.targetPlayer != null)
            {
                if (Vector3.Distance(transform.position, playerMotion.targetPlayer.position) > 1f)
                    rb.AddForce(playerMotion.cam.forward * focusAtkImpulse, ForceMode.Impulse);
            }
            StartCoroutine("moveAgain");
            StartCoroutine("comboEnd");

        }
    }
    public void Hit()
    {
        swordCollision.enabled = true;
        Reset();
        StartCoroutine("moveAgain", (heavyAtk) ? .8f : .5f);
        StartCoroutine("comboEnd");
    }
    IEnumerator comboEnd()
    {
        yield return new WaitForSeconds(1.5f);
        combo = 0;
    }
    IEnumerator moveAgain(float f = .5f)
    {
        yield return new WaitForSeconds(f);
        heavyAtk = false;
        anim.SetInteger("Attack", 0);
        if (swordCollision.enabled)
            swordCollision.enabled = false;
        isAttacking = false;
        playerMotion.StopEnd();
    }
    public void Reset()
    {
        StopCoroutine("moveAgain");
        StopCoroutine("comboEnd");
    }
}