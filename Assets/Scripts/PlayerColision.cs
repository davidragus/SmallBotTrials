using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColision : MonoBehaviour
{
	[SerializeField] private GameObject shield;
	[SerializeField] private bool invincible = false;
	[SerializeField] private float invincibleTime = 3f;

	[Header("Death Animation")]
	[SerializeField] private GameObject player;
	[SerializeField] private Material deathMaterial;
	[SerializeField] private GameObject leftEye;
	[SerializeField] private GameObject rightEye;
	[SerializeField] private GameObject leftArm;
	[SerializeField] private GameObject rightArm;
	[SerializeField] private ParticleSystem explosionFlash;
	[SerializeField] private ParticleSystem explosionSmoke;
	private bool recentlyHit = false;
	private Rigidbody rb;

	public void Start()
	{
		rb = GetComponentInParent<Rigidbody>();
	}

	public void Die()
	{
		if (recentlyHit)
		{
			return;
		}

		if (invincible)
		{
			shield.SetActive(true);
			invincible = false;
			StartCoroutine(ShieldCooldown());
			return;
		}
		StartCoroutine(PlayerDeath());
	}

	public void ActivateShield()
	{
		invincible = true;
	}

	private IEnumerator ShieldCooldown()
	{
		recentlyHit = true;
		yield return new WaitForSeconds(invincibleTime);
		shield.SetActive(false);
		recentlyHit = false;
	}

	private IEnumerator PlayerDeath()
	{
		rb.isKinematic = true;
		leftArm.GetComponent<SkinnedMeshRenderer>().sharedMaterial = deathMaterial;
		rightArm.GetComponent<SkinnedMeshRenderer>().sharedMaterial = deathMaterial;
		GetComponent<MeshRenderer>().sharedMaterial = deathMaterial;
		player.GetComponent<ArmsTracking>().enabled = false;
		player.GetComponent<EyesTracking>().enabled = false;
		player.GetComponent<CharacterRotation>().enabled = false;
		yield return new WaitForSeconds(0.5f);
		GetComponent<MeshRenderer>().enabled = false;
		explosionFlash.Emit(1);
		explosionSmoke.Emit(1);
		leftArm.GetComponent<SkinnedMeshRenderer>().enabled = false;
		rightArm.GetComponent<SkinnedMeshRenderer>().enabled = false;
		leftEye.GetComponent<SkinnedMeshRenderer>().enabled = false;
		rightEye.GetComponent<SkinnedMeshRenderer>().enabled = false;
		yield return new WaitForSeconds(5f);
		GameManager.Instance.AddDeathCount();
		UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
	}

}
