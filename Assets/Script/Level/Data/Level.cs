using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 关卡的数据类
/// </summary>
[System.Serializable]
public class LevelData
{
    /// <summary>
    /// 关卡标号
    /// </summary>
    private int levelIndex;

    public List<WaveData> waves;

    /// <summary>
    /// 给关卡赋值的Index
    /// </summary>
    /// <param name="index"></param>
    public void SetLevelIndex(int index)
    {
        levelIndex = index;
    }
}
