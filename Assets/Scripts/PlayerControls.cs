using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;


public class PlayerControls : MonoBehaviour
{
    [SerializeField] private float _leftLimit = 0.15f;
    [SerializeField] private float _rightLimit = 0.85f;
    [SerializeField] private float _upLimit = 0.6f;
    [SerializeField] private float _downLimit = 0.05f;

    [SerializeField] private InputActionReference _moveActionToUse;
    
    private Camera _mainCamera;
    private Vector3 _offset;

    private float _maxLeft;
    private float _maxRight;
    private float _maxDown;
    private float _maxUp;

    // Start is called before the first frame update
    void Start()
    {
        _mainCamera = Camera.main;
        StartCoroutine(SetBoundries());
    }

    // Update is called once per frame
    void Update()
    {
        GetActiveTouches();
    }

    private void GetActiveTouches()
    {
        if (Touch.activeTouches.Count > 0)
        {
            if (Touch.activeTouches[0].finger.index == 0)
            {
                Touch myTouch = Touch.activeTouches[0];
                Vector3 touchPos = myTouch.screenPosition;

#if UNITY_EDITOR
                if (touchPos.x == Mathf.Infinity) return;
#endif

                touchPos = _mainCamera.ScreenToWorldPoint(touchPos);
                if (Touch.activeTouches[0].phase == TouchPhase.Began)
                {
                    _offset = touchPos - transform.position;
                }

                if (Touch.activeTouches[0].phase == TouchPhase.Moved)
                {
                    transform.position = new Vector3(touchPos.x, touchPos.y, 0) - _offset;
                }

                if (Touch.activeTouches[0].phase == TouchPhase.Stationary)
                {
                    transform.position = new Vector3(touchPos.x, touchPos.y, 0) - _offset;
                }
            }
        }
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, _maxLeft, _maxRight),
        Mathf.Clamp(transform.position.y, _maxDown, _maxUp), 0);

    }

    private void OnEnable()
    {
        EnhancedTouchSupport.Enable();
    }

    private void OnDisable()
    {
        EnhancedTouchSupport.Disable();
    }

    private IEnumerator SetBoundries()
    {
        yield return new WaitForSeconds(0.4f);
        _maxLeft = _mainCamera.ViewportToWorldPoint(new Vector2(_leftLimit, 0)).x;
        _maxRight = _mainCamera.ViewportToWorldPoint(new Vector2(_rightLimit, 0)).x;
        _maxDown = _mainCamera.ViewportToWorldPoint(new Vector2(0, _downLimit)).y;
        _maxUp = _mainCamera.ViewportToWorldPoint(new Vector2(0, _upLimit)).y;
    }
}
