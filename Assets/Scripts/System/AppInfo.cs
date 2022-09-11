using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class AppInfo
{
    public const float FallDisableOffsetY = 0.3f;
    public const float RotateDisableDistance = 1f;

    public const float FallingSoundDelay = 0.25f;
    public const float FallingSoundDelayOffset = 0.1f;
    
    public const string BronzeCoinName = "BronzeCoin";
    public const string SilverCoinName = "SilverCoin";
    public const string GoldCoinName = "GoldCoin";
    public const string PlatinumCoinName = "PlatinumCoin";

    public static Sprite BronzeCoin;
    public static Sprite SilverCoin;
    public static Sprite GoldCoin;
    public static Sprite PlatinumCoin;

    public static bool IsFirstJump = true;

    public static int AnimatedGroundLayerOrder = 4;
}
