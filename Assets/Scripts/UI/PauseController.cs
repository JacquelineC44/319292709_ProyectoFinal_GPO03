//using UnityEngine;
//using UnityEngine.InputSystem;
//using UnityEngine.SceneManagement;

//public class PauseController : MonoBehaviour
//{
//    [SerializeField] private GameObject pausePanel;
//    [SerializeField] private UnityEngine.InputSystem.PlayerInput playerInput;

//    private bool isPaused = false;
//    [SerializeField] private GameObject[] interfacesParaOcultar;


//    private void Awake()
//    {
//        playerInput = GetComponent<UnityEngine.InputSystem.PlayerInput>();
//    }

//    private void Start()
//    {
//        pausePanel.SetActive(false);
//        playerInput.SwitchCurrentActionMap("CharacterController");
//    }

//    public void OnPause()
//    {
//        if (isPaused)
//            ResumeGame();
//        else
//            PauseGame();
//    }

//    public void PauseGame()
//    {
//        isPaused = true;
//        pausePanel.SetActive(true);

//        playerInput.SwitchCurrentActionMap("UIPause");

//        Time.timeScale = 0f;
//    }

//    public void ResumeGame()
//    {
//        isPaused = false;
//        pausePanel.SetActive(false);

//        playerInput.SwitchCurrentActionMap("CharacterController");
//        Time.timeScale = 1f;
//    }

//    public void GoToMainMenu()
//    {
//        Time.timeScale = 1f;
//        SceneManager.LoadScene("MenuPrincipal");
//    }
//}
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private UnityEngine.InputSystem.PlayerInput playerInput;

    [Header("Interfaces que se ocultan al pausar")]
    [SerializeField] private GameObject[] interfacesParaOcultar;

    private bool[] estadosPrevios;
    private bool isPaused = false;

    [SerializeField] private GameObject primerBotonPausa;


    private void Start()
    {
        pausePanel.SetActive(false);
        playerInput.SwitchCurrentActionMap("CharacterController");
    }

    public void OnPause()
    {
        if (isPaused)
            ResumeGame();
        else
            PauseGame();
    }

    public void PauseGame()
    {
        isPaused = true;

        estadosPrevios = new bool[interfacesParaOcultar.Length];

        for (int i = 0; i < interfacesParaOcultar.Length; i++)
        {
            if (interfacesParaOcultar[i] != null)
            {
                estadosPrevios[i] = interfacesParaOcultar[i].activeSelf;
                interfacesParaOcultar[i].SetActive(false);
            }
        }

        pausePanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(primerBotonPausa);
        playerInput.SwitchCurrentActionMap("UIPause");

        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        isPaused = false;

        pausePanel.SetActive(false);

        for (int i = 0; i < interfacesParaOcultar.Length; i++)
        {
            if (interfacesParaOcultar[i] != null)
                interfacesParaOcultar[i].SetActive(estadosPrevios[i]);
        }

        playerInput.SwitchCurrentActionMap("CharacterController");
        Time.timeScale = 1f;
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MenuPrincipal");
    }
}