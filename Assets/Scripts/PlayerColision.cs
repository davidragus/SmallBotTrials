using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColision : MonoBehaviour
{
    [SerializeField] private bool invincible = false;
    [SerializeField] private float invincibleTime = 1f;
    private bool recentlyHit = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Die()
    {
        if (recentlyHit)
        {
            return;
        }

        if (invincible)
        {
            invincible = false;
            StartCoroutine(ShieldCooldown());
            return;
        }

        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    public void ActivateShield()
    {
        invincible = true;
    }

    private IEnumerator ShieldCooldown()
    {
        recentlyHit = true;
        yield return new WaitForSeconds(invincibleTime);
        recentlyHit = false;
    }

}
