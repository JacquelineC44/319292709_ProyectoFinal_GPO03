using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class EnemyCrossbowCombat : EnemyCombat
{
    public GameObject arrowPrefab;
    public Transform attachPoint;
    public float arrowSpeed;

    public override void Attack()
    {
        if (isAttacking)
            return;

        isAttacking = true;
        anim.SetInteger("Attack", 0);
        anim.SetTrigger("Atk");
    }

    public override void Hit()
    {
        GameObject arrow = Instantiate(arrowPrefab, null);

        arrow.transform.position = attachPoint.position;
        arrow.transform.rotation = attachPoint.rotation;
        arrow.transform.localScale = arrowPrefab.transform.localScale;
        arrow.SetActive(true);

        arrow.GetComponent<ArrowCollisionE>().damage = atkDamage1;

        if (enemyMotion.player != null)
        {
            arrow.transform.LookAt(enemyMotion.player.position + (Vector3.up * 1.5f));
            Vector3 targetDir = arrow.transform.forward * arrowSpeed * 2f;
            arrow.GetComponent<Rigidbody>().AddForce(targetDir);
        }
        else
        {
            Vector3 targetDir = arrow.transform.forward * arrowSpeed * 2f;
            arrow.GetComponent<Rigidbody>().AddForce(targetDir);
        }
        Destroy(arrow, 5f);
        DG.Tweening.Sequence s = DOTween.Sequence();
        s.AppendInterval(1.5f).OnComplete(() =>
        {
            isAttacking = false;
            enemyMotion.StopEnd();
        });
    }
}
