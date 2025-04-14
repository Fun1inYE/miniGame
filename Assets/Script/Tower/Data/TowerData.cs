using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 防御塔的数据类
/// </summary>
public class TowerData
{
    /// <summary>
    /// 防御塔的名字
    /// </summary>
    private string name;

    /// <summary>
    /// 防御塔的介绍
    /// </summary>
    private string description;

    /// <summary>
    /// 防御塔的影响范围
    /// </summary>
    private float range;

    /// <summary>
    /// 影响频率
    /// </summary>
    private float frequency;

    /// <summary>
    /// 塔的血量
    /// </summary>
    private int health;

    public string Name { get => name; set => name = value; }
    public string Description { get => description; set => description = value; }
    public float Range { get => range; set => range = value; }
    public float Frequency { get => frequency; set => frequency = value; }
    public int Health { get => health; set => health = value; }

    public TowerData()
    {
    }

    public TowerData(string name, string description, float range, float frequency, int health)
    {
        this.name = name;
        this.description = description;
        this.range = range;
        this.frequency = frequency;
        this.health = health;
    }
}
