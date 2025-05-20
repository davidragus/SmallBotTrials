using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuPanel;
    public GameObject controlsPanel;

    public Button resumeButton;
    public Button controlsButton;
    public Button quitButton;
    public Button backButton;

    private bool isPaused = false;

    void Start()
    {
        pauseMenuPanel.SetActive(false);
        controlsPanel.SetActive(false);
        resumeButton.onClick.AddListener(ResumeGame);
        controlsButton.onClick.AddListener(ShowControls);
        quitButton.onClick.AddListener(QuitGame);
        backButton.onClick.AddListener(BackToPauseMenu);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    void PauseGame()
    {
        Time.timeScale = 0f;
        GameObject player = GameObject.FindGameObjectsWithTag("Player")[0];
        player.GetComponent<PlayerController>().enabled = false;
        player.GetComponent<EyesTracking>().enabled = false;
        player.GetComponent<ArmsTracking>().enabled = false;
        player.GetComponent<CharacterRotation>().enabled = false;
        pauseMenuPanel.SetActive(true);
        isPaused = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        GameObject player = GameObject.FindGameObjectsWithTag("Player")[0];
        player.GetComponent<PlayerController>().enabled = true;
        player.GetComponent<EyesTracking>().enabled = true;
        player.GetComponent<ArmsTracking>().enabled = true;
        player.GetComponent<CharacterRotation>().enabled = true;
        pauseMenuPanel.SetActive(false);
        controlsPanel.SetActive(false);
        isPaused = false;
    }

    void ShowControls()
    {
        pauseMenuPanel.SetActive(false);
        controlsPanel.SetActive(true);
    }

    void BackToPauseMenu()
    {
        controlsPanel.SetActive(false);
        pauseMenuPanel.SetActive(true);
    }

    void QuitGame()
    {
        // Si estás en el editor
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
