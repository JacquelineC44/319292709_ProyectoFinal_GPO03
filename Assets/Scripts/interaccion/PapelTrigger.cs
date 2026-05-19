using UnityEngine;

public class PapelTrigger : MonoBehaviour
{
    public PapelInteractivo papel;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerMotion>().papel = papel;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMotion player = other.GetComponent<PlayerMotion>();

            if (player.papel == papel)
                player.papel = null;
        }
    }
}
