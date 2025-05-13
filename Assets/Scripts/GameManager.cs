using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }
    private TextMeshProUGUI deathCountText;
    private int deathCount = 0;

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

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(deathCount);
        deathCountText = GameObject.Find("DeathCounter").GetComponent<TextMeshProUGUI>();
        deathCountText.text = deathCount.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
