using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : PowerUp
{
	protected override void OnTriggerEnter(Collider other)
	{
		// Cojemos al player y ejecutamos la funcion de activar el escudo en player.
		if (other.CompareTag("Player"))
		{
			SoundManager.PlaySound(SoundType.GrabPowerUp, 1f, Random.Range(0.9f, 1.1f));
			PlayerColision player = other.GetComponent<PlayerColision>();
			player.ActivateShield();
			base.OnTriggerEnter(other);
		}
	}
}
