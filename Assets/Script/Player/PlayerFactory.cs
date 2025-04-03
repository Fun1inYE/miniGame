using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 创建玩家的工厂
/// </summary>
public static class PlayerFactory
{
    /// <summary>
    /// 创建一个玩家
    /// </summary>
    /// <returns></returns>
    public static GameObject CreatePlayer()
    {
        string prefabPath = "Prefab/Player/Player";
        GameObject go = Resources.Load<GameObject>(prefabPath);
        return null;
    }
}
