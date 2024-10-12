using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCondition : MonoBehaviour
{
    float _timer;
    [SerializeField] private float _possibleWinTime = 10f;
    [SerializeField] private GameObject[] _spawners;



    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= _possibleWinTime)
        {
            Debug.Log("You win!");
            for (int i = 0; i < _spawners.Length; i++)
            {
                _spawners[i].SetActive(false);
            }
            EndGameManager.instance.StartResolveSequence();
            gameObject.SetActive(false);
              // win or lose screen
                // GameManager
        }
    }
}
