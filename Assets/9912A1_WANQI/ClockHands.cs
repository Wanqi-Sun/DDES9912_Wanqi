using UnityEngine;

public class ClockHands : MonoBehaviour
{
    public Transform pivotHour;
    public Transform pivotMinute;
    public Transform pivotSecond;

    void Start()
    {
        // 开始游戏时禁用脚本，等待按钮启动
        this.enabled = false;
    }

    void Update()
    {
        var t = System.DateTime.Now;

        float zSec   = -t.Second * 6f;                               // 360/60
        float zMin   = -(t.Minute * 6f + t.Second * 0.1f);           // 每秒多0.1°
        float zHour  = -((t.Hour % 12) * 30f + t.Minute * 0.5f);     // 360/12 + 分钟影响

        pivotSecond.localRotation = Quaternion.Euler(0, 0, zSec);
        pivotMinute.localRotation = Quaternion.Euler(0, 0, zMin);
        pivotHour  .localRotation = Quaternion.Euler(0, 0, zHour);
    }
}


