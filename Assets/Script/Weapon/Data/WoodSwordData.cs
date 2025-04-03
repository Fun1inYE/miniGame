using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 木剑数值
/// </summary>
public class WoodSwordData : MeleeWeaponData
{
    public WoodSwordData(string name, string description, int attack, float back, AttackType attackType, WeaponType weaponType, float radius, float speed, int maxCount) : base(name, description, attack, back, attackType, weaponType, radius, speed, maxCount)
    {

    }
}
