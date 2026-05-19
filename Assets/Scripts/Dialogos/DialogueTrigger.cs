//using UnityEngine;

//public class DialogueTrigger : MonoBehaviour
//{
//    public DialogueManager dialogueManager;
//    public string texto;
//    public AudioClip voz;
//    public bool soloUnaVez = true;

//    private bool yaSeActivo = false;

//    private void OnTriggerEnter(Collider other)
//    {
//        if (other.CompareTag("Player"))
//        {
//            if (soloUnaVez && yaSeActivo) return;

//            dialogueManager.MostrarDialogo(texto, voz);
//            yaSeActivo = true;
//        }
//    }
//}

using UnityEngine;
using System.Collections;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueManager dialogueManager;

    [Header("Diálogo")]
    [TextArea(2, 4)]
    public string texto;
    public AudioClip voz;

    [Header("Opciones")]
    public bool soloUnaVez = true;

    [Header("Secuencia enemigos")]
    public SecuenciaDialogoEnemigos secuencia;
    public bool alTerminarActivarSecuenciaEnemigos;

    private bool activado;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (soloUnaVez && activado)
            return;

        activado = true;

        dialogueManager.MostrarDialogo(texto, voz);

        if (alTerminarActivarSecuenciaEnemigos && secuencia != null)
            StartCoroutine(AvisarCuandoTermine());
    }

    IEnumerator AvisarCuandoTermine()
    {
        if (voz != null)
            yield return new WaitForSeconds(voz.length);
        else
            yield return new WaitForSeconds(4f);

        secuencia.TerminoDialogoProtagonista();
    }
}