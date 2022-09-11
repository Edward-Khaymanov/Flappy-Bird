using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventsHolder
{
    public static event Action PassedScoreZone;
    public static event Action GameLost;
    public static event Action GameStarted;
    public static event Action GamePaused;
    public static event Action GameResumed;
    public static event Action MenuOpened;
    public static event Action Jumped;
    public static event Action FirstJumping;
    public static event Action Fell;

    public static void Lose() => GameLost?.Invoke();
    public static void PassScoreZone() => PassedScoreZone?.Invoke();
    public static void StartGame() => GameStarted?.Invoke();
    public static void PauseGame() => GamePaused?.Invoke();
    public static void ResumeGame() => GameResumed?.Invoke();
    public static void ShowMenu() => MenuOpened?.Invoke();
    public static void Jump() => Jumped?.Invoke();
    public static void FirstJump() => FirstJumping?.Invoke();
    public static void StopFall() => Fell?.Invoke();
}
