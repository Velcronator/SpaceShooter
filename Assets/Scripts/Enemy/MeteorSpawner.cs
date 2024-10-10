using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{
    [SerializeField] private float _leftLimit = 0.15f;
    [SerializeField] private float _rightLimit = 0.85f;

    [SerializeField] private GameObject[] _meteors;
    [SerializeField] private float _spawntime;

    private float timer= 0f;
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
        timer += Time.deltaTime;
        if (timer >= _spawntime)
        {
            timer = 0f;
            i = Random.Range(0, _meteors.Length);
            yPos = _mainCamera.ViewportToWorldPoint(new Vector2(0, 1)).y;
            Instantiate(_meteors[i], new Vector3(Random.Range(_maxLeft, _maxRight), yPos, 0), Quaternion.Euler(0,0,Random.Range(0,360)));

            // randomize the meteor size
            float scale = Random.Range(0.5f, 1.1f);
            _meteors[i].transform.localScale = new Vector3(scale, scale, 1);

        }
    }

    private IEnumerator SetBoundries()
    {
        yield return new WaitForSeconds(0.4f);
        _maxLeft = _mainCamera.ViewportToWorldPoint(new Vector2(_leftLimit, 0)).x;
        _maxRight = _mainCamera.ViewportToWorldPoint(new Vector2(_rightLimit, 0)).x;
    }
}
