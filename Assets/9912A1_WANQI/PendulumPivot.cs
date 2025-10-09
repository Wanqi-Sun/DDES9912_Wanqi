using UnityEngine;

public class PendulumSwing : MonoBehaviour
{
    [Tooltip("最大摆角（度），例如 20 度")]
    public float amplitude = 20f;

    [Tooltip("往返一次所需时间（秒），例如 2 秒")]
    public float period = 2f;

    [Tooltip("初始相位（度），多个钟同场时可错相")]
    public float phaseDeg = 0f;

    [Tooltip("阻尼，0=不衰减，>0 逐渐变小")]
    public float damping = 0f;

    float t0;

    void OnEnable() => t0 = Time.time;

    void Start()
    {
        // 开始游戏时禁用脚本，等待按钮启动
        this.enabled = false;
    }

    void Update()
    {
        float t = Time.time - t0;
        float theta = amplitude * Mathf.Exp(-damping * t)
                    * Mathf.Sin((2f * Mathf.PI / period) * t + phaseDeg * Mathf.Deg2Rad);

        // 绕Z轴左右摆（顺/逆时针）
        transform.localRotation = Quaternion.Euler(0f, 0f, theta);
    }
}
