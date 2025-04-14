using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 魔法塔的其他逻辑
/// </summary>
public class MagicTower : Tower
{
    private void Awake()
    {
        InitCol();
    }

    /// <summary>
    /// 初始化Collider
    /// </summary>
    private void InitCol()
    {
        Collider2D col = ComponentFinder.GetOrAddComponent<BoxCollider2D>(gameObject);
        col.isTrigger = true;
    }

}
