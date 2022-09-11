using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundGenerator : ObjectPool
{
    [SerializeField] private bool _isEnable;

    [SerializeField] private int _count;
    [SerializeField] private float _offsetX;
    [SerializeField] private float _intervalBeetweenSpawn;
    [SerializeField] private Vector2 _disablePoint;
    [SerializeField] private Vector3 _startPosition;
    
    private float _elapsedTime;
    private Vector3 _lastGroundPosition;
    private Camera _camera;

    #region UNITY_EVENTS

    private void Start()
    {
        _camera = Camera.main;
        Initialize(_count);
    }

    private void OnEnable()
    {
        EventsHolder.GameStarted += OnStart;
        EventsHolder.FirstJumping += OnFirstJump;
        EventsHolder.GameLost += OnLose;
        EventsHolder.GamePaused += OnPause;
        EventsHolder.GameResumed += OnResume;
        EventsHolder.MenuOpened += OnMenu;
    }

    private void OnDisable()
    {
        EventsHolder.GameStarted -= OnStart;
        EventsHolder.FirstJumping -= OnFirstJump;
        EventsHolder.GameLost -= OnLose;
        EventsHolder.GamePaused -= OnPause;
        EventsHolder.GameResumed -= OnResume;
        EventsHolder.MenuOpened -= OnMenu;
    }

    private void Update()
    {
        if (_isEnable)
        {
            _elapsedTime += Time.deltaTime;

            if (_elapsedTime > _intervalBeetweenSpawn)
            {
                TrySpawn();
                DisableOffscreenObjects();
            }
        }
    }

    #endregion

    private bool TrySpawn()
    {
        var isGetted = TryGetObject(out GameObject ground);
        if (isGetted)
        {
            _elapsedTime = 0;

            ground.transform.position = new Vector3(_lastGroundPosition.x + _offsetX, transform.position.y, 3);
            ground.SetActive(true);

            _lastGroundPosition = ground.transform.position;
        }

        return isGetted;
    }

    private void DisableOffscreenObjects()
    {
        var disablePoint = _camera.ViewportToWorldPoint(_disablePoint);

        foreach (var ground in _pool)
        {
            if (ground.activeSelf && ground.transform.position.x < disablePoint.x)
                ground.SetActive(false);
        }
    }

    protected override void ResetPool()
    {
        _elapsedTime = 0;
        _lastGroundPosition = _startPosition;
        base.ResetPool();
    }

    #region EVENT_HANDLERS

    private void OnStart()
    {
        ResetPool();
    }

    private void OnFirstJump()
    {
        _isEnable = true;
        TrySpawn();
    }

    private void OnLose()
    {
        _isEnable = false;
    }

    private void OnPause()
    {
        _isEnable = false;
    }

    private void OnResume()
    {
        if (AppInfo.IsFirstJump)
            return;

        _isEnable = true;
    }

    private void OnMenu()
    {
        _isEnable = false;
        _elapsedTime = 0;
        ResetPool();
    }

    #endregion
}
