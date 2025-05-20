using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CharacterRotation : MonoBehaviour
{

	[Header("Parameters")]
	public float maxYAngle = 30f;

	Rigidbody rb;
	float targetYaw;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
	}

	void Update()
	{
		GetPlayerRotation();
	}

	void FixedUpdate()
	{
		RotatePlayer();
	}

	private void GetPlayerRotation()
	{
		float mx = Input.mousePosition.x;
		float half = Screen.width * 0.5f;
		float normX = (mx - half) / half;
		targetYaw = Mathf.Clamp(normX, -1f, 1f) * -maxYAngle;
	}

	private void RotatePlayer()
	{
		Quaternion desired = Quaternion.Euler(-90f, targetYaw, -90f);
		rb.MoveRotation(desired);
	}
}
