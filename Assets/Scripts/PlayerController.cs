using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerController : MonoBehaviour
{
	private Rigidbody rb;

	[Header("References")]

	[SerializeField] private Transform armsParent;
	[SerializeField] private Transform leftOrigin;
	[SerializeField] private Transform rightOrigin;
	// [SerializeField] private ParticleSystem leftParticles;
	// [SerializeField] private ParticleSystem rightParticles;
	[SerializeField] private Transform leftArm;
	[SerializeField] private Transform rightArm;
	[SerializeField] private Transform armRaycast;
	public LayerMask grappleLayer;


	[Header("Grappling parameters")]
	public float grappleRange = 15f;
	public bool isGrappling;
	public bool isHooked;
	public float armsOffsetDistance = 0.3f;
	public float playerOffsetDistance = 1f;
	public Vector3 currentHitPoint;

	[SerializeField] private GameObject grappleIndicator;


	void Start()
	{
		rb = GetComponent<Rigidbody>();
	}

	void Update()
	{
		Debug.DrawRay(armRaycast.position, armRaycast.up * grappleRange, Color.red);
		RaycastHit hit;
		if (Physics.Raycast(armRaycast.position, armRaycast.up, out hit, grappleRange, grappleLayer))
		{
			grappleIndicator.SetActive(true);
			grappleIndicator.transform.position = hit.point;
		}
		else
		{
			grappleIndicator.SetActive(false);
		}

		if (Input.GetKeyDown(KeyCode.Mouse0) && !isGrappling && !isHooked)
		{
			ShootArms();
		}
		else if (Input.GetKeyDown(KeyCode.Mouse0) && isHooked)
		{
			StartCoroutine(ReturnArms());
		}
	}

	private void ShootArms()
	{
		isGrappling = true;
		leftArm.SetParent(null, true);
		rightArm.SetParent(null, true);

		RaycastHit hit;
		if (Physics.Raycast(armRaycast.position, armRaycast.up, out hit, grappleRange, grappleLayer))
		{
			currentHitPoint = hit.point;
			Vector3 armsOffset = hit.normal * armsOffsetDistance;
			Vector3 playerOffset = hit.normal * playerOffsetDistance;

			StartCoroutine(MoveArms(currentHitPoint + armsOffset));
			isHooked = true;
			StartCoroutine(MovePlayer(currentHitPoint + playerOffset));
		}
		else
		{
			currentHitPoint = armRaycast.position + armRaycast.up * grappleRange;
			StartCoroutine(MoveArms(currentHitPoint, true));
		}
	}

	private IEnumerator MoveArms(Vector3 end, bool returnAfter = false)
	{
		float duration = 0.2f;
		float elapsed = 0f;

		while (elapsed < duration)
		{
			elapsed += Time.deltaTime;
			float t = elapsed / duration;
			leftArm.position = Vector3.Lerp(leftOrigin.position, end, t);
			rightArm.position = Vector3.Lerp(rightOrigin.position, end, t);
			yield return null;
		}

		leftArm.position = end;
		rightArm.position = end;

		if (returnAfter)
		{
			StartCoroutine(ReturnArms());
			yield return null;
		}
	}

	private IEnumerator MovePlayer(Vector3 end)
	{
		yield return new WaitForSeconds(0.2f);
		rb.useGravity = false;
		rb.velocity = Vector3.zero;

		float duration = 0.2f;
		float elapsed = 0f;
		Vector3 originalPos = rb.position;

		while (elapsed < duration)
		{
			elapsed += Time.deltaTime;
			float t = elapsed / duration;
			Vector3 newPos = Vector3.Lerp(originalPos, end, t);
			rb.MovePosition(newPos);
			yield return null;
		}

		rb.MovePosition(end);
	}


	private IEnumerator ReturnArms()
	{
		rb.useGravity = true;
		leftArm.SetParent(armsParent);
		rightArm.SetParent(armsParent);

		float duration = 0.2f;
		float elapsed = 0f;

		while (elapsed < duration)
		{
			elapsed += Time.deltaTime;
			float t = elapsed / duration;
			leftArm.position = Vector3.Lerp(leftArm.position, leftOrigin.position, t);
			rightArm.position = Vector3.Lerp(rightArm.position, rightOrigin.position, t);
			yield return null;
		}

		isGrappling = false;
		isHooked = false;
	}
}
