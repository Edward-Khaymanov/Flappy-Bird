using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraTracker : MonoBehaviour
{
    [SerializeField] private bool _isEnable;

    [SerializeField] private float _xOffset;
    [SerializeField] private Vector3 _startPosition;
    [SerializeField] private GameObject _target;

    private float _aspect;
    private Camera _camera;

    #region UNITY_EVENTS

    private void Start()
    {
        _camera = GetComponent<Camera>();
        _aspect = _camera.aspect;
    }

    private void OnEnable()
    {
        EventsHolder.GameStarted += OnStart;
        EventsHolder.GameLost += OnLose;
        EventsHolder.MenuOpened += OnMenu;
    }

    private void OnDisable()
    {
        EventsHolder.GameStarted -= OnStart;
        EventsHolder.GameLost -= OnLose;
        EventsHolder.MenuOpened -= OnMenu;
    }

    private void Update()
    {
        Track();
    }

    #endregion

    private void Track()
    {
        if (_isEnable)
        {
            transform.position = new Vector3(_target.transform.position.x + _xOffset * _aspect, transform.position.y, transform.position.z);
        }
    }

    #region EVENT_HANDLERS

    private void OnStart()
    {
        _isEnable = true;
    }

    private void OnLose()
    {
        _isEnable = false;
    }

    private void OnMenu()
    {
        _isEnable = false;
        transform.position = _startPosition;
    }

    #endregion
}
