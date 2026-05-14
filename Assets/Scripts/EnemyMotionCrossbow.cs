using DG.Tweening;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyMotionCrossbow : EnemyMotion
{
    void Update()
    {
        if (stop)
            return;

        machineState();

        anim.SetBool("Move", run);
    }
    protected override void machineState()
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
                        StartCoroutine("nextWaypoint");
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

                if (agent.pathPending)
                {
                    timeToSearching = 0;
                    bool playerView = playerDirect();
                    if (!playerView)
                    {
                        agent.ResetPath();
                        player = null;
                        Stopping();
                        StartCoroutine("nextWaypoint");
                        return;

                    }
                }
                if (timeToSearching > 30f)
                {
                    timeToSearching = 0;
                    bool playerView = playerDirect();
                    if (!playerView)
                    {
                        agent.ResetPath();
                        player = null;
                        Stopping();
                        StartCoroutine("nextWaypoint");
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
                if (Vector3.Distance(player.position, transform.position) <= agent.stoppingDistance)
                {
                    Stopping();
                    state = enemyState.attacking;
                    Vector3 target = player.transform.position;
                    Vector3 lookPos = target - transform.position;
                    lookPos.y = 0;
                    Quaternion rotation = Quaternion.LookRotation(lookPos);
                    transform.rotation = rotation;
                    enemyCombat.Attack();
                }
                break;
            case enemyState.attacking:
                run = (Vector3.Distance(waypoints[waypointN].position, transform.position) > agent.stoppingDistance);

                if (Vector3.Distance(player.position, transform.position) >= agent.stoppingDistance)
                {
                    agent.stoppingDistance = stoppingDistance;
                    Stopping();

                    Vector3 target = player.position;
                    Vector3 lookPos = target - transform.position;
                    lookPos.y = 0;

                    Quaternion rotation = Quaternion.LookRotation(lookPos);
                    transform.rotation = rotation;

                    enemyCombat.Attack();

                    return;
                }
                if (Vector3.Distance(waypoints[waypointN].position, transform.position) <= 1f)
                {
                    if (waypointN == waypoints.Length - 1)
                    {
                        waypointN = 0;
                    }
                    else
                    {
                        waypointN++;
                    }
                    Stopping();

                    Vector3 target = player.position;
                    Vector3 lookPos = target - transform.position;
                    lookPos.y = 0;
                    Quaternion rotation = Quaternion.LookRotation(lookPos);
                    transform.rotation = rotation;
                    StopEnd();
                }
                break;
            default:
                break;


        }
    }
    public override void StopEnd()
    {
        if (state == enemyState.attacking)
        {
            agent.speed = speedCombat;
            agent.angularSpeed = angularCombat;

            if (Vector3.Distance(player.position, transform.position) <= 2f)
            {
                state = enemyState.attacking;
                agent.stoppingDistance = 1f;
                agent.SetDestination(waypoints[waypointN].position);
            }
            else
            {
                agent.stoppingDistance = stoppingDistance;
                state = enemyState.followPlayer;
            }
        }
        stop = false;
        agent.isStopped = false;
    }
}
