using Unity.VisualScripting;
using UnityEngine;

public class LampTooltip : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            GameCanvasController.Instance.ToggleLampTooltip();
            Destroy(this.gameObject);
        }
    }
}
