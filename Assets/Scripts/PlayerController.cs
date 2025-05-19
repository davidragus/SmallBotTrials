using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
	public float grappleRangeDefault = 15f;
	private float grappleRange = 15f;
	public bool isGrappling;
	public bool isHooked;
	public float armsOffsetDistance = 0.3f;
	public float playerOffsetDistance = 1f;
	public Vector3 currentHitPoint;

	[SerializeField] private GameObject grappleIndicator;
	private bool isHoldingGrapple = false;
	public bool isDead = false;

	// 5 More range Grapple
	[Header("Grappling powerUp parameters")] 
	public float maxGrappleRange = 20f;
	private float numberOfGrapples = 0f;
	public float numberOfGrapplesDefault = 5f;


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
			if (numberOfGrapples > 0)
			{
				numberOfGrapples--;
			}
			else
			{
				grappleRange = grappleRangeDefault;
			}
			ShootArms();
		}

		if (Input.GetKey(KeyCode.Mouse0) && isHooked)
		{
			isHoldingGrapple = true;
		}

		if (Input.GetKeyUp(KeyCode.Mouse0) && (isHooked || isGrappling))
		{
			isHoldingGrapple = false;
			StartCoroutine(ReturnArms());
		}
	}

	private void FixedUpdate()
	{
		if (isHooked && isHoldingGrapple)
		{
			Vector3 direction = (currentHitPoint - rb.position).normalized;

			rb.AddForce(direction * 100f, ForceMode.Acceleration);

			float maxSpeed = 20f;
			if (rb.velocity.magnitude > maxSpeed)
			{
				rb.velocity = rb.velocity.normalized * maxSpeed;
			}

			float distance = Vector3.Distance(rb.position, currentHitPoint);
			if (distance < 1.5f)
			{
				rb.velocity = Vector3.zero;
				rb.useGravity = false;
			}

			if (rb.velocity.y < 0f)
			{
				rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
			}
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
			// StartCoroutine(MovePlayer(currentHitPoint + playerOffset));
		}
		else
		{
			currentHitPoint = armRaycast.position + armRaycast.up * grappleRange;
			StartCoroutine(MoveArms(currentHitPoint, true));
		}
	}

	private IEnumerator MoveArms(Vector3 end, bool missed = false)
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

		if (missed)
		{
			StartCoroutine(ReturnArms());
			yield return null;
		}
		else
		{
			isHooked = true;
			yield return null;
		}
	}

	// private IEnumerator MovePlayer(Vector3 end)
	// {
	// 	yield return new WaitForSeconds(0.2f);

	// 	// rb.useGravity = false;
	// 	// rb.velocity = Vector3.zero;

	// 	// float duration = 0.2f;
	// 	// float elapsed = 0f;
	// 	// Vector3 originalPos = rb.position;

	// 	// while (elapsed < duration)
	// 	// {
	// 	// 	elapsed += Time.deltaTime;
	// 	// 	float t = elapsed / duration;
	// 	// 	Vector3 newPos = Vector3.Lerp(originalPos, end, t);
	// 	// 	rb.MovePosition(newPos);
	// 	// 	yield return null;
	// 	// }

	// 	// rb.MovePosition(end);

	// 	Vector3 impulseDirection = (end - rb.position).normalized;
	// 	rb.velocity = Vector3.zero;
	// 	rb.AddForce(impulseDirection * 1000f, ForceMode.Impulse);
	// }


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

	public void ActivateGrapple()
	{
		grappleRange = maxGrappleRange;
		numberOfGrapples = numberOfGrapplesDefault;
	}
}
