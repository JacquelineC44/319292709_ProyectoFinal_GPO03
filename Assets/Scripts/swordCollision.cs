using UnityEngine;
using Unity.Cinemachine;

public class swordCollision : MonoBehaviour
{
    public int attack;
    public PlayerCombat playerCombat;
    CinemachineImpulseSource cinemachineImpulse;
    private void Awake()
    {
        cinemachineImpulse = GetComponent<CinemachineImpulseSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Target")
        {
            if (other.transform.GetComponent<Life>() != null)
            {
                cinemachineImpulse.GenerateImpulse(Camera.main.transform.forward);
                if (other.transform.GetComponent<Life>().player == null)
                    other.transform.GetComponent<Life>().player = playerCombat.gameObject;
                other.transform.GetComponent<Life>().GetHit(attack);
            }
        }
    }
}
