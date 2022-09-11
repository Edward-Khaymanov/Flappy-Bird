using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public int CurrentScore { get; private set; }
    public int Highscore { get; private set; }

    private void Awake()
    {
        Highscore = PlayerPrefsSystem.LoadHighscore();
    }

    private void OnEnable()
    {
        EventsHolder.GameStarted += ResetCurrentScore;
    }

    private void OnDisable()
    {
        EventsHolder.GameStarted -= ResetCurrentScore;
    }

    public bool TrySetHighscore()
    {
        if (CurrentScore > Highscore)
        {
            Highscore = CurrentScore;
            PlayerPrefsSystem.SaveHighscore(Highscore);
            return true;
        }

        return false;
    }

    internal void AddScorePoint()
    {
        CurrentScore++;
    }

    private void ResetCurrentScore()
    {
        CurrentScore = 0;
    }
}
