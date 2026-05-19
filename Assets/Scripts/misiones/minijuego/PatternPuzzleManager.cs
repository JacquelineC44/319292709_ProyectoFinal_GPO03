using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternPuzzleManager : MonoBehaviour
{
    public PuzzleCube[] cubos;
    public DissolveDoor puerta;

    public Color colorNormal = Color.white;
    public Color colorActivo = Color.yellow;
    public Color colorCorrecto = Color.green;
    public Color colorError = Color.red;

    public float tiempoEncendido = .6f;
    public float tiempoEntreCubos = .3f;

    private List<int[]> rondas = new List<int[]>();
    private int rondaActual = 0;
    private int pasoJugador = 0;
    private bool aceptandoInput = false;
    private bool puzzleCompleto = false;
    private bool puzzleIniciado = false;

    private void Start()
    {
        rondas.Add(new int[] { 0, 2, 1, 4 });      // ronda de 4
        rondas.Add(new int[] { 3, 0, 4, 1, 2 });   // ronda de 5

        for (int i = 0; i < cubos.Length; i++)
        {
            cubos[i].Configurar(this, i);
            cubos[i].CambiarColor(colorNormal);
        }
    }

    public void IniciarPuzzle()
    {
        if (puzzleCompleto) return;

        StartCoroutine(MostrarPatron());
    }

    IEnumerator MostrarPatron()
    {
        aceptandoInput = false;
        pasoJugador = 0;

        yield return new WaitForSeconds(.5f);

        int[] patron = rondas[rondaActual];

        for (int i = 0; i < patron.Length; i++)
        {
            int index = patron[i];

            cubos[index].CambiarColor(colorActivo);
            cubos[index].SonidoPatron();
            yield return new WaitForSeconds(tiempoEncendido);

            cubos[index].CambiarColor(colorNormal);
            yield return new WaitForSeconds(tiempoEntreCubos);
        }

        aceptandoInput = true;
    }

    public void CuboSeleccionado(int index)
    {
        if (puzzleCompleto)
            return;

        if (!puzzleIniciado)
        {
            puzzleIniciado = true;
            StartCoroutine(MostrarPatron());
            return;
        }

        if (!aceptandoInput)
            return;

        int[] patron = rondas[rondaActual];

        if (index == patron[pasoJugador])
        {
            cubos[index].Flash(colorActivo);
            pasoJugador++;

            if (pasoJugador >= patron.Length)
                StartCoroutine(RondaCorrecta());
        }
        else
        {
            StartCoroutine(ErrorPatron());
        }
    }
    IEnumerator RondaCorrecta()
    {
        aceptandoInput = false;

        foreach (PuzzleCube cubo in cubos)
            cubo.CambiarColor(colorCorrecto);

        yield return new WaitForSeconds(1f);

        rondaActual++;

        if (rondaActual >= rondas.Count)
        {
            puzzleCompleto = true;

            if (puerta != null)
                puerta.AbrirPuerta();

            if (MissionManager.Instance != null)
                MissionManager.Instance.SiguientePaso();
        }
        else
        {
            foreach (PuzzleCube cubo in cubos)
                cubo.CambiarColor(colorNormal);

            yield return new WaitForSeconds(.5f);
            StartCoroutine(MostrarPatron());
        }
    }

    IEnumerator ErrorPatron()
    {
        aceptandoInput = false;

        foreach (PuzzleCube cubo in cubos)
            cubo.CambiarColor(colorError);

        yield return new WaitForSeconds(1f);

        foreach (PuzzleCube cubo in cubos)
            cubo.CambiarColor(colorNormal);

        StartCoroutine(MostrarPatron());
    }
}
