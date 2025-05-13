using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sawblade : Obstacle
{
    public float speed = 1.0f;
    public float distance = 1.0f;
    private Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
         startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f, speed * Time.deltaTime, 0f);
    }
}
