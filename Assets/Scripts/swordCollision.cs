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
        if(other.tag == "Target")
        {
            if(other.transform.GetComponent<targetDamage>() != null)
            {
                cinemachineImpulse.GenerateImpulse(Camera.main.transform.forward);
                if (other.transform.GetComponent<targetDamage>().player)
                    other.transform.GetComponent<targetDamage>().player = playerCombat.gameObject;
                other.transform.GetComponent<Life>().GetHit(attack);
            }
        }
    }
}
