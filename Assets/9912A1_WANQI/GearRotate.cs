using UnityEngine;

public class GearRotate : MonoBehaviour
{
    [Tooltip("每秒角速度（度）。顺时针用负数，逆时针用正数。")]
    public float speedDegPerSec = -30f;

    [Tooltip("旋转轴：通常选 Z 轴（Vector3.forward）")]
    public Vector3 axis = Vector3.forward; // Z 轴 = 面向/背向屏幕

    [Tooltip("在世界坐标绕轴转更直观，除非你要跟随局部旋转")]
    public Space space = Space.World; // 或 Space.Self

    void Start()
    {
        // 开始游戏时禁用脚本，等待按钮启动
        this.enabled = false;
    }
    void Update()
    {
        transform.Rotate(axis, speedDegPerSec * Time.deltaTime, space);
    }
}

