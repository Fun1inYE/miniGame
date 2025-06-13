using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家buff等级类
/// </summary>
public class PlayerBuffLevelData
{
    /// <summary>
    /// 血量等级
    /// </summary>
    private int hpLevel;
    /// <summary>
    /// 攻击等级
    /// </summary>
    private int attackLevel;
    /// <summary>
    /// 速度等级
    /// </summary>
    private int speedLevel;
    /// <summary>
    /// 攻击速度等级
    /// </summary>
    private int attackSpeedLevel;

    /// <summary>
    /// 最高等级上上限
    /// </summary>
    public readonly static int MAX_LEVEL = 5;

    /// <summary>
    /// 每次升级提升的金币数
    /// </summary>
    public readonly static int SPANDING_BOOST = 500;

    /// <summary>
    /// 初始升级所需的金币数
    /// </summary>
    public readonly static int INITIAL_PRICE = 500;

    /// <summary>
    /// 重构函数
    /// </summary>
    /// <param name="hpLevel"></param>
    /// <param name="attackLevel"></param>
    /// <param name="speedLevel"></param>
    public PlayerBuffLevelData(int hpLevel, int attackLevel, int speedLevel, int attackSpeedLevel)
    {
        this.hpLevel = hpLevel;
        this.attackLevel = attackLevel;
        this.speedLevel = speedLevel;
        this.attackSpeedLevel = attackSpeedLevel;
    }

    public int HpLevel { get => hpLevel; set => hpLevel = value; }
    public int AttackLevel { get => attackLevel; set => attackLevel = value; }
    public int SpeedLevel { get => speedLevel; set => speedLevel = value; }
    public int AttackSpeedLevel { get => attackSpeedLevel; set => attackSpeedLevel = value; }
}
