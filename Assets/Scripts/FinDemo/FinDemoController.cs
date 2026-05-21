using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class FinDemoController : MonoBehaviour
{
    [Header("Panel final")]
    [SerializeField] private GameObject panelFinDemo;
    [SerializeField] private GameObject primerBotonSeleccionado;

    [Header("Interfaces que se deben ocultar")]
    [SerializeField] private GameObject[] interfacesParaOcultar;

    [Header("Input")]
    [SerializeField] private UnityEngine.InputSystem.PlayerInput playerInput;

    private bool activado = false;

    public void MostrarFinDemo()
    {
        if (activado) return;
        activado = true;

        foreach (GameObject interfaz in interfacesParaOcultar)
        {
            if (interfaz != null)
                interfaz.SetActive(false);
        }

        if (panelFinDemo != null)
            panelFinDemo.SetActive(true);

        if (EventSystem.current != null && primerBotonSeleccionado != null)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(primerBotonSeleccionado);
        }

        if (playerInput != null)
            playerInput.SwitchCurrentActionMap("UIPause");

        Time.timeScale = 0f;
    }
}