using UnityEngine;
using TMPro;

public class MissionManager : MonoBehaviour
{
    public static MissionManager Instance;

    public TMP_Text textoMision;

    [Header("UI Misión secundaria")]
    public GameObject panelMisionSecundaria;
    public TMP_Text textoMisionSecundaria;

    public string nombreMision = "LLega a la piedra de fenix";
    public string[] pasos;

    [Header("Misión secundaria")]
    public string nombreMisionSecundaria = "Misión secundaria";
    public string[] pasosSecundaria;

    private int pasoActual = 0;
    private int pasoSecundarioActual = 0;

    private bool misionSecundariaActiva;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        MostrarPaso();
        if (panelMisionSecundaria != null)
            panelMisionSecundaria.SetActive(false);
    }

    void MostrarPaso()
    {
        textoMision.text = nombreMision + "\n" + pasos[pasoActual];
    }
    void MostrarPasoSecundario()
    {
        textoMisionSecundaria.text =
            nombreMisionSecundaria + "\n" + pasosSecundaria[pasoSecundarioActual];
    }

    public void SiguientePaso()
    {
        pasoActual++;

        if (pasoActual >= pasos.Length)
        {
            textoMision.text = nombreMision + "\nMisión completada";
            return;
        }

        MostrarPaso();
    }
    public void ActivarMisionSecundaria()
    {
        if (misionSecundariaActiva)
            return;

        misionSecundariaActiva = true;

        if (panelMisionSecundaria != null)
            panelMisionSecundaria.SetActive(true);

        MostrarPasoSecundario();
    }

    public void SiguientePasoSecundario()
    {
        if (!misionSecundariaActiva)
            return;

        pasoSecundarioActual++;

        if (pasoSecundarioActual >= pasosSecundaria.Length)
        {
            textoMisionSecundaria.text =
                nombreMisionSecundaria + "\nMisión completada";
            return;
        }

        MostrarPasoSecundario();
    }

}