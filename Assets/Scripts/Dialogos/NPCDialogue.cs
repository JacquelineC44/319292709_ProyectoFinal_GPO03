using UnityEngine;
using static DialogueManager;

public class NPCDialogue : MonoBehaviour
{
    public DialogueManager dialogueManager;

    [Header("Conversación")]
    public LineaDialogo[] lineasDialogo;

    [Header("Texto sobre el personaje")]
    public GameObject textoInteractuar;

    private bool jugadorCerca;
    private bool conversacionActiva;

    private void Start()
    {
        if (textoInteractuar != null)
            textoInteractuar.SetActive(false);
    }

    public void JugadorEntro()
    {
        jugadorCerca = true;

        if (textoInteractuar != null)
            textoInteractuar.SetActive(true);
    }

    public void JugadorSalio()
    {
        jugadorCerca = false;

        if (textoInteractuar != null)
            textoInteractuar.SetActive(false);
    }

    public void Interactuar()
    {
        if (!jugadorCerca || conversacionActiva)
            return;

        conversacionActiva = true;

        if (textoInteractuar != null)
            textoInteractuar.SetActive(false);

        dialogueManager.MostrarDialogoPorLineas(lineasDialogo);
    }
}