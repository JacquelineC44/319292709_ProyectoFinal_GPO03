using UnityEngine;

public class EventosIgnara : MonoBehaviour
{
    PlayerMotion playerMotion;
    public void Awake()
    {
        playerMotion = GetComponentInParent<PlayerMotion>();
    }
    public void Land()
    {
        playerMotion.FallEnd();
    }
}
