using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpHeal : MonoBehaviour
{
    [SerializeField] private AudioClip _healPickupSound;

    [SerializeField] private int _healAmount;

    private void OnTriggerEnter2D(Collider2D otherColl)
    {
        if (otherColl.CompareTag("Player"))
        {
            PlayerStats player = otherColl.GetComponent<PlayerStats>();
            if (player != null)
            {
                AudioSource.PlayClipAtPoint(_healPickupSound, transform.position, 1f);
                player.PlayerHeal(_healAmount);
                Destroy(gameObject);
            }
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
