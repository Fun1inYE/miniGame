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

    private void Start()
    {
        MessageManager.Instance.AddFunctionInAction(MessageDefine.BACK_TO_MAINMENU, RemoveAllTower);
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

    /// <summary>
    /// 消除所有塔的方法
    /// </summary>
    public void RemoveAllTower()
    {
        foreach (GameObject tower in towerList)
        {
            Destroy(tower);
        }
        //清除列表
        towerList.Clear();
    }

    private void OnDisable()
    {
        MessageManager.Instance.RemoveFunctionInAction(MessageDefine.BACK_TO_MAINMENU, RemoveAllTower);
    }
}
