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
	private Vector3 velocityBeforeCollision;

	public void Start()
	{
		rb = GetComponentInParent<Rigidbody>();
	}

	private void FixedUpdate()
	{
		velocityBeforeCollision = rb.velocity;
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (velocityBeforeCollision.magnitude > 12f)
		{
			SoundManager.PlaySound(SoundType.HardHit);
		}
		else if (velocityBeforeCollision.magnitude > 6f)
		{
			SoundManager.PlaySound(SoundType.MediumHit);

		}
		else if (velocityBeforeCollision.magnitude > 1f)
		{
			SoundManager.PlaySound(SoundType.SoftHit);
		}
	}

	public void Die(bool ignoreInvincibility = false)
	{
		if (recentlyHit)
		{
			return;
		}

		if (invincible && !ignoreInvincibility)
		{
			shield.SetActive(true);
			GameManager.Instance.RemoveShield();
			invincible = false;
			StartCoroutine(ShieldCooldown());
			return;
		}
		if (!player.GetComponent<PlayerController>().isDead)
		{
			StartCoroutine(PlayerDeath());
		}
	}

	public void ActivateShield()
	{
		GameManager.Instance.AddShield();
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
		player.GetComponent<PlayerController>().isDead = true;
		rb.isKinematic = true;
		leftArm.GetComponent<SkinnedMeshRenderer>().sharedMaterial = deathMaterial;
		rightArm.GetComponent<SkinnedMeshRenderer>().sharedMaterial = deathMaterial;
		GetComponent<MeshRenderer>().sharedMaterial = deathMaterial;
		player.GetComponent<ArmsTracking>().enabled = false;
		player.GetComponent<EyesTracking>().enabled = false;
		player.GetComponent<CharacterRotation>().enabled = false;
		SoundManager.PlaySound(SoundType.BuildUpDeath);
		yield return new WaitForSeconds(0.5f);
		SoundManager.PlaySound(SoundType.Explosion);
		SoundManager.PlaySound(SoundType.Death);
		ExplodePlayer();
		GameManager.Instance.AddDeathCount();
		GameManager.Instance.GameOver();
		
		// UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
	}

	private void ExplodePlayer()
	{
		GetComponent<MeshRenderer>().enabled = false;
		leftArm.GetComponent<SkinnedMeshRenderer>().enabled = false;
		rightArm.GetComponent<SkinnedMeshRenderer>().enabled = false;
		leftEye.GetComponent<SkinnedMeshRenderer>().enabled = false;
		rightEye.GetComponent<SkinnedMeshRenderer>().enabled = false;
		explosionFlash.Play();
		explosionSmoke.Play();
	}

}
