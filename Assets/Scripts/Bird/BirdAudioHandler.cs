using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdAudioHandler : MonoBehaviour
{
    [SerializeField] private AudioSource _fallingSound;
    [SerializeField] private AudioSource _flapSound;
    [SerializeField] private AudioSource _hitSound;

    private DateTime _hitTime;

    #region UNITY_EVENTS

    private void OnEnable()
    {
        EventsHolder.Jumped += OnJump;
        EventsHolder.GameLost += OnLose;
        EventsHolder.Fell += OnFell;
    }

    private void OnDisable()
    {
        EventsHolder.Jumped -= OnJump;
        EventsHolder.GameLost -= OnLose;
        EventsHolder.Fell -= OnFell;
    }

    #endregion

    private void OnLose()
    {
        _hitSound.Play();
        _hitTime = DateTime.Now;
        _fallingSound.PlayDelayed(_hitSound.clip.length - AppInfo.FallingSoundDelayOffset);
    }

    private void OnFell()
    {
        var dif = DateTime.Now - _hitTime;
        if (dif < TimeSpan.FromSeconds(AppInfo.FallingSoundDelay))
            _fallingSound.Stop();
    }

    private void OnJump()
    {
        _flapSound.Play();
    }
}
