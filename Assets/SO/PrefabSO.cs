using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spawner", menuName = "ScriptableObjects/Spawner")]
public class PrefabSO : ScriptableObject
{
    public GameObject[] powerUps;
    public int spawnThreshold = 50;

    public void SpawnPowerUps(Vector3 spawnPosition)
    {
        int randomChance = Random.Range(0, 100);
        if (randomChance > spawnThreshold)
        {
            int randomIndex = Random.Range(0, powerUps.Length);
            Instantiate(powerUps[randomIndex], spawnPosition, Quaternion.identity);
        }

    }
}
