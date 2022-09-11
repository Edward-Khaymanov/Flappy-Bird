using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public sealed class PipeGenerator : ObjectPool
{
    [SerializeField] private bool _isEnable;

    [SerializeField] private int _count;
    [SerializeField] private float _maxSpawnPositionY;
    [SerializeField] private float _minSpawnPositionY;
    [SerializeField] private float _intervalBeetweenSpawn;
    [SerializeField] private Vector2 _disablePoint;

    private float _elapsedTime;
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
        var isGetted = TryGetObject(out GameObject pipe);
        if (isGetted)
        {
            _elapsedTime = 0;

            pipe.transform.position = new Vector3(transform.position.x, Random.Range(_minSpawnPositionY, _maxSpawnPositionY), transform.position.z);
            pipe.SetActive(true);
        }

        return isGetted;
    }

    private void DisableOffscreenObjects()
    {
        var disablePoint = _camera.ViewportToWorldPoint(_disablePoint);

        foreach (var pipePrefab in _pool)
        {
            if (pipePrefab.activeSelf && pipePrefab.transform.position.x < disablePoint.x)
                ResetPipe(pipePrefab);
        }
    }

    private void ResetPipe(GameObject pipePrefab)
    {
        pipePrefab.GetComponent<Pipe>().ScoreZoneCollider.enabled = true;
        pipePrefab.SetActive(false);
    }

    protected override void ResetPool()
    {
        _elapsedTime = 0;
        foreach (var pipePrefab in _pool)
        {
            ResetPipe(pipePrefab);
        }
    }

    #region EVENT_HANDLERS

    private void OnStart()
    {
        ResetPool();
    }

    private void OnFirstJump()
    {
        _isEnable = true;
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
        ResetPool();
    }

    #endregion
}
