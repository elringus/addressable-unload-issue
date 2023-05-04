using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class Loader : MonoBehaviour
{
    public SpriteRenderer Renderer;
    public string LargeTextureAddress;
    public string SmallTextureAddress;
    public KeyCode ContinueKey = KeyCode.Space;

    private IEnumerator Start ()
    {
        Debug.Log("Loading large texture...");
        var largeTextureHandle = Addressables.LoadAssetAsync<Sprite>(LargeTextureAddress);
        while (!largeTextureHandle.IsDone) yield return null;
        Renderer.sprite = largeTextureHandle.Result;
        Debug.Log($"Large texture loaded. Press {ContinueKey} to continue.");
        while (!Input.GetKeyUp(ContinueKey)) yield return null;

        Debug.Log("Loading small texture...");
        var smallTextureHandle = Addressables.LoadAssetAsync<Sprite>(SmallTextureAddress);
        while (!smallTextureHandle.IsDone) yield return null;
        Renderer.sprite = smallTextureHandle.Result;
        Debug.Log($"Small texture loaded. Press {ContinueKey} to continue.");
        while (!Input.GetKeyUp(ContinueKey)) yield return null;

        Debug.Log("Releasing large texture and unloading unused assets...");
        Addressables.Release(largeTextureHandle);
        var unloadHandle = Resources.UnloadUnusedAssets();
        while (!unloadHandle.isDone) yield return null;
        Debug.Log("Large texture should now be unloaded.");
    }
}
