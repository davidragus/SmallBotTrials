using UnityEngine;

public class Obstacle : MonoBehaviour
{
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerColision player = other.GetComponent<PlayerColision>();
            if (player != null)
            {
                player.Die();
            }
        }
    }

    protected virtual void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerColision player = other.GetComponent<PlayerColision>();
            if (player != null)
            {
                player.Die();
            }
        }
    }
    
}
