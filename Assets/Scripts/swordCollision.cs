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
        Debug.Log("ENTER espada tocó: " + other.name +
                  " tag: " + other.tag +
                  " tiempo: " + Time.time);

        TryDamage(other);
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("STAY espada sigue tocando: " + other.name +
                  " tag: " + other.tag +
                  " tiempo: " + Time.time);

        TryDamage(other);
    }
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("EXIT espada dejó de tocar: " + other.name +
                  " tag: " + other.tag +
                  " tiempo: " + Time.time);
    }
    void TryDamage(Collider other)
    {
        
        if (!enabled)
            return;
        Debug.Log("TRY DAMAGE con: " + other.name +
              " tag: " + other.tag +
              " tiempo: " + Time.time);

        if (!other.CompareTag("Target"))
        {
            Debug.Log("NO DAÑA: no tiene tag Target");
            return;
        }

        Life life = other.GetComponent<Life>();

        if (life == null)
        {
            Debug.Log("TOCÓ TARGET pero no encontró Life");
            return;
        }

        Debug.Log("SÍ HACE DAÑO A: " + other.name);

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
