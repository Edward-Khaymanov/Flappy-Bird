using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerPrefsSystem
{
    private const string HIGHSCORE_KEY = "highscore";
    
    public static int LoadHighscore() => PlayerPrefs.GetInt(HIGHSCORE_KEY);

    internal static void SaveHighscore(int highscore) => PlayerPrefs.SetInt(HIGHSCORE_KEY, highscore);
}
