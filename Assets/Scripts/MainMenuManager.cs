using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
	public GameObject mainMenu;
	public GameObject howToPlayMenu;
	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	public void OpenHowToPlay()
	{
		mainMenu.SetActive(false);
		howToPlayMenu.SetActive(true);
	}
	public void CloseHowToPlay()
	{
		mainMenu.SetActive(true);
		howToPlayMenu.SetActive(false);
	}
}
