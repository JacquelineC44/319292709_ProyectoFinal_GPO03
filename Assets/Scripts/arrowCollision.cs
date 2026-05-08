using UnityEngine;
using Unity.Cinemachine;

public class arrowCollision : MonoBehaviour
{
    public int damage;
    public CinemachineImpulseSource cinemachineImpulse;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Target")
        {
            cinemachineImpulse.GenerateImpulse(Camera.main.transform.forward);
            //other.transform.GetComponent<targetDamage>().Damage(damage);
            other.transform.GetComponent<Life>().GetHit(damage);
            Destroy(gameObject);
        }
        if(other.tag == "Ground")
        {
            Destroy(gameObject);
        }
    }
}
