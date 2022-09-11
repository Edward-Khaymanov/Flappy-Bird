using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Bird))]
public class BirdMover : MonoBehaviour
{
    [SerializeField] private bool _isEnable;

    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private Vector3 _startPosition;

    private Vector3 _hidePostion = new Vector3(0, 20);
    private IEnumerator _fallCoroutine;
    private Bird _bird;
    private Rigidbody2D _body;

    private bool IsTouched => Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId) == false;

    #region UNITY_EVENTS

    private void Awake()
    {
        _bird = GetComponent<Bird>();
        _fallCoroutine = StopFalling();
    }

    private void Start()
    {
        _body = _bird.Body;
    }

    private void Update()
    {
        if (_isEnable && IsTouched)
        {
            Jump();
        }
    }

    private void OnBecameInvisible()
    {
        _isEnable = false;
    }

    private void OnBecameVisible()
    {
        if (Camera.current.cameraType == CameraType.SceneView)
            return;

        _isEnable = true;
    }

    #endregion

    public void StartActions()
    {
        StopCoroutine(_fallCoroutine);
        ResetMover();
        _isEnable = true;
    }

    public void LoseActions()
    {
        _isEnable = false;
        _fallCoroutine = StopFalling();
        StartCoroutine(_fallCoroutine);
    }

    public void PauseActions()
    {
        _isEnable = false;
        _body.simulated = false;
    }

    public void ResumeActions()
    {
        _isEnable = true;
        if (AppInfo.IsFirstJump == false)
            _body.simulated = true;
    }

    public void MenuActions()
    {
        _body.simulated = false;
        _isEnable = false;
        StopCoroutine(_fallCoroutine);
        Hide();
    }

    private void Jump()
    {
        if (AppInfo.IsFirstJump)
        {
            AppInfo.IsFirstJump = false;
            EventsHolder.FirstJump();
            _body.simulated = true;
        }

        _body.velocity = new Vector2(_speed, 0);
        _body.AddForce(Vector2.up * _jumpForce, ForceMode2D.Force);
        EventsHolder.Jump();
    }

    private IEnumerator StopFalling()
    {
        var groundSurfacePoint = _bird.ClosestGroundFallPoint();

        _body.velocity = new Vector2(0, _body.velocity.y);

        while (transform.position.y > groundSurfacePoint.y)
        {
            yield return null;
        }

        _body.simulated = false;
        transform.position = groundSurfacePoint;

        yield return null;
        EventsHolder.StopFall();

        yield break;
    }

    private void ResetMover()
    {
        AppInfo.IsFirstJump = true;
        transform.position = _startPosition;
        _body.velocity = Vector2.zero;
    }

    private void Hide()
    {
        transform.position = _hidePostion;
    }

}
