using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{

    [SerializeField] private Transform playerTransform;

    [Header("Configuración de límites")]
    [SerializeField] private float upperMargin = 4f;
    [SerializeField] private float lowerMargin = 3f;

    [Header("Velocidad de seguimiento")]
    [SerializeField] private float followSpeed = 5f;

    
    // Start is called before the first frame update
    void Start()
    {
        if (playerTransform == null)
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (playerTransform != null)
        {
            float cameraY = transform.position.y;
            float playerY = playerTransform.position.y;

            float offsetY = playerY - cameraY;

            if (offsetY > upperMargin)
            {
                cameraY = Mathf.Lerp(cameraY, playerY - upperMargin, Time.deltaTime * followSpeed);
            }
            else if (offsetY < -lowerMargin)
            {
                cameraY = Mathf.Lerp(cameraY, playerY + lowerMargin, Time.deltaTime * followSpeed);
            }

            transform.position = new Vector3(0f, cameraY, -20f);
        }
    }
}
