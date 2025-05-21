using UnityEngine;

public class Deathpit : Obstacle
{
	// protected virtual void OnTriggerEnter(Collider other)
	// {
	//     if (other.CompareTag("Player"))
	//     {
	//         PlayerColision player = other.GetComponent<PlayerColision>();
	//         if (player != null)
	//         {
	//             player.Die();
	//         }
	//     }
	// }

	protected override void OnTriggerStay(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			PlayerColision playerCollision = other.GetComponent<PlayerColision>();
			if (playerCollision != null)
			{
				playerCollision.Die(true);
			}
		}
	}

}
