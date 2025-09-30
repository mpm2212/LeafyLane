using System;
using UnityEngine;

public static class GameEvents
{
    public static event Action<GameObject> RemoveClouds;
    public static event Action<String> RegionUnlocked;


    public static void RaiseRemoveClouds(GameObject cloudsToRemove) => RemoveClouds?.Invoke(cloudsToRemove);

    public static void RaiseRegionUnlocked(String region) => RegionUnlocked?.Invoke(region);
    




}
