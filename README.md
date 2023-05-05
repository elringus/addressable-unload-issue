## Description

As per [documentation](https://docs.unity3d.com/Packages/com.unity.addressables@1.21/manual/runtime/MemoryManagement.html) `Resources.UnloadUnusedAssets` should unload released assets even if other assets from the same bundle are still in use, but that's not the case.

## Repro steps

1. Build addressables
2. Build standalone player (I've been testing on Windows) with development build enabled
3. Open memory profiler and connect to the running player
4. Take snapshot and notice "LargeTexture" held by SpriteRenderer and asyncHandle, as expected
5. Focus the player window and press Space key to load "SmallTexture" and assign it to renderer
6.Take snapshot and notice "LargeTexture" is now held only by async handle, as expected
7. Focus the player window and press Space key to release "LargeTexture" and invoke Resources.UnloadUnusedAssets
8. Take memory snapshot again

 - Expected: "LargeTexture" has to be unloaded
 - Actual: "LargeTexture" is still loaded, while being held only by "AssetBundle"

Video: https://www.youtube.com/watch?v=tX8vrgie5N8

Issue tracker case number: `IN-40196`.

Associated forum thread: https://forum.unity.com/threads/resources-unloadunusedassets-cannot-unload-assets-without-any-references.1088116/
