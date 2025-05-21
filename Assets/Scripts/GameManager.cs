using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance { get; private set; }
	private TextMeshProUGUI deathCountText;
	//[SerializeField] private GameObject shieldUI;
	private int deathCount = 0;
	[SerializeField] private int sceneToLoad = 3;

	// Gameover canvas
	[SerializeField] private GameObject gameOverCanvas;
	public Button quitButton;
    public Button retryButton;

	private void Awake()
	{
		if (GameManager.Instance == null)
		{
			GameManager.Instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}

	public void AddDeathCount()
	{
		deathCount++;
	}

	public int GetDeathCounter()
	{
		return deathCount;
	}

	public void AddShield()
	{
		// if (shieldUI == null)
		// {
		// 	shieldUI = GameObject.Find("ShieldAlpha");
		// }
		// Debug.Log("Add shield");
		// shieldUI.SetActive(true);
	}

	public void RemoveShield()
	{
		// Debug.Log("Remove shield");
		// shieldUI.SetActive(false);
	}

    public void ChangeScene()
    {

        SceneManager.LoadScene(sceneToLoad);
    }

	void Update()
	{

	}

	void Start()
	{
		quitButton.onClick.AddListener(QuitGame);
		retryButton.onClick.AddListener(RetryGame);
	}

	public void GameOver()
	{
		gameOverCanvas.SetActive(true);
	}

	public void RetryGame()
	{
		gameOverCanvas.SetActive(false);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void QuitGame()
	{
	#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
    #else
            Application.Quit();
    #endif
	}

}