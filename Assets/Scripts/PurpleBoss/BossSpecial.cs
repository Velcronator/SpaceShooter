using System.Collections;
using UnityEngine;

public class BossSpecial : BossBaseState
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _waitTime = 30f;
    [SerializeField] private GameObject _specialBullet;

    [Header("Shooting Points")]
    [SerializeField] private Transform [] _shootSpecialPoints;

    Vector2 targetPoint;

    protected override void Start()
    {
        targetPoint = _mainCamera.ViewportToWorldPoint(new Vector2(0.5f, 0.9f));
    }

    public override void RunState()
    {
        StartCoroutine(RunSpecialState());
    }

    public override void StopState()
    {
        base.StopState();
    }

    IEnumerator RunSpecialState()
    {
        while (Vector2.Distance(transform.position, targetPoint) > 0.01f)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPoint, _speed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

        Instantiate(_specialBullet, _shootSpecialPoints[0].position, Quaternion.identity);

        yield return new WaitForSeconds(_waitTime);
        _bossController.ChangeState(BossState.fire);

        //float specialTimer = 0f;

        //while (specialTimer < _waitTime)
        //{
        //    if(Vector2.Distance(transform.position, targetPosition) > 0.01f)
        //    {
        //        transform.position = Vector2.MoveTowards(transform.position, targetPosition, _speed * Time.deltaTime);
        //    }

        //    specialTimer += Time.deltaTime;

        //    yield return new WaitForEndOfFrame();
        //}

        //_bossController.ChangeState(BossState.fire);
    }

}
