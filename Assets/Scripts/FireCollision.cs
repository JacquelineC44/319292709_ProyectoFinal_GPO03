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
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" || other.tag == "Shield")
            return;
        if (other.tag == "Target")
            other.GetComponent<Life>().GetHit(damage);
        cinemachineImpulse.GenerateImpulse(Camera.main.transform.forward);
        explosion.transform.parent = null;
        explosion.SetActive(true);
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        if (gameObject == null)
            return;
        if(explosion != null)
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
