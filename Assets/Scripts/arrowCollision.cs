using UnityEngine;
using Unity.Cinemachine;

public class arrowCollision : MonoBehaviour
{
    public int damage;
    public GameObject player;
    public CinemachineImpulseSource cinemachineImpulse;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Target")
        {
            cinemachineImpulse.GenerateImpulse(Camera.main.transform.forward);
            if (other.transform.GetComponent<Life>().player == null)
                other.transform.GetComponent<Life>().player = player;
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
