using System.Collections;
using System.Collections.Generic;
// using System.Numerics;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UIElements;

public class ArmsTracking : MonoBehaviour
{

	[Header("References")]
	[SerializeField] private Transform leftArm;
	[SerializeField] private Transform rightArm;
	[SerializeField] private Transform armRaycast;

	[Header("Parameters")]
	[SerializeField] private LayerMask armsTrackingLayer;
	[SerializeField] private float maxArmTrackingRadius = 150f;


	private Camera mainCamera;
	private PlayerController playerController;



	void Start()
	{
		mainCamera = Camera.main;
		playerController = GetComponent<PlayerController>();
	}

	void Update()
	{
		if (!playerController.isGrappling)
		{
			AimArms();
		}
	}

	private (bool success, Vector3 position) GetCursorPosition()
	{
		Vector2 characterScreenPos = mainCamera.WorldToScreenPoint(transform.position);
		Vector2 mousePos = Input.mousePosition;

		Vector2 offset = mousePos - characterScreenPos;
		if (offset.magnitude < maxArmTrackingRadius)
		{
			offset = offset.normalized * maxArmTrackingRadius;
			mousePos = characterScreenPos + offset;
		}

		var raycast = mainCamera.ScreenPointToRay(mousePos);
		// Debug.DrawRay(raycast.origin, raycast.direction * 100f, Color.red);
		if (Physics.Raycast(raycast, out var hitInfo, Mathf.Infinity, armsTrackingLayer))
		{
			return (success: true, position: hitInfo.point);
		}
		else
		{
			return (success: false, position: Vector3.zero);
		}
	}

	private void AimArms()
	{
		var (success, position) = GetCursorPosition();

		if (success)
		{
			var leftArmDirection = position - leftArm.position;
			var rightArmDirection = position - rightArm.position;
			var armRaycastDirection = position - armRaycast.position;

			leftArm.up = leftArmDirection;
			rightArm.up = rightArmDirection;
			armRaycast.up = armRaycastDirection;
		}
	}
}
