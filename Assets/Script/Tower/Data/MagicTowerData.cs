using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 魔法塔的数据
/// </summary>
public class MagicTowerData : AttackTowerData
{
    public MagicTowerData(string name, string description, float range, float frequency, int health, int attack, float back, float bulletSpeed, float effectiveRange, bool canTrack) : base(name, description, range, frequency, health, attack, back, bulletSpeed, effectiveRange, canTrack)
    {
    }
    public MagicTowerData()
    {
    }
}
