using System.Collections;
using UnityEngine;

public class PuzzleCube : MonoBehaviour
{
    private PatternPuzzleManager manager;
    private int index;
    private Renderer rend;

    public AudioSource audioSource;
    public AudioClip sonidoSeleccion;
    public AudioClip sonidoMostrarPatron;


    private void Awake()
    {
        rend = GetComponent<Renderer>();
    }

    public void Configurar(PatternPuzzleManager puzzleManager, int cuboIndex)
    {
        manager = puzzleManager;
        index = cuboIndex;
    }
    public void ReproducirSonido()
    {
        if (audioSource != null && sonidoSeleccion != null)
            audioSource.PlayOneShot(sonidoSeleccion);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        SonidoJugador();

        if (manager != null)
            manager.CuboSeleccionado(index);
    }

    public void CambiarColor(Color color)
    {
        rend.material.color = color;
    }

    public void Flash(Color color)
    {
        StopAllCoroutines();
        StartCoroutine(FlashCoroutine(color));
    }

    IEnumerator FlashCoroutine(Color color)
    {
        Color original = rend.material.color;
        CambiarColor(color);

        yield return new WaitForSeconds(.25f);

        CambiarColor(original);
    }

    public void SonidoJugador()
    {
        if (audioSource != null && sonidoSeleccion != null)
            audioSource.PlayOneShot(sonidoSeleccion);
    }

    public void SonidoPatron()
    {
        if (audioSource != null && sonidoMostrarPatron != null)
            audioSource.PlayOneShot(sonidoMostrarPatron);
    }
}