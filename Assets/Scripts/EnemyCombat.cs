using UnityEngine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;

public class EnemyCombat : MonoBehaviour
{
    public bool isAttacking;
    public LayerMask playerMask;
    public Transform swordPoint;
    public float swordArea = 1f;
    public int atkDamage1, atkDamage2;
    public int atkN, atkC;

    protected EnemyMotion enemyMotion;
    protected Animator anim;
    public float tiempoEntreAtaques = 1f;
    private bool yaHizoDaño;
    public bool ataqueBloqueado;

    private void Awake()
    {
        enemyMotion = GetComponent<EnemyMotion>();
        anim = GetComponentInChildren<Animator>();
    }

    //public virtual void Attack()
    //{
    //    if (isAttacking)
    //        return;

    //    isAttacking = true;
    //    atkN = Random.Range(0, 2);
    //    anim.SetInteger("Attack", atkN);
    //    anim.SetTrigger("Atk");
    //}
    //public virtual void Attack()
    //{
    //    Debug.Log("INTENTA ATACAR. isAttacking = " + isAttacking);

    //    if (isAttacking)
    //    {
    //        Debug.Log("NO ATACA porque isAttacking sigue en TRUE");
    //        return;
    //    }

    //    Debug.Log("SI ATACA. Se activa animación");

    //    isAttacking = true;
    //    atkC = 0;

    //    atkN = Random.Range(0, 2);
    //    Debug.Log("Ataque elegido: " + atkN);

    //    anim.SetInteger("Attack", atkN);
    //    anim.SetTrigger("Atk");
    //}
    //public virtual void Attack()
    //{
    //    if (isAttacking)
    //        return;

    //    isAttacking = true;
    //    yaHizoDaño = false;

    //    anim.SetInteger("Attack", 0);
    //    anim.SetTrigger("Atk");
    //}
    public virtual void Attack()
    {
        if (isAttacking || ataqueBloqueado)
            return;

        isAttacking = true;
        yaHizoDaño = false;

        anim.SetInteger("Attack", 0);
        anim.SetTrigger("Atk");
    }

    //public virtual void Hit()
    //{
    //    Debug.Log("EVENTO HIT EJECUTADO");
    //    if (atkN == 0)
    //    {
    //        Combo1();
    //    }
    //    else
    //    {
    //        Combo2();
    //    }
    //}
    public virtual void Hit()
    {
        AtaqueSimple();
    }
    //public void AtaqueSimple()
    //{
    //    Collider[] rangeChecks = Physics.OverlapSphere(swordPoint.position, swordArea, playerMask);

    //    foreach (Collider col in rangeChecks)
    //    {
    //        PlayerLife playerLife = col.GetComponentInParent<PlayerLife>();

    //        if (playerLife != null)
    //        {
    //            playerLife.GetHit(atkDamage1);
    //            return;
    //        }
    //    }
    //}
    public void AtaqueSimple()
    {
        if (yaHizoDaño)
            return;

        Collider[] rangeChecks = Physics.OverlapSphere(swordPoint.position, swordArea, playerMask);

        foreach (Collider col in rangeChecks)
        {
            PlayerLife playerLife = col.GetComponentInParent<PlayerLife>();

            if (playerLife != null)
            {
                yaHizoDaño = true;
                playerLife.GetHit(atkDamage1);
                return;
            }
        }
    }

    //public void EndAttack()
    //{
    //    isAttacking = false;
    //    atkC = 0;
    //    enemyMotion.StopEnd();
    //}
    public void EndAttack()
    {
        StartCoroutine(EsperarParaVolverAtacar());
    }

    IEnumerator EsperarParaVolverAtacar()
    {
        yield return new WaitForSeconds(tiempoEntreAtaques);

        isAttacking = false;
        atkC = 0;
        enemyMotion.StopEnd();
    }

    //void Combo1()
    //{
    //    Collider[] rangeChecks = Physics.OverlapSphere(swordPoint.position, swordArea, playerMask);

    //    if (rangeChecks.Length > 0)
    //    {
    //        RaycastHit hit;

