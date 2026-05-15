using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class swordCollision : MonoBehaviour
{
    public int attack;
    public PlayerCombat playerCombat;
    CinemachineImpulseSource cinemachineImpulse;
    private HashSet<Life> enemiesHit = new HashSet<Life>();
    private void Awake()
    {
        cinemachineImpulse = GetComponent<CinemachineImpulseSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        TryDamage(other);
    }

    private void OnTriggerStay(Collider other)
    {
        TryDamage(other);
    }

    void TryDamage(Collider other)
    {
        if (!other.CompareTag("Target"))
            return;

        Life life = other.GetComponent<Life>();

        if (life == null)
            return;

        cinemachineImpulse.GenerateImpulse(Camera.main.transform.forward);

        if (life.player == null)
            life.player = playerCombat.gameObject;

        life.GetHit(attack);
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.tag == "Target")
    //    {
    //        if (other.transform.GetComponent<Life>() != null)
    //        {
    //            cinemachineImpulse.GenerateImpulse(Camera.main.transform.forward);
    //            if (other.transform.GetComponent<Life>().player == null)
    //                other.transform.GetComponent<Life>().player = playerCombat.gameObject;
    //            other.transform.GetComponent<Life>().GetHit(attack);
    //        }
    //    }
    //}
}
