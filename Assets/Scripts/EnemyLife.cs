using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using DG.Tweening;

public class EnemyLife : Life
{
    public GameObject text;
    public Transform pointDamage;
    public GameObject particleDamage, particleDead;
    public bool inDamage;
    public NavMeshAgent agent;

    EnemyMotion enemyMotion;
    EnemyCombat enemyCombat;

    private void Awake()
    {
        enemyMotion = GetComponent<EnemyMotion>();
        enemyCombat = GetComponent<EnemyCombat>();
        agent = GetComponent<NavMeshAgent>();
    }

    public override void GetHit(int damage)
    {
        if (inDamage)
            return;

        base.GetHit(damage);
        inDamage = true;

        enemyMotion.Stopping();
        enemyMotion.ResetEnemy();
        StopCoroutine("AttackAgain");
        anim.Rebind();
        anim.SetInteger("Life", currentLife);
        anim.SetTrigger("Hit");
        particleDamage.SetActive(false);
        particleDamage.SetActive(true);
        GameObject t = Instantiate(text, UIManager.Instance.transform);
        t.GetComponent<Text>().text = damage.ToString();
        Vector3 tPosition = Camera.main.WorldToScreenPoint(pointDamage.position);
        t.GetComponent<RectTransform>().position = tPosition + (Vector3.left * t.GetComponent<RectTransform>().sizeDelta.x/2f);
        DG.Tweening.Sequence time = DOTween.Sequence();
        Time.timeScale = .4f;
        time.AppendInterval(.03f).OnComplete(() => Time.timeScale = 1f).SetUpdate(true);
        float y = t.GetComponent<RectTransform>().position.y;
        t.GetComponent<RectTransform>().DOMoveY(y + 250f, 1f);
        t.GetComponent<Text>().DOFade(0, 1f).OnComplete(() => Destroy(t));
        DG.Tweening.Sequence s = DOTween.Sequence();
        s.AppendInterval(.5f).OnComplete(() => inDamage = false);
        StartCoroutine("AttackAgain");
    }
    IEnumerator AttackAgain()
    {
        yield return new WaitForSeconds(.5f);

        if (currentLife <= 0)
        {
            player.GetComponent<PlayerMotion>().isFocus();
            enemyMotion.enabled = false;
            //enemyCombat.enabled = false;
            this.enabled = false;

            yield return new WaitForSeconds(.2f);

            particleDead.transform.parent = null;
            particleDead.SetActive(true);

            Destroy(particleDead, .1f);
            Destroy(gameObject);
        }
        else
        {
            enemyCombat.isAttacking = false;

            if (enemyMotion.player == null)
            {
                enemyMotion.Enconter();
            }
            else
            {
                enemyMotion.StopEnd();
            }
        }
    }
    private void OnDestroy()
    {
        if(player != null)
        {
            player.GetComponent<PlayerMotion>().noTarget();
            player = null;
        }
    }
}