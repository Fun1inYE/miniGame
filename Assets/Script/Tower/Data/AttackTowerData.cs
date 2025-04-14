using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 攻击塔的数据类
/// </summary>
public class AttackTowerData : TowerData
{
    /// <summary>
    /// 攻击塔的攻击力
    /// </summary>
    private int attack;

    private float back;

    /// <summary>
    /// 子弹速度
    /// </summary>
    private float bulletSpeed;

    /// <summary>
    /// 子弹的有效范围
    /// </summary>
    private float effectiveRange;

    /// <summary>   
    /// 子弹是否可以追踪
    /// </summary>
    private bool canTrack;

    public int Attack { get => attack; set => attack = value; }
    public float BulletSpeed { get => bulletSpeed; set => bulletSpeed = value; }
    public bool CanTrack { get => canTrack; set => canTrack = value; }
    public float EffectiveRange { get => effectiveRange; set => effectiveRange = value; }
    public float Back { get => back; set => back = value; }

    public AttackTowerData()
    {
    }

    public AttackTowerData(string name, string description, float range, float frequency, int health, int attack, float back, float bulletSpeed, float effectiveRange, bool canTrack) : base(name, description, range, frequency, health)
    {
        this.attack = attack;
        this.back = back;
        this.bulletSpeed = bulletSpeed;
        this.effectiveRange = effectiveRange;
        this.canTrack = canTrack;
    }   
}
