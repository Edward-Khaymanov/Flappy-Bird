using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class CoinProvider
{
    private AsyncOperationHandle _cachedHandle;
    private AssetReferenceSprite _coinAtlas;
    private List<Sprite> _sprites = new List<Sprite>();

    public async Task Init(AssetReferenceSprite coinAtlas)
    {
        _coinAtlas = coinAtlas;
        var handle = _coinAtlas.LoadAssetAsync<IList<Sprite>>();
        _cachedHandle = handle;
        await handle.Task;
        _sprites.AddRange(handle.Result);
    }

    public Sprite GetCoin(string key)
    {
        return _sprites.FirstOrDefault(x => x.name == key);
    }

    public void Unload()
    {
        Addressables.Release(_cachedHandle);
    }
}
