using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public sealed class LoseLayout : CanvasLayout
{
    [SerializeField] private Button _menuButton;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Score _score;
    [SerializeField] private FlashView _flash;
    [SerializeField] private GameOverView _gameOver;
    [SerializeField] private ScoreBoardView _scoreBoard;
    [SerializeField] private LoseControlsView _controls;
    [SerializeField] private LayoutTransitionView _transition;
    [SerializeField] private UIAudioHandler _uiAudio;

    private IEnumerator _flashCoroutine;
    private IEnumerator _gameOverCoroutine;
    private IEnumerator _scoreBoardCoroutine;

    #region UNITY_EVENTS

    private void Start()
    {
        SetLoseActions();
    }

    private void OnEnable()
    {
        _menuButton.onClick.AddListener(OnMenu);
        _restartButton.onClick.AddListener(OnRestart);
        EventsHolder.GameStarted += OnStart;
        EventsHolder.GameLost += OnLose;
    }

    private void OnDisable()
    {
        _menuButton.onClick.RemoveListener(OnMenu);
        _restartButton.onClick.RemoveListener(OnRestart);
        EventsHolder.GameStarted -= OnStart;
        EventsHolder.GameLost -= OnLose;
    }

    #endregion

    public override void Show()
    {
        base.Show();
        var actions = GetLoseActions();
        StartCoroutine(AnimatedShow(actions, _controls.Show));
    }

    private IEnumerator AnimatedShow(List<IEnumerator> actions, Action callback)
    {
        foreach (var action in actions)
        {
            yield return StartCoroutine(action);
        }

        callback?.Invoke();
    }

    private List<IEnumerator> GetLoseActions()
    {
        var actions = new List<IEnumerator>();
        actions.Add(_flashCoroutine);
        actions.Add(_gameOverCoroutine);
        actions.Add(_scoreBoardCoroutine);

        return actions;
    }

    private void SetLoseActions()
    {
        var scoreBoard = new ScoreBoard(_score.CurrentScore, _score.Highscore);

        _flashCoroutine = _flash.Render();
        _gameOverCoroutine = _gameOver.Render();
        _scoreBoardCoroutine = _scoreBoard.Render(scoreBoard);
    }

    private void ResetScreen()
    {
        _flash.ResetView();
        _gameOver.ResetView();
        _scoreBoard.ResetView();
        _controls.Hide();
    }

    #region EVENT_HANDLERS

    private void OnRestart()
    {
        StartCoroutine(_transition.Render(EventsHolder.StartGame));
        _uiAudio.PlaySound();
    }

    private void OnMenu()
    {
        StartCoroutine(_transition.Render(EventsHolder.ShowMenu));
        _uiAudio.PlaySound();
        Hide();
        ResetScreen();
    }

    private void OnStart()
    {
        Hide();
        ResetScreen();
    }

    private void OnLose()
    {
        SetLoseActions();
        Show();
    }  

    #endregion

}
