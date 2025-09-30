using System;
using UnityEngine;

public static class GameEvents
{
    public static event Action<GameObject> RemoveCloudsEvent;
    public static event Action<String> RegionUnlockedEvent;
    public static event Action<bool> RockPlacedCorrectlyEvent;
    public static event Action<int, bool> FlowerPlacedEvent;
    public static event Action<int> LightbulbPickedUpEvent;
    public static event Action<int> LightbulbPlacedEvent;
    public static event Action<GameObject> TalkingToFreddyEvent;
    public static event Action<GameObject> FoundBobEvent;
    public static event Action<GameObject> DidntFindBobEvent;
    public static event Action<GameObject> PlayerEnteredForestEvent;



    public static void RaiseRemoveClouds(GameObject cloudsToRemove) => RemoveCloudsEvent?.Invoke(cloudsToRemove);

    public static void RaiseRegionUnlocked(String region) => RegionUnlockedEvent?.Invoke(region);
    public static void RaiseRockPlacedCorrectly(bool correct) => RockPlacedCorrectlyEvent?.Invoke(correct);
    public static void RaiseFlowerPlacedEvent(int totalNumFlowers, bool more) => FlowerPlacedEvent?.Invoke(totalNumFlowers, more);
    public static void RaiseLightbulbPickedUpEvent(int numLightbulbs) => LightbulbPickedUpEvent?.Invoke(numLightbulbs);
    public static void RaiseLightBulbPlacedEvent(int numLightbulbs) => LightbulbPlacedEvent?.Invoke(numLightbulbs);
    public static void RaiseTalkingToFreddyEvent(GameObject obj) => TalkingToFreddyEvent?.Invoke(obj);
    public static void RaiseFoundBobEvent(GameObject obj) => FoundBobEvent?.Invoke(obj);
    public static void RaiseDidntFindBobEvent(GameObject obj) => DidntFindBobEvent?.Invoke(obj);
    public static void RaisePlayerEnteredForestEvent(GameObject obj) => PlayerEnteredForestEvent?.Invoke(obj);

}
