using UnityEngine;

/// <summary>
/// Vector2的拓展方法
/// </summary>
public static class Vector2Extensions
{
    /// <summary>
    /// 拓展的Vector2的相加方法
    /// </summary>
    /// <param name="v1">向量1</param>
    /// <param name="v2">向量2</param>
    /// <returns>返回的2维向量</returns>
    public static Vector2 Add(this Vector2 v1, Vector2 v2)
    {
        return new Vector2(v1.x + v2.x, v1.y + v2.y);
    }
}
