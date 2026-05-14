using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ArrowCollisionE : MonoBehaviour
{
    public int damage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<Life>().GetHit(damage);
            Destroy(gameObject);
        }

        //if (other.tag == "Shield")
        //{
        //    other.GetComponentInParent<PlayerCombat>().Block();
        //    Destroy(gameObject);
        //}

        if (other.tag == "Ground")
        {
            Destroy(gameObject);
        }
    }
}
