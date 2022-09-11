using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLayout : CanvasLayout
{
    [SerializeField] private Button _pauseButton;
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _menuButton;
    [SerializeField] private GameObject _background;
    [SerializeField] private Score _score;
    [SerializeField] private ScoreView _currentScore;
    [SerializeField] private LayoutTransitionView _transition;
    [SerializeField] private UIAudioHandler _uiAudio;

    #region UNITY_EVENTS

    private void OnEnable()
    {
        EventsHolder.GameStarted += OnStart;
        EventsHolder.GameLost += OnLose;
        EventsHolder.PassedScoreZone += OnScoreZonePassed;
        _menuButton.onClick.AddListener(ShowMenu);
        _pauseButton.onClick.AddListener(Pause);
        _resumeButton.onClick.AddListener(Resume);
    }

    private void OnDisable()
    {
        EventsHolder.GameStarted -= OnStart;
        EventsHolder.GameLost -= OnLose;
        EventsHolder.PassedScoreZone -= OnScoreZonePassed;
        _menuButton.onClick.RemoveListener(ShowMenu);
        _pauseButton.onClick.RemoveListener(Pause);
        _resumeButton.onClick.RemoveListener(Resume);
    }

    #endregion

    private void SwitchState(bool isPaused)
    {
        _background.SetActive(isPaused);
        _menuButton.gameObject.SetActive(isPaused);
        _resumeButton.gameObject.SetActive(isPaused);
        _pauseButton.gameObject.SetActive(!isPaused);
    }

    #region EVENT_HANDLERS

    private void OnStart()
    {
        _currentScore.Render(0);
        Show();
    }

    private void OnLose()
    {
        Hide();
    }

    private void OnScoreZonePassed()
    {
        _currentScore.Render(_score.CurrentScore);
    }

    private void ShowMenu()
    {
        _uiAudio.PlaySound();
        StartCoroutine(_transition.Render(EventsHolder.ShowMenu, () => Time.timeScale = 1));
        Hide();
        SwitchState(false);
    }

    private void Pause()
    {
        Time.timeScale = 0;
        SwitchState(true);
        EventsHolder.PauseGame();
    }

    private void Resume()
    {
        Time.timeScale = 1;
        SwitchState(false);
        EventsHolder.ResumeGame();
    }

    #endregion
}
