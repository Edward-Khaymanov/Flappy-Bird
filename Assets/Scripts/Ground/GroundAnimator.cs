using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class GroundAnimator : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        EventsHolder.FirstJumping += StopAnimation;
        EventsHolder.MenuOpened += StartAnimation;
        EventsHolder.GameStarted += StartAnimation;
    }

    private void OnDisable()
    {
        EventsHolder.FirstJumping -= StopAnimation;
        EventsHolder.MenuOpened -= StartAnimation;
        EventsHolder.GameStarted -= StartAnimation;
    }

    private void StartAnimation()
    {
        _spriteRenderer.sortingOrder = AppInfo.AnimatedGroundLayerOrder;
        _animator.enabled = true;
    }

    private void StopAnimation()
    {
        _spriteRenderer.sortingOrder = -1;
        _animator.enabled = false;
    }    
}
