using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 波次的数据类
/// </summary>
[System.Serializable]
public class WaveData
{   
    /// <summary>
    /// 敌人的生成频率
    /// </summary>
    public float generateFreqency;

    /// <summary>
    /// 每一波敌人最大数量
    /// </summary>
    public int maxEnemyCount;

    /// <summary>
    /// 这个波次的时间
    /// </summary>
    public int waveTime;

    /// <summary>
    /// 下一波之间的冷却时间
    /// </summary>
    public int nextWaveCold;

    /// <summary>
    /// 此波次的宝箱信息
    /// </summary>
    public ChestData chestData;

    /// <summary>
    /// 波次中的敌人结构
    /// </summary>
    public List<EnemyStruct> enemies;
}
