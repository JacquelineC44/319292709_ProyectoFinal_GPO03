using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    public enum StepType
    {
        Mensaje,
        Accion
    }

    [System.Serializable]
    public class TutorialStep
    {
        public StepType tipo;
        public string accion;
        [TextArea] public string mensaje;
    }

    public GameObject panelTutorial;
    public TMP_Text textoTutorial;
    public List<TutorialStep> pasos = new List<TutorialStep>();

    public bool iniciarAlComenzar;

    private int pasoActual;
    private bool activo;

    public static TutorialManager tutorialActivo;
    public GameObject objetoAlTerminar;

    private void OnEnable()
    {
        tutorialActivo = this;
    }

    private void OnDisable()
    {
        if (tutorialActivo == this)
            tutorialActivo = null;
    }

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

        TutorialStep paso = pasos[pasoActual];

        if (paso.tipo == StepType.Mensaje)
        {
            if (accion != "continuar")
                return;
        }

        if (paso.tipo == StepType.Accion)
        {
            if (accion != paso.accion)
                return;
        }

        pasoActual++;

        if (pasoActual >= pasos.Count)
            TerminarSecuencia();
        else
            MostrarPaso();
    }

    void TerminarSecuencia()
    {
        activo = false;
        panelTutorial.SetActive(false);

        if (objetoAlTerminar != null)
            objetoAlTerminar.SetActive(false);

        if (MissionManager.Instance != null)
            MissionManager.Instance.SiguientePaso();
    }

    public bool TutorialTerminado()
    {
        return !activo && pasoActual >= pasos.Count;
    }

    public bool EsperandoContinuar()
    {
        if (!activo) return false;

        return pasos[pasoActual].tipo == StepType.Mensaje;
    }
}