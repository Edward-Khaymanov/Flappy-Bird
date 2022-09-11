using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BirdMover))]
[RequireComponent(typeof(BirdRotator))]
[RequireComponent(typeof(Rigidbody2D))]
public class Bird : MonoBehaviour
{
    [SerializeField] private ContactFilter2D _filter;

    private Animator _flyAnimator;
    private BirdMover _mover;
    private BirdRotator _rotator;
    private TransformAnimator _idleAnimation;

    public Rigidbody2D Body { get; private set; }

    #region UNITY_EVENTS

    private void Awake()
    {
        Body = GetComponent<Rigidbody2D>();
        _flyAnimator = GetComponent<Animator>();
        _mover = GetComponent<BirdMover>();
        _rotator = GetComponent<BirdRotator>();
        _idleAnimation = GetComponent<TransformAnimator>();
    }

    private void OnEnable()
    {
        EventsHolder.GameStarted += OnStart;
        EventsHolder.GameLost += OnLose;
        EventsHolder.GamePaused += OnPause;
        EventsHolder.GameResumed += OnResume;
        EventsHolder.MenuOpened += OnMenu;
        EventsHolder.FirstJumping += OnFirstJump;
        EventsHolder.Jumped += OnJump;
        EventsHolder.Fell += OnFell;
    }

    private void OnDisable()
    {
        EventsHolder.GameStarted -= OnStart;
        EventsHolder.GameLost -= OnLose;
        EventsHolder.GamePaused -= OnPause;
        EventsHolder.GameResumed -= OnResume;
        EventsHolder.MenuOpened -= OnMenu;
        EventsHolder.FirstJumping -= OnFirstJump;
        EventsHolder.Jumped -= OnJump;
        EventsHolder.Fell -= OnFell;
    }

    #endregion

    public float DistanceToGround(ref RaycastHit2D[] raycastHit)
    {
        var distanse = 0f;
        var hitCount = Body.Cast(Vector2.down, _filter, raycastHit);
        if (hitCount > 0)
            distanse = raycastHit[0].distance;

        return distanse;
    }

    public Vector3 ClosestGroundFallPoint()
    {
        var hit = new RaycastHit2D[1];
        DistanceToGround(ref hit);
        var point = hit[0].point;
        return new Vector3(point.x, point.y + AppInfo.FallDisableOffsetY, transform.position.z);
    }

    #region EVENT_HANDLERS

    private void OnStart()
    {
        _flyAnimator.enabled = true;
        _mover.StartActions();
        _rotator.StartActions();
        _idleAnimation.StartAnimation();
    }

    private void OnLose()
    {
        _flyAnimator.enabled = false;
        _mover.LoseActions();
    }

    private void OnPause()
    {
        _flyAnimator.enabled = false;
        _mover.PauseActions();
    }

    private void OnResume()
    {
        _flyAnimator.enabled = true;
        _mover.ResumeActions();
    }

    private void OnMenu()
    {
        _flyAnimator.enabled = false;
        _idleAnimation.StopAnimation();
        _mover.MenuActions();
        _rotator.MenuActions();
    }

    private void OnJump()
    {
        _rotator.JumpActions();
    }

    private void OnFell()
    {
        _rotator.FellActions();
    }

    private void OnFirstJump()
    {
        _idleAnimation.StopAnimation();
        _rotator.FirstJumpActions();
    }

    #endregion

}
