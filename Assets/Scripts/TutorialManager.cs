using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    [System.Serializable]
    public class TutorialStep
    {
        public string accion;
        [TextArea] public string mensaje;
    }

    public GameObject panelTutorial;
    public TMP_Text textoTutorial;
    public List<TutorialStep> pasos = new List<TutorialStep>();

    public bool iniciarAlComenzar = false;

    private int pasoActual = 0;
    private bool activo = false;

    void Start()
    {
        panelTutorial.SetActive(false);

        if (iniciarAlComenzar)
            IniciarSecuencia();
    }

    public void IniciarSecuencia()
    {
        if (pasos.Count == 0) return;

        pasoActual = 0;
        activo = true;

        MostrarPaso();
    }

    void MostrarPaso()
    {
        panelTutorial.SetActive(true);
        textoTutorial.text = pasos[pasoActual].mensaje;
    }

    public void CompletarAccion(string accion)
    {
        if (!activo) return;

        if (accion != pasos[pasoActual].accion)
            return;

        pasoActual++;

        if (pasoActual >= pasos.Count)
        {
            TerminarSecuencia();
        }
        else
        {
            MostrarPaso();
        }
    }

    void TerminarSecuencia()
    {
        activo = false;
        panelTutorial.SetActive(false);
    }
}