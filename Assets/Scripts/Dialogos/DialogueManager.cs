//using UnityEngine;
//using UnityEngine.UI;
//using TMPro;
//using System.Collections;


//public class DialogueManager : MonoBehaviour
//{
//    public GameObject panelDialogo;
//    public TMP_Text textoDialogo;
//    public AudioSource audioSource;

//    public float tiempoAntesDeCerrar = 3f;
//    private Coroutine cerrarCoroutine;

//    public void MostrarDialogo(string texto, AudioClip voz)
//    {
//        panelDialogo.SetActive(true);
//        textoDialogo.text = texto;

//        if (voz != null)
//        {
//            audioSource.clip = voz;
//            audioSource.Play();
//        }

//        if (cerrarCoroutine != null)
//            StopCoroutine(cerrarCoroutine);

//        cerrarCoroutine = StartCoroutine(CerrarDespues());
//    }

//    IEnumerator CerrarDespues()
//    {
//        yield return new WaitForSeconds(tiempoAntesDeCerrar);
//        CerrarDialogo();
//    }

//    public void CerrarDialogo()
//    {
//        panelDialogo.SetActive(false);
//        if (audioSource != null)
//            audioSource.Stop();
//    }
//}

using UnityEngine;
using TMPro;
using System.Collections;

public class DialogueManager : MonoBehaviour
{
    public GameObject panelDialogo;
    public TMP_Text textoDialogo;
    public AudioSource audioSource;

    private Coroutine cerrarCoroutine;

    public void MostrarDialogo(string texto, AudioClip voz)
    {
        panelDialogo.SetActive(true);
        textoDialogo.text = texto;

        if (cerrarCoroutine != null)
            StopCoroutine(cerrarCoroutine);

        if (voz != null)
        {
            audioSource.Stop();
            audioSource.clip = voz;
            audioSource.Play();

            cerrarCoroutine = StartCoroutine(CerrarCuandoTermineAudio(voz.length));
        }
        else
        {
            cerrarCoroutine = StartCoroutine(CerrarDespuesDeTiempo(4f));
        }
    }

    IEnumerator CerrarCuandoTermineAudio(float duracion)
    {
        yield return new WaitForSeconds(duracion);
        panelDialogo.SetActive(false);
    }

    IEnumerator CerrarDespuesDeTiempo(float tiempo)
    {
        yield return new WaitForSeconds(tiempo);
        panelDialogo.SetActive(false);
    }
}