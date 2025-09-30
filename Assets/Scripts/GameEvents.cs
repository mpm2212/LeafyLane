using System;
using UnityEngine;

public static class GameEvents
{
    public static event Action<GameObject> RemoveClouds;


    public static void RaiseRemoveClouds(GameObject cloudsToRemove) => RemoveClouds?.Invoke(cloudsToRemove);
    




}
