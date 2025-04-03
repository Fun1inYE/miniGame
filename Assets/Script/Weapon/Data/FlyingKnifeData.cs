using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 飞刀数据类
/// </summary>
public class FlyingKnifeData : RemoteWeaponData
{
    public FlyingKnifeData(string name, string description, int attack, float back, AttackType attackType, WeaponType weaponType, float range, float effectiveRange, float attackFrequency, float speed, bool canTrack) : base(name, description, attack, back, attackType, weaponType, range, effectiveRange, attackFrequency, speed, canTrack)
    {
    }
}
