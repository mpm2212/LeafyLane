using System;
using UnityEngine;

public static class GameEvents
{
    public static event Action<GameObject> RemoveClouds;
    public static event Action<String> RegionUnlocked;
    public static event Action<bool> RockPlacedCorrectly;


    public static void RaiseRemoveClouds(GameObject cloudsToRemove) => RemoveClouds?.Invoke(cloudsToRemove);

    public static void RaiseRegionUnlocked(String region) => RegionUnlocked?.Invoke(region);
    public static void RaiseRockPlacedCorrectly(bool correct) => RockPlacedCorrectly?.Invoke(correct);
    




}
