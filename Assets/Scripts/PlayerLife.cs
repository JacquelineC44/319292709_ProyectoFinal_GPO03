using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using Unity.Cinemachine;
using System.Collections;

public class PlayerLife : Life
{
    public PlayerMotion playerMotion;
    public PlayerCombat playerCombat;
    public Inventario inventory;
    public GameObject particleDamage;
    CinemachineImpulseSource cinemachineImpulse;

    private void Awake()
    {
        playerMotion = GetComponent<PlayerMotion>();
        playerCombat = GetComponent<PlayerCombat>();
        inventory = GetComponent<Inventario>();
        cinemachineImpulse = GetComponent<CinemachineImpulseSource>();
    }
    public override void GetHit(int damage)
    {
        if (currentLife == 0)
            return;
        base.GetHit(damage);
        UIManager.Instance.UpdateLifeText();
        StopCoroutine("noHit");
        playerCombat.Reset();
        playerMotion.Stopping();
        if(currentLife > 0)
        {
            StartCoroutine("noHit");
        }
        else
        {
            playerMotion.stop = true;
            playerMotion.enabled= false;
            playerCombat.enabled= false;
            StartCoroutine("death");

        }
        anim.Rebind();
        if (inventory.swordUse)
        {
            anim.SetBool("Weapon", true);
            anim.SetTrigger("SwitchWeapon");
        }
        if(playerCombat.weaponActual != null)
        {
            if (playerCombat.weaponActual.typeItem == WeaponType.sword)
                anim.SetFloat("WeaponN", 0);
            //corregir numero
            if (playerCombat.weaponActual.typeItem == WeaponType.crossbow)
                anim.SetFloat("WeaponN", 2);
        }
        anim.SetInteger("Life", currentLife);
        UIManager.Instance.UpdateLife(currentLife);
        rb.linearVelocity = Vector3.zero;
        particleDamage.SetActive(false);
        particleDamage.SetActive(true);
        cinemachineImpulse.GenerateImpulse(Camera.main.transform.forward);
        anim.SetTrigger("Hit");
        Sequence time = DOTween.Sequence();
        Time.timeScale = 0.4f;
        time.AppendInterval(0.03f).OnComplete(()=> Time.timeScale = 1f).SetUpdate(true);
    }

    IEnumerator noHit()
    {
        yield return new WaitForSeconds(.5f);
        if(currentLife != 0)
        {
            playerCombat.isAttacking = false;
            playerMotion.StopEnd();
        }
    }
    IEnumerator death()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }
}
