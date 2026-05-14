using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public GameObject panelDialogo;
    public TMP_Text textoDialogo;
    public AudioSource audioSource;

    public void MostrarDialogo(string texto, AudioClip voz)
    {
        panelDialogo.SetActive(true);
        textoDialogo.text = texto;

        if (voz != null)
        {
            audioSource.clip = voz;
            audioSource.Play();
        }
    }

    public void CerrarDialogo()
    {
        panelDialogo.SetActive(false);
        audioSource.Stop();
    }
}