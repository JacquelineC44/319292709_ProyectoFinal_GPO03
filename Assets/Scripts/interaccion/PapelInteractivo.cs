using UnityEngine;

public class PapelInteractivo : MonoBehaviour
{
    public GameObject panelMensaje;

    [Header("Diálogo al cerrar")]
    public DialogueManager dialogueManager;

    [TextArea(2, 4)]
    public string textoProtagonista;

    public AudioClip vozProtagonista;

    private bool mensajeAbierto;

    public void Interactuar()
    {
        mensajeAbierto = !mensajeAbierto;

        panelMensaje.SetActive(mensajeAbierto);

        if (!mensajeAbierto)
        {
            dialogueManager.MostrarDialogo(textoProtagonista, vozProtagonista);
        }
    }
}