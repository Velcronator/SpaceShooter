using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpShooting : MonoBehaviour
{
    [SerializeField] private AudioClip _shootingPickupSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerShooting playerShooting = collision.GetComponent<PlayerShooting>();
            if (playerShooting != null)
            {
                AudioSource.PlayClipAtPoint(_shootingPickupSound, transform.position, 1f);
                playerShooting.ChangeWeaponUpgradeLevel(1);
            }
            Destroy(gameObject);
        }
    }
}
