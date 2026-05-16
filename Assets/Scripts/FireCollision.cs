using Unity.Cinemachine;
using UnityEngine;

public class FireCollision : MonoBehaviour
{
    public int damage;
    public GameObject explosion;
    CinemachineImpulseSource cinemachineImpulse;

    private void Awake()
    {
        cinemachineImpulse = GetComponent<CinemachineImpulseSource>();
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.tag == "Player")
    //        return;
    //    if (other.tag == "Target")
    //        other.GetComponent<Life>().GetHit(damage);
    //    cinemachineImpulse.GenerateImpulse(Camera.main.transform.forward);
    //    explosion.transform.parent = null;
    //    explosion.SetActive(true);
    //    Destroy(gameObject);
    //}
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            return;

        if (other.CompareTag("Target"))
        {
            Life life = other.GetComponent<Life>();

            if (life != null)
                life.GetHit(damage);

            Explode();
        }

        if (other.CompareTag("Ground"))
        {
            Explode();
        }
    }

    void Explode()
    {
        if (cinemachineImpulse != null && Camera.main != null)
            cinemachineImpulse.GenerateImpulse(Camera.main.transform.forward);

        explosion.transform.parent = null;
        explosion.SetActive(true);

        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        if (gameObject == null)
            return;
        if (explosion != null)
        {
            if (!explosion.activeSelf)
            {
                explosion.transform.parent = null;
                explosion.SetActive(true);
            }
            Destroy(explosion, 5f);
        }
    }

}