    //        Physics.Raycast(
    //            enemyMotion.pointOfView.position,
    //            enemyMotion.pointOfView.forward,
    //            out hit,
    //            2.5f,
    //            playerMask
    //        );

    //        if (hit.collider != null)
    //        {
    //            hit.collider.GetComponent<PlayerLife>().GetHit(atkDamage1);
    //        }
    //    }

    //    Debug.Log("COMBO1 TERMINA, libera ataque");

    //    isAttacking = false;
    //    atkC = 0;
    //    enemyMotion.StopEnd();
    //}

    void Combo1()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(swordPoint.position, swordArea, playerMask);

        if (rangeChecks.Length > 0)
        {
            PlayerLife playerLife = rangeChecks[0].GetComponent<PlayerLife>();

            if (playerLife != null)
                playerLife.GetHit(atkDamage1);
        }
        atkC = 1;
        anim.SetInteger("Attack", 2);
        //StartCoroutine(EsperarSiguienteAtaque());
    }
    private void OnDrawGizmosSelected()
    {
        if (swordPoint == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(swordPoint.position, swordArea);
    }
    //void Combo2()
    //{
    //    Vector3 sword = swordPoint.position;
    //    int damage = (atkC == 0) ? atkDamage1 : atkDamage2;

    //    Collider[] rangeChecks = Physics.OverlapSphere(swordPoint.position, swordArea, playerMask);

    //    if (rangeChecks.Length > 0)
    //    {
    //        RaycastHit hit;

    //        Physics.Raycast(enemyMotion.pointOfView.position, enemyMotion.pointOfView.forward, out hit, 2.5f, playerMask);
    //        if(hit.collider != null)
    //        {
    //            //if (hit.collider.tag == "Shield")
    //            //{
    //            //    hit.collider.GetComponentInParent<PlayerCombat>().Block();
    //            //}
    //            //else
    //            //{
    //            hit.collider.GetComponent<PlayerLife>().GetHit(damage);
    //            //}

    //        }


    //    }

    //    if (atkC >= 1)
    //    {
    //        Debug.Log("COMBO2 llamado. atkC = " + atkC);
    //        Sequence s = DOTween.Sequence();

    //        s.AppendInterval(1.5f).OnComplete(() =>
    //        {
    //            atkC = 0;
    //            isAttacking = false;
    //            enemyMotion.StopEnd();
    //        });
    //    }

    //    atkC++;
    //}
    //void Combo2()
    //{
    //    int damage = (atkC == 0) ? atkDamage1 : atkDamage2;

    //    Collider[] rangeChecks = Physics.OverlapSphere(swordPoint.position, swordArea, playerMask);

    //    if (rangeChecks.Length > 0)
    //    {
    //        PlayerLife playerLife = rangeChecks[0].GetComponent<PlayerLife>();

    //        if (playerLife != null)
    //            playerLife.GetHit(damage);
    //    }

    //    if (atkC >= 1)
    //    {
    //        StartCoroutine(EsperarSiguienteAtaque());
    //    }

    //    atkC++;
    //}
    void Combo2()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(swordPoint.position, swordArea, playerMask);

        if (rangeChecks.Length > 0)
        {
            PlayerLife playerLife = rangeChecks[0].GetComponentInParent<PlayerLife>();

            if (playerLife != null)
                playerLife.GetHit(atkDamage2);
        }

        StartCoroutine(EsperarSiguienteAtaque());
    }
    IEnumerator EsperarSiguienteAtaque()
    {
        yield return new WaitForSeconds(1.5f);

        atkC = 0;
        isAttacking = false;
        enemyMotion.StopEnd();
    }
    public void BloquearAtaquePorGolpe(float tiempo)
    {
        StartCoroutine(BloquearAtaqueCoroutine(tiempo));
    }

    IEnumerator BloquearAtaqueCoroutine(float tiempo)
    {
        ataqueBloqueado = true;
        isAttacking = false;

        yield return new WaitForSeconds(tiempo);

        ataqueBloqueado = false;
    }
}
