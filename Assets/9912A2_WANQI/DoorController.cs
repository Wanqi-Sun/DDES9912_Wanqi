using UnityEngine;
using System.Collections;

public class DoorController : MonoBehaviour
{
    [Header("Door Pieces")]
    public Transform hingePivot;      // 指向 HingePivot

    [Header("Rotation")]

    [Header("Env Lights")]
    public Light sunLight;      // 场景里的 Directional Light（太阳）
    public Light headLamp;      // 刚才做好的玩家头灯

    public float closedY = 0f;        // 关门角度（HingePivot 的本地 Y）
    public float openY = 90f;         // 开门角度（根据门朝向可能是 -90）
    public float rotateSpeed = 140f;  // 旋转速度（度/秒）

    Coroutine rotCo;

    void Start()
    {
        // 运行时强制门在“关闭”角度
        Vector3 e = hingePivot.localEulerAngles;
        e.y = closedY;
        hingePivot.localEulerAngles = e;

        SetIndoor(false);   // 先默认在室外
    }

    public void OpenDoor()
    {
        Debug.Log($"[Door] OpenDoor() called. hinge={hingePivot?.name}, nowY={hingePivot.localEulerAngles.y}, target={openY}");
        if (rotCo != null) StopCoroutine(rotCo);
        rotCo = StartCoroutine(RotateTo(openY));
    }

    public void CloseDoor()
{
    Debug.Log("[Door] CloseDoor() called");
    if (rotCo != null) StopCoroutine(rotCo);
    rotCo = StartCoroutine(RotateTo(closedY));
}

    public void SetIndoor(bool inside)
{
    // inside = true 表示在屋内；false 表示屋外
    if (sunLight != null)
        sunLight.enabled = !inside;     // 屋内关太阳光，屋外开

    if (headLamp != null)
        headLamp.gameObject.SetActive(inside);  // 屋内开头灯，屋外关头灯
}


    IEnumerator RotateTo(float targetY)
    {
        float eps = 0.5f;
        int step = 0;
        while (true)
        {
            float currentY = hingePivot.localEulerAngles.y;
            float nextY = Mathf.MoveTowardsAngle(currentY, targetY, rotateSpeed * Time.deltaTime);

            Vector3 e = hingePivot.localEulerAngles;
            e.y = nextY;
            hingePivot.localEulerAngles = e;

            if (step++ % 10 == 0) // 每10帧打印一次，避免刷屏
                Debug.Log($"[Door] rotating... {currentY:0.0} -> {nextY:0.0} (target {targetY})");

            if (Mathf.Abs(Mathf.DeltaAngle(nextY, targetY)) <= eps) break;
            yield return null;
        }
        Vector3 f = hingePivot.localEulerAngles; f.y = targetY; hingePivot.localEulerAngles = f;
        Debug.Log($"[Door] reached target {targetY}");
        rotCo = null;
    }


}