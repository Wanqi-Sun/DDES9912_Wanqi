using UnityEngine;
using System.Collections;


public class GhostHeadFollow : MonoBehaviour
{
    [Header("Follow Target")]
    public Transform target;          // 玩家或玩家的主体（不是相机也可以）

    [Header("Follow Settings")]
    public float moveSpeed = 2f;      // 移动速度（地上爬的速度）
    public float stopDistance = 1.5f; // 离玩家多近就不再贴着往上撞
    public float groundHeight = 0.1f; // 幽灵头离地高度（根据你地板高度调）

    [Header("Stop Hint Settings")]
    public float bounceHeight = 0.4f;   // 每次向上弹多高
    public int bounceTimes = 5;         // 上下抖动次数
    public int dripTimes = 6;           // 滴水次数
    public AudioClip dripClip;          // 滴水音效剪辑

    AudioSource dripSource;
    bool isPlayingHint = false;

void Awake()
{
    dripSource = GetComponent<AudioSource>();
}


    void Update()
    {
        if (target == null) return;

        // 当前幽灵位置，强制在地面高度
        Vector3 currentPos = transform.position;
        currentPos.y = groundHeight;

        // 玩家位置，也投影到地面（只在平面上追）
        Vector3 targetPos = target.position;
        targetPos.y = groundHeight;

        // 计算与玩家的距离
        float dist = Vector3.Distance(currentPos, targetPos);

        // 如果距离大于 stopDistance，就向玩家方向移动
        if (dist > stopDistance)
        {
            Vector3 dir = (targetPos - currentPos).normalized;
            currentPos += dir * moveSpeed * Time.deltaTime;
        }

        // 更新位置
        transform.position = currentPos;

        // 让幽灵头在水平方向上朝向玩家
        Vector3 lookTarget = target.position;
        lookTarget.y = groundHeight;
        transform.LookAt(lookTarget);
    }
    public void PlayStopHint()
    {
    if (isPlayingHint) return;          // 防止重复播放
    if (!gameObject.activeInHierarchy) return;

    StartCoroutine(StopHintRoutine());
    }

    IEnumerator StopHintRoutine()
    {
    isPlayingHint = true;

    // 暂停追逐逻辑
    enabled = false;

    Vector3 basePos = transform.position;

    // 1）先上下抖动 bounceTimes 次
    // 1）先上下抖动 bounceTimes 次
    for (int i = 0; i < bounceTimes; i++)
{
    Vector3 upPos = basePos + Vector3.up * bounceHeight;

    float t = 0f;
    float duration = 0.25f;    // ← 改成 0.25f
    while (t < duration)
    {
        float lerp = t / duration;
        transform.position = Vector3.Lerp(basePos, upPos, lerp);
        t += Time.deltaTime;
        yield return null;
    }

    t = 0f;
    while (t < duration)
    {
        float lerp = t / duration;
        transform.position = Vector3.Lerp(upPos, basePos, lerp);
        t += Time.deltaTime;
        yield return null;
    }

    // 👇 新增：每次抖完稍微停一下
    yield return new WaitForSeconds(0.1f);
}


    // 2）播放滴水声 dripTimes 次
    float interval = 0.6f; // 默认值
    if (dripClip != null)
{
    interval = dripClip.length + 0.1f;  // 音频长度 + 0.1 秒间隔
}

    for (int i = 0; i < dripTimes; i++)
{
    if (dripSource != null && dripClip != null)
    {
        dripSource.pitch = 1f;          // 确保不被拉快
        dripSource.PlayOneShot(dripClip);
    }
    yield return new WaitForSeconds(interval);
}


    // 确保位置回到地面原位
    transform.position = basePos;

    // 恢复追逐
    enabled = true;
    isPlayingHint = false;
    }


}

