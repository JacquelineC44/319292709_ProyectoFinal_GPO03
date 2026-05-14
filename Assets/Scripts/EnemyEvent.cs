using UnityEngine;

public class EnemyEvent : MonoBehaviour
{
    public EnemyCombat enemyCombat;

    private void Awake()
    {
        enemyCombat = GetComponent<EnemyCombat>();
    }

    public void Hit()
    {
        enemyCombat.Hit();
    }
    public void Shoot()
    {
        enemyCombat.Hit();
    }
    public void FootL()
    {

    }
    public void FootR()
    {

    }
}
