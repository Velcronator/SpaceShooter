using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpShield : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D otherColl)
    {
        if (otherColl.CompareTag("Player"))
        {
            PlayerShieldActivator playerShieldActivator = otherColl.GetComponent<PlayerShieldActivator>();
            playerShieldActivator.ActivateShield();
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
