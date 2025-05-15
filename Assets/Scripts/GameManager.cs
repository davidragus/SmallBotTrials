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
        // UpdateDeathCounterUI();
    }

    // private void UpdateDeathCounterUI()
    // {
    //     if (deathCountText == null)
    //     {
    //         GameObject deathCounterObject = GameObject.Find("DeathCounter");
    //         if (deathCounterObject != null)
    //         {
    //             deathCountText = deathCounterObject.GetComponent<TextMeshProUGUI>();
    //         }
    //     }

    //     if (deathCountText != null)
    //     {
    //         deathCountText.text = deathCount.ToString();
    //     }
    // }

    public int GetDeathCounter()
    {
        return deathCount;
    }

    // Start is called before the first frame update
    // void Start()
    // {
    //     UpdateDeathCounterUI();
    // }

    // Update is called once per frame
    void Update()
    {
        
    }
}