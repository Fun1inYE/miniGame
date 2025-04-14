using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 辅助塔数据类
/// </summary>
public class AssistedTowerData : TowerData
{
    /// <summary>
    /// 影响数值
    /// </summary>
    private float effectValues;

    public float EffectValues { get => effectValues; set => effectValues = value; }

    public AssistedTowerData()
    {
    }

    public AssistedTowerData(string name, string description, float range, float frequency, int health, float effectValues) : base(name, description, range, frequency, health)
    {
        this.effectValues = effectValues;
    }
}
