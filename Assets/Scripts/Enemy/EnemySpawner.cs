using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float _leftLimit = 0.15f;
    [SerializeField] private float _rightLimit = 0.85f;

    [SerializeField] private GameObject[] _enemy;
    [SerializeField] private float _spawntime;

    [Header("Boss")]
    [SerializeField] private GameObject _bossPrefab;
    [SerializeField] private WinCondition _winCondition;

    private float timer = 0f;
    private int i;
    private float yPos;


    private float _maxLeft;
    private float _maxRight;

    private Camera _mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        _mainCamera = Camera.main;
        StartCoroutine(SetBoundries());
    }

    // Update is called once per frame
    void Update()
    {
        EnemySpawn();
    }

    private void EnemySpawn()
    {
        timer += Time.deltaTime;
        if (timer >= _spawntime)
        {
            timer = 0f;
            i = Random.Range(0, _enemy.Length);
            yPos = _mainCamera.ViewportToWorldPoint(new Vector2(0, 1)).y;
            Instantiate(_enemy[i], new Vector3(Random.Range(_maxLeft, _maxRight), yPos, 0), Quaternion.identity);
        }
    }

    private IEnumerator SetBoundries()
    {
        yield return new WaitForSeconds(0.4f);
        _maxLeft = _mainCamera.ViewportToWorldPoint(new Vector2(_leftLimit, 0)).x;
        _maxRight = _mainCamera.ViewportToWorldPoint(new Vector2(_rightLimit, 0)).x;
    }

    private void OnDisable()
    {
        if(_winCondition._canSpawnBoss == false)
        {
            return;
        }

        if(_bossPrefab != null)
        {
            Vector2 spawnPosition = _mainCamera.ViewportToWorldPoint(new Vector2(0.5f, 1.2f));
            Instantiate(_bossPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
