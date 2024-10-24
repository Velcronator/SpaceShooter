using UnityEngine;

public class BossBaseState : MonoBehaviour
{
    protected Camera _mainCamera;

    protected float _maxLeft;
    protected float _maxRight;
    protected float _maxDown;
    protected float _maxUp;

    [Header("Boundries")]
    [SerializeField] private float _leftLimit = 0.3f;
    [SerializeField] private float _rightLimit = 0.7f;
    [SerializeField] private float _upLimit = 0.9f;
    [SerializeField] private float _downLimit = 0.6f;

    protected BossController _bossController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _bossController = GetComponent<BossController>();
        _mainCamera = Camera.main;
    }

    protected virtual void Start()
    {
        _maxLeft = _mainCamera.ViewportToWorldPoint(new Vector2(_leftLimit, 0)).x;
        _maxRight = _mainCamera.ViewportToWorldPoint(new Vector2(_rightLimit, 0)).x;
        _maxDown = _mainCamera.ViewportToWorldPoint(new Vector2(0, _downLimit)).y;
        _maxUp = _mainCamera.ViewportToWorldPoint(new Vector2(0, _upLimit)).y;
    }

    public virtual void RunState()
    {

    }

    public virtual void StopState()
    {
        StopAllCoroutines();
    }
}