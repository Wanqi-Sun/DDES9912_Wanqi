using UnityEngine;

public class OpenZoneTrigger : MonoBehaviour
{
    public DoorController door;
    public string playerTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
         Debug.Log($"[OpenZone] enter by: {other.name}, tag={other.tag}");
         if (!other.CompareTag(playerTag)) return;
         Debug.Log("[OpenZone] opening door");
         door.OpenDoor();
    }
}
