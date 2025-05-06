using System.Collections;
using System.Collections.Generic;
// using System.Numerics;
using UnityEngine;
using UnityEngine.UIElements;

public class EyesTracking : MonoBehaviour
{

	[SerializeField] private LayerMask eyesTrackingLayer;
	[SerializeField] private Transform leftEye;
	[SerializeField] private Transform rightEye;
	[SerializeField] private float maxEyeTrackingRadius = 100f;

	private Camera mainCamera;

	void Start()
	{
		mainCamera = Camera.main;
	}

	void Update()
	{
		AimEyes();
	}

	private (bool success, Vector3 position) GetCursorPosition()
	{
		Vector2 characterScreenPos = mainCamera.WorldToScreenPoint(transform.position);
		Vector2 mousePos = Input.mousePosition;

		Vector2 offset = mousePos - characterScreenPos;
		if (offset.magnitude > maxEyeTrackingRadius)
		{
			offset = offset.normalized * maxEyeTrackingRadius;
			mousePos = characterScreenPos + offset;
		}

		Ray raycast = mainCamera.ScreenPointToRay(mousePos);
		// Debug.DrawRay(raycast.origin, raycast.direction * 100f, Color.red);
		if (Physics.Raycast(raycast, out var hitInfo, Mathf.Infinity, eyesTrackingLayer))
		{
			return (true, hitInfo.point);
		}
		else
		{
			return (false, Vector3.zero);
		}
	}

	private void AimEyes()
	{
		var (success, position) = GetCursorPosition();

		if (success)
		{
			var leftEyeDirection = position - leftEye.position;
			var rightEyeDirection = position - rightEye.position;

			leftEye.up = leftEyeDirection;
			rightEye.up = rightEyeDirection;
		}
	}
}
