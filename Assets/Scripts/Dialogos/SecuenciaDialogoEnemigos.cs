using UnityEngine;
using static DialogueManager;

public class SecuenciaDialogoEnemigos : MonoBehaviour
{
    public DialogueManager dialogueManager;

    [Header("Diálogo de enemigos")]
    public LineaDialogo[] lineasEnemigos;

    private bool dialogoProtagonistaTermino;
    private bool jugadoraEscondida;
    private bool dialogoEnemigosIniciado;

    private bool secuenciaConsumida;

    public void TerminoDialogoProtagonista()
    {
        Debug.Log("Terminó diálogo protagonista");
        dialogoProtagonistaTermino = true;
        IntentarIniciarDialogoEnemigos();
    }

    public void JugadoraSeEscondio()
    {
        Debug.Log("Jugadora está escondida");
        jugadoraEscondida = true;
        IntentarIniciarDialogoEnemigos();
    }

    public void JugadoraSalioDelEscondite()
    {
        Debug.Log("Jugadora salió del escondite");
        jugadoraEscondida = false;

        if (dialogoEnemigosIniciado)
        {
            dialogueManager.CerrarDialogo();
            dialogoEnemigosIniciado = false;
        }
    }


    void IntentarIniciarDialogoEnemigos()
    {
        if (secuenciaConsumida)
            return;

        if (dialogoEnemigosIniciado)
            return;

        if (dialogoProtagonistaTermino && jugadoraEscondida)
        {
            secuenciaConsumida = true; 
            dialogoEnemigosIniciado = true;

            dialogueManager.MostrarDialogoPorLineas(lineasEnemigos);
        }
    }
}