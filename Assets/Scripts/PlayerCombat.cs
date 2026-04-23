using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Cinemachine;

public class PlayerCombat : MonoBehaviour
{
    public items weaponActual;
    public items itemActual;
    public CapsuleCollider swordCollision;
    public GameObject arrowPrefab;//flechas
    public LayerMask enemyMask;
    public Transform attachPoint;//flechas
    public float focusAtkImpulse;
    public float combo;//flechas
    public float arrowSpeed; //flecha
    public bool isAttacking;
    PlayerMotion playerMotion;
    Inventario inventory;
    ZTarget ztar;
    Animator anim;
    Rigidbody rb;
    CinemachineImpulseSource cinemachineImpulse; //escudo tiembla camara al golpe
    bool heavyAtk;

    private void Awake()
    {
        playerMotion = GetComponent<PlayerMotion>();
        inventory = GetComponent<Inventario>();
        ztar = GetComponent<ZTarget>();
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
        cinemachineImpulse = GetComponent<CinemachineImpulseSource>();

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
            if(weaponActual.typeItem == WeaponType.sword)
                anim.SetInteger("Attack", 1);
            if (weaponActual.typeItem == WeaponType.crossbow)
                anim.SetInteger("Attack", 4);//modificar en mi arbol de anijmacion
            anim.SetTrigger("Atk");
            if(combo == 2)
            {
                combo = 0;
            }
            else
            {
                combo++;
            }
            //flechas
            if(playerMotion.focus && weaponActual.typeItem != WeaponType.crossbow)
            {
                if (playerMotion.targetPlayer != null)
                {
                    if (Vector3.Distance(transform.position, playerMotion.targetPlayer.position) > 1f)
                        rb.AddForce(playerMotion.cam.forward * focusAtkImpulse, ForceMode.Impulse);
                }

            }
            
            StartCoroutine("moveAgain", 1f);
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
            Reset();
            rb.linearVelocity = Vector3.zero;
            playerMotion.Stopping();
            if(weaponActual.typeItem == WeaponType.sword)
            {
                heavyAtk = true;
                anim.SetInteger("Attack", 2);
            }
            if (weaponActual.typeItem == WeaponType.crossbow)
            {
                anim.SetInteger("Attack", 4);
            }
            anim.SetTrigger("Atk");
            if (playerMotion.focus && weaponActual.typeItem != WeaponType.crossbow)
            {
                if (playerMotion.targetPlayer != null)
                {
                    if (Vector3.Distance(transform.position, playerMotion.targetPlayer.position) > 1f)
                        rb.AddForce(playerMotion.cam.forward * focusAtkImpulse, ForceMode.Impulse);
                }
            }
                
            StartCoroutine("moveAgain", 1f);
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

    public void Shoot()
    {
        if (ztar.t != null)
            playerMotion.UpdateFocus();
        if (inventory.arrows != 0)
        {
            GameObject arrow = Instantiate(arrowPrefab, null);
            arrow.transform.position = attachPoint.position;
            arrow.transform.rotation = arrowPrefab.transform.rotation;
            arrow.SetActive(true);
            arrow.GetComponent<arrowCollision>().cinemachineImpulse = cinemachineImpulse;
            arrow.GetComponent<arrowCollision>().damage = weaponActual.pto;
            //focus
            if(playerMotion.targetPlayer != null)
            {
                arrow.transform.LookAt(playerMotion.targetPlayer);
                Vector3 targetDir = arrow.transform.forward * arrowSpeed * 2f;
                arrow.GetComponent<Rigidbody>().AddForce(targetDir);
            }
            else
            {
                Vector3 targetDir = arrow.transform.forward * arrowSpeed * 2f;
                arrow.GetComponent<Rigidbody>().AddForce(targetDir);
            }
            Destroy(arrow, 5f);
            inventory.arrows--;
            UIManager.Instance.UpdateArrows(inventory.arrows);
        }
        StartCoroutine("moveAgain", .5f);
    }
    public void OnArrowL()
    {
        if (weaponActual == null)
            return;
        if(!isAttacking && playerMotion.Attack())
        {
            StopCoroutine("comboEnd");
            if (inventory.weapons.Count < 2)
                return;
            for(int i = inventory.weapons.Count -1; i >= 0; i--)
            {
                if (inventory.weapons[i] == weaponActual)
                {
                    weaponActual = (i == 0) ? inventory.weapons[inventory.weapons.Count - 1] : inventory.weapons[i - 1];
                    break;
                }
            }
        }
    }
    public void OnArrowR()
    {
        if (weaponActual == null)
            return;
        if (!isAttacking && playerMotion.Attack())
        {
            StopCoroutine("comboEnd");
            if (inventory.weapons.Count < 2)
                return;
            for (int i = 0; i < inventory.weapons.Count; i++)
            {
                if (inventory.weapons[i] == weaponActual)
                {
                    weaponActual = (i == inventory.weapons.Count - 1) ? inventory.weapons[0] : inventory.weapons[i + 1];
                    break;
                }
            }
            ActiveWeapon();
        }
    }
    public void ActiveWeapon()
    {
        if(weaponActual.typeItem == WeaponType.sword)
        {
            inventory.swordActive(weaponActual);
        }
        else if(weaponActual.typeItem == WeaponType.crossbow)
        {
            inventory.crossbowActive(weaponActual);
        }
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