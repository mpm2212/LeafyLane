using System;
using UnityEngine;

public class GameEvents
{
    public static event Action<GameObject> RemoveClouds;


    public static void RaiseRemoveClouds(GameObject cloudsToRemove) => RemoveClouds?.Invoke(cloudsToRemove);
    




}
