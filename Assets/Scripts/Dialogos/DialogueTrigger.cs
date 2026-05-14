using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueManager dialogueManager;
    public string texto;
    public AudioClip voz;
    public bool soloUnaVez = true;

    private bool yaSeActivo = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (soloUnaVez && yaSeActivo) return;

            dialogueManager.MostrarDialogo(texto, voz);
            yaSeActivo = true;
        }
    }
}