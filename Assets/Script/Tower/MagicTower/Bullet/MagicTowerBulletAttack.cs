using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 魔法塔的子弹攻击逻辑
/// </summary>
public class MagicTowerBulletAttack : WeaponAttack
{
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        if(other.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
