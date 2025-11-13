using UnityEngine;

public class InsideThresholdTrigger : MonoBehaviour
{
    public DoorController door;      // 拖 DoorRoot 上的 DoorController 进来
    public string playerTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(playerTag)) return;

        // 先切环境：变成屋内（关太阳，开头灯）
        door.SetIndoor(true);

        // 再关门
        door.CloseDoor();
    }
}