using UnityEngine;

public class NPCDialogueTrigger : MonoBehaviour
{
    public NPCDialogue npcDialogue;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            npcDialogue.JugadorEntro();
            other.GetComponent<PlayerMotion>().npcDialogue = npcDialogue;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            npcDialogue.JugadorSalio();

            PlayerMotion player = other.GetComponent<PlayerMotion>();

            if (player.npcDialogue == npcDialogue)
                player.npcDialogue = null;
        }
    }
}