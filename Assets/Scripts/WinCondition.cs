using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCondition : MonoBehaviour
{
    float _timer;
    [SerializeField] private float _possibleWinTime = 10f;
    [SerializeField] private GameObject[] _spawners;
    [SerializeField] private bool _hasBoss = false;

    public bool _canSpawnBoss = false;


    private void Update()
    {
        if(EndGameManager.instance._gameOver == true)
        {
            return;
        }

        _timer += Time.deltaTime;
        if (_timer >= _possibleWinTime)
        {
            if (_hasBoss == false)
            {
                EndGameManager.instance.StartResolveSequence();
            }
            else
            {
                _canSpawnBoss = true;
            }

            for (int i = 0; i < _spawners.Length; i++)
            {
                _spawners[i].SetActive(false);
            }

            gameObject.SetActive(false);
              // win or lose screen
                // GameManager
        }
    }
}
