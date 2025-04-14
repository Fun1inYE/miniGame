using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 塔的管理器
/// </summary>
public class TowerManager : Singleton<TowerManager>
{
    /// <summary>
    /// 塔的列表
    /// </summary>
    private List<GameObject> towerList = new List<GameObject>();

    protected override void Awake()
    {
        base.Awake();
    }

    /// <summary>
    /// 添加一个塔的方法
    /// </summary>
    /// <param name="tower"></param>
    public void RegisterTower(GameObject tower)
    {
        towerList.Add(tower);
    }

    /// <summary>
    /// 消除一个塔的方法
    /// </summary>
    /// <param name="tower"></param>
    public void UnregisterTower(GameObject tower)
    {
        towerList.Remove(tower);
    }
}
