using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 关卡编辑器
/// </summary>
public class LevelEditor : MonoBehaviour
{
    public List<LevelData> levels;

    /// <summary>
    /// 判断LevelEditor是否初始化完成
    /// </summary>
    private bool isInitLevel = false;

    public bool IsInitLevel { get => isInitLevel; set => isInitLevel = value; }

    private void Awake()
    {
        if(InitLevelEditor())
        {
            Debug.Log("关卡编辑器数据初始化成功");
            isInitLevel = true;
        }
        else
        {
            Debug.LogError("关卡编辑器数据初始化失败");
        }
    }

    /// <summary>
    /// 观察关卡编辑器是否正确使用
    /// </summary>
    /// <returns></returns>
    private bool InitLevelEditor()
    {
        if(levels == null)
        {
            Debug.LogError($"{gameObject.name}中的levels为null");
            return false;
        }
        if(levels.Count == 0)
        {
            Debug.LogError($"{gameObject.name}中的levels的数量为0");
            return false;
        }
        for(int i = 0; i < levels.Count; i++)
        {
            //给关卡赋值
            levels[i].SetLevelIndex(i + 1);
            if(levels[i].waves == null)
            {
                Debug.LogError($"{gameObject.name}中的levels[{i}]中的波次为null");
                return false;
            }
            if(levels[i].waves.Count == 0)
            {
                Debug.LogError($"{gameObject.name}中的levels[{i}]中的波次的数量为0");
                return false;
            }
            for(int j = 0; j < levels[i].waves.Count; j++)
            {
                foreach(var enemy in levels[i].waves[j].enemies)
                {
                    if(enemy.enmeyType == EnemyType.Null)
                    {
                        Debug.LogError($"{gameObject.name}中的levels[{i}]中的wave[{j}]中有未被正确初始化的enmeyType");
                        return false;
                    }
                }
            }
        }

        return true;
    }
}




