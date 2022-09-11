using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;
using System.Linq;
public class ScoreBoard
{
    public ScoreBoard(int currentScore, int highcore)
    {
        CurrentScore = currentScore;
        Highscore = highcore;
    }

    public int CurrentScore { get; private set; }
    public int Highscore { get; private set; }
    public Sprite CoinSprite
    {
        get
        {
            switch (CurrentScore)
            {
                case >= 10 and < 20:
                    return AppInfo.BronzeCoin;
                case >= 20 and < 30:
                    return AppInfo.SilverCoin;
                case >= 30 and < 40:
                    return AppInfo.GoldCoin;
                case >= 40:
                    return AppInfo.PlatinumCoin;
                default:
                    return null;
            }
        }
    }
}
