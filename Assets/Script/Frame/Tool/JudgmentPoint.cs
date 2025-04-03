using UnityEngine;

/// <summary>
/// 判断点的类
/// </summary>
public static class JudgmentPoint
{
    /// <summary>
    /// 判断点是否在屏幕内
    /// </summary>
    /// <param name="point"></param>
    /// <returns></returns>
    public static bool IsInScreen(Vector2 point)
    {
        Vector2 viewportPoint = Camera.main.WorldToViewportPoint(point);
        // 判断点是否在视口内
        return viewportPoint.x >= 0 && viewportPoint.x <= 1 &&
                viewportPoint.y >= 0 && viewportPoint.y <= 1;
    }
}
