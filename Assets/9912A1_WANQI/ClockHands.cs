using UnityEngine;

public class ClockHands : MonoBehaviour
{
    public Transform pivotHour;
    public Transform pivotMinute;
    public Transform pivotSecond;

    void Awake()
    {
        this.enabled = false;  // 提前禁用，防止 Update 提前运行
    }

    void Update()
    {
        var t = System.DateTime.Now;

        float zSec = -t.Second * 6f;
        float zMin = -(t.Minute * 6f + t.Second * 0.1f);
        float zHour = -((t.Hour % 12) * 30f + t.Minute * 0.5f);

        pivotSecond.localRotation = Quaternion.Euler(0, 0, zSec);
        pivotMinute.localRotation = Quaternion.Euler(0, 0, zMin);
        pivotHour.localRotation = Quaternion.Euler(0, 0, zHour);
    }
}


