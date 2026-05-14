using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private UnityEngine.InputSystem.PlayerInput playerInput;

    private bool isPaused = false;

    private void Awake()
    {
        playerInput = GetComponent<UnityEngine.InputSystem.PlayerInput>();
    }

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
        pausePanel.SetActive(true);

        playerInput.SwitchCurrentActionMap("UIPause");

        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        isPaused = false;
        pausePanel.SetActive(false);

        playerInput.SwitchCurrentActionMap("CharacterController");
        Time.timeScale = 1f;
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MenuPrincipal");
    }
}