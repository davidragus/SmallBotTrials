using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sawblade : Obstacle
{
    public float speed = 200.0f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward, speed * Time.deltaTime);

    }
}
