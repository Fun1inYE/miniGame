using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 武器的其他逻辑
/// </summary>
public class Weapon : MonoBehaviour
{
    public AttackType attackType;
    public WeaponType weaponType;

    /// <summary>
    /// 武器名字
    /// </summary>
    public string weaponName;

    /// <summary>
    /// 武器的介绍
    /// </summary>
    public string description;
}
