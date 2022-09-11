using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BirdCollisionHandler : MonoBehaviour
{
    [SerializeField] private bool _isEnable;

    [SerializeField] private Score _score;

    private void OnEnable()
    {
        EventsHolder.GameStarted += OnStart;
    }

    private void OnDisable()
    {
        EventsHolder.GameStarted -= OnStart;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isEnable == false) return;

        if (collision.TryGetComponent(out ScoreZone scoreZone))
        {
            collision.enabled = false;
            _score.AddScorePoint();
            EventsHolder.PassScoreZone();
        }
        else
        {
            _isEnable = false;
            _score.TrySetHighscore();
            EventsHolder.Lose();
        }
    }

    private void OnStart()
    {
        _isEnable = true;
    }
}
