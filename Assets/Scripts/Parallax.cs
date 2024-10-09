using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private float _parallaxSpeed;

    private float _spriteHeight;
    private Vector3 _startPos;

    void Start()
    {
        _startPos = transform.position;
        _spriteHeight = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _parallaxSpeed * Time.deltaTime);
        if(transform.position.y < _startPos.y - _spriteHeight)
        {
            transform.position = _startPos;
        }
    }
}
