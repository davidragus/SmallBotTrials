using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrownPowerUp : PowerUp
{
    [SerializeField] private bool isWin = false;
    [Header("Scene")]
    [SerializeField] private int SceneNumber = 0;

    protected override void OnTriggerEnter(Collider other)
    {
        if (isWin)
        {
            GameManager.Instance.WinGame();
        }
        else
        {
            GameManager.Instance.ChangeSceneLoad(SceneNumber);
            GameManager.Instance.ChangeScene();
        }
        
    }
    protected override void Movement()
    {
        // Rotate the crown around its Y-axis
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);

        // Make the crown float up and down
        float yOffset = Mathf.Sin(Time.time * floatFrequency) * floatAmplitude;
        transform.position = new Vector3(startPos.x, startPos.y + yOffset, startPos.z);
    }
}
