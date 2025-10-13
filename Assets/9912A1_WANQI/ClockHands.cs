using UnityEngine;
using System;

public class ClockHands : MonoBehaviour
{
    public Transform pivotHour;
    public Transform pivotMinute;
    public Transform pivotSecond;

    private DateTime startTime;          // 启动时刻（用于计算）
    private DateTime currentTime;        // 当前显示的时间
    private bool isRunning = false;      // 控制是否旋转

    // 手动设定时间
    public void SetManualTime(int hour, int minute, int second)
    {
        currentTime = new DateTime(1, 1, 1, hour % 24, minute % 60, second % 60);
        ApplyRotation(currentTime);
    }

    // 启动使用系统时间开始运转
    public void UseSystemTime()
    {
        startTime = DateTime.Now;
        TimeSpan offset = currentTime.TimeOfDay;
        currentTime = startTime.Date + offset;
        isRunning = true;
    }

    // 停止指针旋转
    public void StopSystemTime()
    {
        isRunning = false;
    }

    void Update()
    {
        if (isRunning)
        {
            // 增加时间（用deltaTime累积）
            currentTime = currentTime.AddSeconds(Time.deltaTime);
            ApplyRotation(currentTime);
        }
    }

    // 根据时间设置指针角度
    private void ApplyRotation(DateTime time)
    {
        float sec = time.Second + time.Millisecond / 1000f;
        float min = time.Minute + sec / 60f;
        float hour = (time.Hour % 12) + min / 60f;

        float zSec = -sec * 6f;
        float zMin = -min * 6f;
        float zHour = -hour * 30f;

        pivotSecond.localRotation = Quaternion.Euler(0, 0, zSec);
        pivotMinute.localRotation = Quaternion.Euler(0, 0, zMin);
        pivotHour.localRotation = Quaternion.Euler(0, 0, zHour);
    }
}





