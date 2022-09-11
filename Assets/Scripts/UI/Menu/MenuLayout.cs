using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public sealed class MenuLayout : CanvasLayout
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _scoreButton;
    [SerializeField] private ScoreView _highscoreView;
    [SerializeField] private Score _score;
    [SerializeField] private LayoutTransitionView _transition;
    [SerializeField] private UIAudioHandler _uiAudio;

    #region UNITY_EVENTS

    private void OnEnable()
    {
        _startButton.onClick.AddListener(OnStart);
        _scoreButton.onClick.AddListener(OnScore);
        EventsHolder.MenuOpened += OnMenu;
    }

    private void OnDisable()
    {
        _startButton.onClick.RemoveListener(OnStart);
        EventsHolder.MenuOpened -= OnMenu;
    }

    #endregion

    #region EVENT_HANDLERS

    private void OnStart()
    {
        StartCoroutine(_transition.Render(EventsHolder.StartGame));
        _uiAudio.PlaySound();
        Hide();
    }

    private void OnMenu()
    {
        Show();
    }

    private void OnScore()
    {
        if (_highscoreView.gameObject.activeSelf == false)
        {
            _highscoreView.Render(_score.Highscore);
            _highscoreView.gameObject.SetActive(true);
        }
        else
        {
            _highscoreView.gameObject.SetActive(false);
        }
    }

    #endregion
}
