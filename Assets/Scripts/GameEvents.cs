using System;
using UnityEngine;

public static class GameEvents
{
    public static event Action<GameObject> RemoveCloudsEvent;
    public static event Action<String> RegionUnlockedEvent;
    public static event Action<bool> RockPlacedCorrectlyEvent;
    public static event Action<int, bool> FlowerPlacedEvent;
    public static event Action<GameObject> LightbulbPickedUpEvent;



    public static void RaiseRemoveClouds(GameObject cloudsToRemove) => RemoveCloudsEvent?.Invoke(cloudsToRemove);

    public static void RaiseRegionUnlocked(String region) => RegionUnlockedEvent?.Invoke(region);
    public static void RaiseRockPlacedCorrectly(bool correct) => RockPlacedCorrectlyEvent?.Invoke(correct);
    public static void RaiseFlowerPlacedEvent(int totalNumFlowers, bool more) => FlowerPlacedEvent.Invoke(totalNumFlowers, more);
    public static void RaiseLightbulbPickedUpEvent(GameObject lightbulb) => LightbulbPickedUpEvent.Invoke(lightbulb);

    




}
