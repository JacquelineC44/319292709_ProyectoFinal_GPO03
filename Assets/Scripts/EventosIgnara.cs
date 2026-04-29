using UnityEngine;

public class EventosIgnara : MonoBehaviour
{
    PlayerMotion playerMotion;
    PlayerCombat playerCombat;
    public void Awake()
    {
        playerMotion = GetComponentInParent<PlayerMotion>();
        playerCombat = GetComponentInParent<PlayerCombat>();
    }
    public void Land()
    {
        playerMotion.FallEnd();
    }
    public void rollStop()
    {
        playerMotion.rollStop();
    }
    public void Hit()
    {
        playerCombat.Hit();
    } 
    public void Shoot()
    {
        playerCombat.Shoot();
    }
    public void healEnd()
    {
        playerCombat.healEnd();
    }
    public void Fire()
    {
        playerCombat.Fire();
    }
}
