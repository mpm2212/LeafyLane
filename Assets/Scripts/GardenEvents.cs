using System;
using UnityEditor;
using UnityEngine;

public class GardenEvents
{
    public static event Action<int, bool> FlowerPlacedEvent;

    public static void RaiseFlowerPlacedEvent(int totalNumFlowers, bool more) => FlowerPlacedEvent.Invoke(totalNumFlowers, more); 

}
