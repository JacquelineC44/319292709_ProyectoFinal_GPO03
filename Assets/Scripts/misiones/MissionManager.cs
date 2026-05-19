using UnityEngine;
using TMPro;

public class MissionManager : MonoBehaviour
{
    public static MissionManager Instance;

    public TMP_Text textoMision;

    public string nombreMision = "LLega a la piedra de cenizas";
    public string[] pasos;

    private int pasoActual = 0;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        MostrarPaso();
    }

    void MostrarPaso()
    {
        textoMision.text = nombreMision + "\n" + pasos[pasoActual];
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
}