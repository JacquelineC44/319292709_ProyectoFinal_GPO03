using UnityEngine;

public class EventosIgnara : MonoBehaviour
{
    PlayerMotion playerMotion;
    PlayerCombat playerCombat;

    public AudioSource footAudio;
    public AudioClip[] footstepClips;

    public void Awake()
    {
        playerMotion = GetComponentInParent<PlayerMotion>();
        playerCombat = GetComponentInParent<PlayerCombat>();
    }
    public void rollStop()
    {
        playerMotion.rollStop();
    }
    public void Hit()
    {
        Debug.Log("EVENTO HIT desde: " + gameObject.name +
          " instance: " + GetInstanceID());
        playerCombat.Hit();
    }

    public void EndHit()
    {
        playerCombat.EndHit();
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
    public void FootR()
    {
        PlayFootstep();
    }
    public void FootL()
    {
        PlayFootstep();
    }
    void PlayFootstep()
    {
        if (footAudio == null) return;
        if (footstepClips.Length == 0) return;

        int random = Random.Range(0, footstepClips.Length);
        footAudio.PlayOneShot(footstepClips[random]);
    }
}
