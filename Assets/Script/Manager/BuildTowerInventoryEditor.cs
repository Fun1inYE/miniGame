using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 塔信息的结构体
/// </summary>
[System.Serializable]
public class TowerStruct
{
    /// <summary>
    /// 塔的类型
    /// </summary>
    public TowerType towerType;

    /// <summary>
    /// 塔的贴图
    /// </summary>
    public Sprite sprite;

    /// <summary>
    /// 塔的价格
    /// </summary>
    public int price;
}

/// <summary>
/// 塔的库存类
/// </summary>
public class BuildTowerInventoryEditor : MonoBehaviour
{
    /// <summary>
    /// 塔的结构体列表
    /// </summary>
    public List<TowerStruct> towerList;

    /// <summary>
    /// 判断库存是否编辑完成
    /// </summary>
    private bool isInitInventory = false;

    public bool IsInitInventory { get => isInitInventory; set => isInitInventory = value; }

    private void Awake()
    {
        if(InitInventoryEditor())
        {
            Debug.Log("关卡编辑器数据初始化成功");
            IsInitInventory = true;
        }
        else
        {
            Debug.LogError("关卡编辑器数据初始化失败");
        }
    }

    /// <summary>
    /// 判断比那机器中是否有错误的信息
    /// </summary>
    /// <returns></returns>
    private bool InitInventoryEditor()
    {
        for(int i = 0; i < towerList.Count; i++)
        {
            //检查列表中是否有塔的类型为Null的
            if(towerList[i].towerType == TowerType.Null)
            {
                Debug.LogError($"towerList中的第{i}位塔的类型是错误的，请检查问题");
                return false;
            }
        }
        return true;
    }
}
