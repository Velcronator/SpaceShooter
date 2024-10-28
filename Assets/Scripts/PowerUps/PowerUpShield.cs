using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpShield : MonoBehaviour
{
    [SerializeField] private AudioClip _shieldPickupSound;

    private void OnTriggerEnter2D(Collider2D otherColl)
    {
        if (otherColl.CompareTag("Player"))
        {
            PlayerShieldActivator playerShieldActivator = otherColl.GetComponent<PlayerShieldActivator>();
            playerShieldActivator.ActivateShield();

            // Play the pickup sound
            AudioSource.PlayClipAtPoint(_shieldPickupSound, transform.position, 1f);

            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
