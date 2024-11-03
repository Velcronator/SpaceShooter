using System.Collections;
using UnityEngine;

public class BossEnter : BossBaseState
{
    private Vector2 _enterPoint;
    [SerializeField] private float _speed = 5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start(); // Call the base class's Start() method
        _enterPoint = _mainCamera.ViewportToWorldPoint(new Vector2(0.5f, 0.7f));
    }

    public override void RunState()
    {
        StartCoroutine(RunEnterState());
    }

    public override void StopState()
    {
        base.StopState();
    }

    IEnumerator RunEnterState()
    {
        while (Vector2.Distance(transform.position, _enterPoint) > 0.01f)
        {
            transform.position = Vector2.MoveTowards(transform.position, _enterPoint, _speed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        _bossController.ChangeState(BossState.fire);
    }
}
