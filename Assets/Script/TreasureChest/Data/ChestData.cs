using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 包厢类型的枚举类
/// </summary>
public enum ChestType
{
    // 金色  普通  不刷宝箱
    Gold, Normal, None
} 

/// <summary>
/// 宝箱数据
/// </summary>
[System.Serializable]
public class ChestData
{   
    /// <summary>
    /// 宝箱类型
    /// </summary>
    public ChestType chestType;
    /// <summary>
    /// 蓝宝石数量
    /// </summary>
    public int sapphire;
}
