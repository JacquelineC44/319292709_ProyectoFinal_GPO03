using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenuManager : MonoBehaviour
{
    public GameObject botonContinuar;
    public GameObject panelCargando;

    void Start()
    {
        botonContinuar.SetActive(PlayerPrefs.HasKey("PartidaGuardada"));
        panelCargando.SetActive(false);
    }

    public void NuevaPartida()
    {
        PlayerPrefs.SetInt("PartidaGuardada", 1);
        PlayerPrefs.Save();

        StartCoroutine(CargarEscena("319292709_ProyectoFinal_GP03ver5"));
    }

    public void ContinuarPartida()
    {
        StartCoroutine(CargarEscena("319292709_ProyectoFinal_GP03ver5"));
    }

    IEnumerator CargarEscena(string nombreEscena)
    {
        panelCargando.SetActive(true);

        AsyncOperation carga = SceneManager.LoadSceneAsync(nombreEscena);

        while (!carga.isDone)
        {
            yield return null;
        }
    }

    public void Salir()
    {
        Application.Quit();
    }
}