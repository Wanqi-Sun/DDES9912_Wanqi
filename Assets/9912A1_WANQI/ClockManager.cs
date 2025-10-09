using UnityEngine;

public class ClockManager : MonoBehaviour
{
    // 拖进所有你想控制的脚本（如齿轮、指针、摆锤的脚本组件）
    public MonoBehaviour[] controlScripts;

    // 启动钟表
    public void StartClock()
    {
        foreach (var script in controlScripts)
        {
            script.enabled = true;
        }
    }

    // 停止钟表
    public void StopClock()
    {
        foreach (var script in controlScripts)
        {
            script.enabled = false;
        }
    }
}
