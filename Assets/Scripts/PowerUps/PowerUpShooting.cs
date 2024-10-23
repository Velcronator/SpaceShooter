using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpShooting : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerShooting playerShooting = collision.GetComponent<PlayerShooting>();
            if (playerShooting != null)
            {
                playerShooting.ChangeWeaponUpgradeLevel(1);
            }
            Destroy(gameObject);
        }
    }
}
