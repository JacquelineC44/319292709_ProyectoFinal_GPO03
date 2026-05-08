using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;
using System.Collections;
public enum enemyState
{
    patrolling,
    alert,
    followPlayer,
    attacking,
    searching
}


public class EnemyMotion : MonoBehaviour
{
    public enemyState state;
    public Transform pointOfView;
    public GameObject target;
    public Transform player;
    public Transform[] waypoints;
    public LayerMask playerMask, visibleMask;

    public float viewDistance, speedNormal, speedCombat,
                 angularNormal, angularCombat,
                 stoppingDistance, timeToSearching, radius;

    [Range(0, 360)]
    public float angle;

    public int waypointN;
    public bool playerDetected, stop, run;
    NavMeshAgent agent;
    Animator anim;
    DG.Tweening.Sequence sequence;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        agent.stoppingDistance = 1;
        agent.SetDestination(waypoints[waypointN].position);
    }
    void Update()
    {
        if (stop)
            return;

        machineState();

        anim.SetBool("Move", run);
    }
    private void machineState()
    {
        switch (state)
        {
            case enemyState.patrolling:
                playerDetected = OnPlayerDetect();
                if (!playerDetected)
                {
                    run = (Vector3.Distance(waypoints[waypointN].position, transform.position) > agent.stoppingDistance);
                    if (!run)
                    {
                        Stopping();
                        Vector3 target = waypoints[waypointN].position;
                        Vector3 lookPos = target - transform.position;
                        lookPos.y = 0;
                        Quaternion rotation = Quaternion.LookRotation(lookPos);
                        float dirAnim = (rotation.x > 0) ? 1 : -1;
                        anim.SetFloat("Direction", dirAnim);
                        StartCoroutine(nextWaypoint());
                        return;
                    }
                }
                else
                {
                    Stopping();
                    anim.SetBool("Enconter", true);
                    sequence = DOTween.Sequence();
                    sequence.AppendInterval(1f).OnComplete(() =>
                    {
                        anim.SetBool("Enconter", false);
                        Enconter();
                    });
                }
                break;

            case enemyState.alert:

                run = false;

                playerDetected = OnPlayerDetect();

                if (playerDetected)
                {
                    ResetEnemy();
                    Stopping();

                    anim.Rebind();

                    anim.SetBool("Enconter", true);

                    sequence = DOTween.Sequence();

                    sequence.AppendInterval(1f).OnComplete(() =>
                    {
                        anim.SetBool("Enconter", false);
                        Enconter();
                    });
                }
                break;

            case enemyState.followPlayer:

                if (timeToSearching > 30f)
                {
                    timeToSearching = 0;
                    bool playerView = playerDirect();
                    if (!playerView)
                    {
                        player = null;
                        Stopping();
                        StartCoroutine(nextWaypoint());
                        return;
                    }
                }
                else
                {
                    timeToSearching += Time.deltaTime;
                }
                if (agent.isStopped)
                    agent.isStopped = false;
                if (player == null)
                    OnPlayerDetect();
                agent.SetDestination(player.position);
                run = (Vector3.Distance(waypoints[waypointN].position, transform.position) > agent.stoppingDistance);
                if(Vector3.Distance(player.position, transform.position) <= agent.stoppingDistance)
                {
                    Stopping();
                    state = enemyState.attacking;
                    Vector3 target = player.transform.position;
                    Vector3 lookPos = target - transform.position;
                    lookPos.y = 0;
                    Quaternion rotation = Quaternion.LookRotation(lookPos);
                    transform.rotation = rotation;

                }
                break;

        }
    }

    private bool OnPlayerDetect()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, playerMask);
        if (rangeChecks.Length != 0)        {
            RaycastHit hit;
            Transform playerT = rangeChecks[0].transform;
            Vector3 directionToTarget = ((playerT.position + (Vector3.up * 1.5f)) - pointOfView.position);
            if (Vector3.Angle(pointOfView.forward, directionToTarget) < angle / 2)
            {
                if (Physics.Raycast(pointOfView.position, directionToTarget, out hit, viewDistance, visibleMask))
                {
                    if (hit.collider.tag != "Player")
                        return false;
                    player = hit.collider.transform;
                    return true;
                }
            }
        }
        player = null;
        return false;
    }

    private bool playerDirect()
    {
        if (player == null)
            return false;
        RaycastHit hit;
        Vector3 directionToTarget = ((player.position + (Vector3.up * 1.5f)) - pointOfView.position);

        if (Physics.Raycast(pointOfView.position, directionToTarget, out hit, viewDistance, visibleMask))
        {
            if (hit.collider.tag == "Player")
                return true;
        }
        return false;
    }
    public void Enconter()
    {
        ResetEnemy();
        Stopping();
        state = enemyState.searching;
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, playerMask);

        if (rangeChecks.Length != 0)
        {
            Vector3 target = rangeChecks[0].transform.position;
            Vector3 lookPos = target - transform.position;
            lookPos.y = 0;
            Quaternion rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = rotation;
            playerDetected = OnPlayerDetect();
            agent.speed = speedCombat;
            agent.angularSpeed = angularCombat;
            state = enemyState.followPlayer;
            agent.stoppingDistance = stoppingDistance;
            StopEnd();
        }
        else
        {
            state = enemyState.alert;
            StopEnd();
            
            sequence = DOTween.Sequence();

            sequence.AppendInterval(1f).OnComplete(() =>
            {
                Stopping();
                anim.SetBool("Alert", true);
                state = enemyState.patrolling;
                agent.stoppingDistance = 1;
                agent.speed = speedNormal;
                agent.angularSpeed = angularNormal;
                agent.SetDestination(waypoints[waypointN].position);
                StopEnd();
            });
        }
    }

    public void Stopping()
    {
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
        stop = true;
        run = false;
        anim.SetBool("Move", run);
    }

    public void StopEnd()
    {
        if (state == enemyState.attacking)
        {
            agent.speed = speedCombat;
            agent.angularSpeed = angularCombat;
            state = enemyState.followPlayer;
            agent.stoppingDistance = stoppingDistance;
        }

        stop = false;
        agent.isStopped = false;
    }

    public void ResetEnemy()
    {
        StopCoroutine("nextWaypoint");
        transform.DOKill();
        sequence.Kill();
    }

    IEnumerator nextWaypoint()
    {
        transform.DOLookAt(waypoints[waypointN].position, 1f, AxisConstraint.Y).OnComplete(() =>
        {
            anim.SetFloat("Direction", 0);
            state = enemyState.alert;
            StopEnd();
            anim.SetBool("Alert", true);
        });

        yield return new WaitForSeconds(3.5f);

        Stopping();

        anim.SetBool("Alert", false);

        yield return new WaitForSeconds(1f);

        state = enemyState.patrolling;
        agent.stoppingDistance = 1;
        agent.speed = speedNormal;
        agent.angularSpeed = angularNormal;
        if(waypointN == waypoints.Length - 1)
        {
            waypointN = 0;
        }
        else
        {
            waypointN++;
        }
        agent.SetDestination(waypoints[waypointN].position);
        StopEnd();
    }

}
