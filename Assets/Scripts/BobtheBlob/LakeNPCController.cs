using System.Data.Common;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class LakeNPCController : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            GameEvents.RaiseTalkingToFreddyEvent(other.gameObject);
            Debug.Log("raised talking to freddy");
        }
    }

}
