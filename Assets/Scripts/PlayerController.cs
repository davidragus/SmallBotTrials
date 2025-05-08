using System.Collections;
using System.Collections.Generic;
// using System.Numerics;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerController : MonoBehaviour
{

	[Header("References")]

	[SerializeField] private Transform armsParent;
	[SerializeField] private Transform leftArm;
	[SerializeField] private Transform rightArm;
	[SerializeField] private Transform armRaycast;
	public LayerMask grappleLayer;


	[Header("Grappling parameters")]
	public float grappleRange;
	public bool isGrappling;
	public bool isHooked;
	public float offsetDistance = 0.3f;
	public Vector3 currentHitPoint;

	void Start()
	{

	}

	void Update()
	{
		Debug.DrawRay(armRaycast.position, armRaycast.up * grappleRange, Color.red);
		if (Input.GetKeyDown(KeyCode.Mouse0) && !isGrappling && !isHooked)
		{
			ShootArms();
		}
	}

	private void ShootArms()
	{
		isGrappling = true;
		Vector3 leftArmPos = leftArm.position;
		Vector3 rightArmPos = rightArm.position;
		leftArm.SetParent(null, true);
		rightArm.SetParent(null, true);

		RaycastHit hit;
		if (Physics.Raycast(armRaycast.position, armRaycast.up, out hit, grappleRange))
		{
			currentHitPoint = hit.point;
			Vector3 offset = hit.normal * offsetDistance;

			StartCoroutine(MoveArms(leftArm, rightArm, leftArmPos, rightArmPos, currentHitPoint + offset));
		}
		else
		{
			currentHitPoint = armRaycast.position + armRaycast.up * grappleRange;
			StartCoroutine(MoveArms(leftArm, rightArm, leftArmPos, rightArmPos, currentHitPoint, true));
		}
	}

	private IEnumerator MoveArms(Transform leftArm, Transform rightArm, Vector3 leftArmStart, Vector3 rightArmStart, Vector3 end, bool returnAfter = false)
	{
		float duration = 0.2f;
		float elapsed = 0f;

		while (elapsed < duration)
		{
			elapsed += Time.deltaTime;
			float t = elapsed / duration;
			leftArm.position = Vector3.Lerp(leftArmStart, end, t);
			rightArm.position = Vector3.Lerp(rightArmStart, end, t);
			yield return null;
		}

		leftArm.position = end;
		rightArm.position = end;

		if (returnAfter)
		{
			StartCoroutine(ReturnArms(leftArmStart, rightArmStart));
			yield return null;
		}
	}

	private IEnumerator ReturnArms(Vector3 leftArmOriginalPos, Vector3 rightArmOriginalPos)
	{
		leftArm.SetParent(armsParent);
		rightArm.SetParent(armsParent);

		float duration = 0.2f;
		float elapsed = 0f;

		while (elapsed < duration)
		{
			elapsed += Time.deltaTime;
			float t = elapsed / duration;
			leftArm.position = Vector3.Lerp(leftArm.position, leftArmOriginalPos, t);
			rightArm.position = Vector3.Lerp(rightArm.position, rightArmOriginalPos, t);
			yield return null;
		}

		isGrappling = false;
	}
}
