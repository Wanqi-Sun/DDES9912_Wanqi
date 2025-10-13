using UnityEngine;
using TMPro;   // 需要导入 TMP 才能访问 TMP_InputField

public class ClockManager : MonoBehaviour
{
    [Header("Clock Control Scripts")]
    public MonoBehaviour[] controlScripts;   // 拖入 GearRotate, PendulumPivot, ClockHands 等脚本

    [Header("Audio")]
    public AudioSource clockAudio;           // 拖入 Tick 声音

    [Header("Time Input Panel UI")]
    public GameObject timeInputPanel;        // 拖入 TimeInputPanel
    public TMP_InputField hourInput;         // 拖入 HourInput (TMP)
    public TMP_InputField minuteInput;       // 拖入 MinuteInput (TMP)
    public TMP_InputField secondInput;       // 拖入 SecondInput (TMP)

    [Header("Clock Hands")]
    public ClockHands clockHands;            // 拖入 ClockHands 脚本对象，用于更新指针时间

    void Start()
    {
        // 默认隐藏输入面板
        if (timeInputPanel != null)
            timeInputPanel.SetActive(false);

        // 默认锁定鼠标（保持原有控制逻辑）
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // 点击 Start 按钮
    public void StartClock()
    {
        foreach (var script in controlScripts)
            script.enabled = true;

        if (clockHands != null)
            clockHands.UseSystemTime();

        if (clockAudio != null && !clockAudio.isPlaying)
            clockAudio.Play();
    }

    // 点击 Stop 按钮
    public void StopClock()
    {
        foreach (var script in controlScripts)
            script.enabled = false;

        if (clockAudio != null && clockAudio.isPlaying)
            clockAudio.Pause();
    }

    // 点击 Set Time 按钮
    public void ShowTimeInputPanel()
    {
        if (timeInputPanel != null)
            timeInputPanel.SetActive(true);

        // 解锁鼠标，允许输入
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // 点击 OK 按钮
    public void SetTimeFromInput()
    {
        int hour = 0, minute = 0, second = 0;

        // 解析用户输入（防止空输入报错）
        if (!string.IsNullOrEmpty(hourInput.text))
            int.TryParse(hourInput.text, out hour);
        if (!string.IsNullOrEmpty(minuteInput.text))
            int.TryParse(minuteInput.text, out minute);
        if (!string.IsNullOrEmpty(secondInput.text))
            int.TryParse(secondInput.text, out second);

        // 更新时钟的显示时间
        if (clockHands != null)
            clockHands.SetManualTime(hour, minute, second);

        // 隐藏输入面板
        if (timeInputPanel != null)
            timeInputPanel.SetActive(false);

        // 重新锁定鼠标
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Debug.Log($"⏰ Time manually set to {hour:D2}:{minute:D2}:{second:D2}");
    }
}
