using UnityEngine;

public class ClockManager : MonoBehaviour
{
    public MonoBehaviour[] controlScripts;
    public AudioSource clockAudio;  // 拖入音效组件

    public void StartClock()
    {
        foreach (var script in controlScripts)
            script.enabled = true;

        if (clockAudio != null && !clockAudio.isPlaying)
            clockAudio.Play();
    }

    public void StopClock()
    {
        foreach (var script in controlScripts)
            script.enabled = false;

        if (clockAudio != null && clockAudio.isPlaying)
            clockAudio.Pause();  // 用 Pause 而不是 Stop，可保留播放进度
    }
}

