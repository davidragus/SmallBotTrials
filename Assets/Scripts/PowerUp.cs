using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public float rotationSpeed = 50f;
    public float floatAmplitude = 0.25f;
    public float floatFrequency = 1f;

    protected Vector3 startPos;

    protected void Start()
    {
        startPos = transform.position;
    }

    protected void Update()
    {
        Movement();
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }

    protected virtual void Movement()
    {
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);

        float yOffset = Mathf.Sin(Time.time * floatFrequency) * floatAmplitude;
        transform.position = new Vector3(startPos.x, startPos.y + yOffset, startPos.z);
    }

}
