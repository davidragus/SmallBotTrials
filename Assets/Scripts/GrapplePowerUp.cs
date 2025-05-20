using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplePowerUp : PowerUp
{
    protected override void OnTriggerEnter(Collider other)
{
    if (other.CompareTag("Player"))
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player == null)
        {
            player = other.GetComponentInParent<PlayerController>();
        }

        if (player != null)
        {
            player.ActivateGrapple();
            base.OnTriggerEnter(other);
        }
        else
        {
            Debug.LogWarning("El objeto con tag 'Player' no tiene PlayerController.");
        }
    }
}

}
