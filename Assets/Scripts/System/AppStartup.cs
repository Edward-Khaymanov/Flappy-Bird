using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

public class AppStartup : MonoBehaviour
{
    [SerializeField] private AssetReferenceSprite _coinAtlasReference;

    private CoinProvider _coinProvider;

    private async void Awake()
    {
        Application.targetFrameRate = 60;

        _coinProvider = new CoinProvider();
        await _coinProvider.Init(_coinAtlasReference);

        var bronzeCoin = _coinProvider.GetCoin(AppInfo.BronzeCoinName);
        var silverCoin = _coinProvider.GetCoin(AppInfo.SilverCoinName);
        var goldCoin = _coinProvider.GetCoin(AppInfo.GoldCoinName);
        var platinumCoin = _coinProvider.GetCoin(AppInfo.PlatinumCoinName);

        AppInfo.BronzeCoin = bronzeCoin;
        AppInfo.SilverCoin = silverCoin;
        AppInfo.GoldCoin = goldCoin;
        AppInfo.PlatinumCoin = platinumCoin;
    }
}
