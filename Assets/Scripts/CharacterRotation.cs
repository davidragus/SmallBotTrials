using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CharacterRotation : MonoBehaviour
{
	public float maxYAngle = 30f;

	Rigidbody rb;
	float targetYaw;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
	}

	void Update()
	{
		float mx = Input.mousePosition.x;
		float half = Screen.width * 0.5f;
		float normX = (mx - half) / half;
		targetYaw = Mathf.Clamp(normX, -1f, 1f) * -maxYAngle;
	}

	void FixedUpdate()
	{
		Quaternion desired = Quaternion.Euler(0f, targetYaw, 0f);
		rb.MoveRotation(desired);
	}
}
