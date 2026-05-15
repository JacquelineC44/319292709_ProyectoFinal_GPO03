//using UnityEngine;
//using Unity.Cinemachine;

//public class arrowCollision : MonoBehaviour
//{
//    public int damage;
//    public GameObject player;
//    public CinemachineImpulseSource cinemachineImpulse;

//    private void OnTriggerEnter(Collider other)
//    {
//        if (other.tag == "Target")
//        {
//            cinemachineImpulse.GenerateImpulse(Camera.main.transform.forward);
//            if (other.transform.GetComponent<Life>().player == null)
//                other.transform.GetComponent<Life>().player = player;
//            //other.transform.GetComponent<targetDamage>().Damage(damage);
//            other.transform.GetComponent<Life>().GetHit(damage);
//            Destroy(gameObject);
//        }
//        if(other.tag == "Ground")
//        {
//            Destroy(gameObject);
//        }
//    }
//}
using UnityEngine;
using Unity.Cinemachine;

public class arrowCollision : MonoBehaviour
{
    public int damage;
    public GameObject player;

    private CinemachineImpulseSource cinemachineImpulse;

    private void Awake()
    {
        cinemachineImpulse = GetComponent<CinemachineImpulseSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Target"))
        {
            Life life = other.GetComponent<Life>();

            if (life == null)
                return;

            if (cinemachineImpulse != null && Camera.main != null)
                cinemachineImpulse.GenerateImpulse(Camera.main.transform.forward);

            if (life.player == null)
                life.player = player;

            life.GetHit(damage);

            Destroy(gameObject);
        }

        if (other.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}