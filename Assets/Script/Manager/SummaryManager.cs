using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummaryManager : Singleton<SummaryManager>
{
    /// <summary>
    /// 引用的总结数据
    /// </summary>
    private SummaryData summaryData;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        MessageManager.Instance.AddFunctionInAction(MessageDefine.GAME_START, buildData);
        MessageManager.Instance.AddFunctionInAction<int>(MessageDefine.ADD_GET_COIN_COUNT, AddGetCoinCount);
        MessageManager.Instance.AddFunctionInAction<int>(MessageDefine.ADD_GET_SAPPHIRE_COUNT, AddGetSapphireCount);
        MessageManager.Instance.AddFunctionInAction<int>(MessageDefine.ADD_TOTAL_ATTACK, AddTotalAttack);
        MessageManager.Instance.AddFunctionInAction(MessageDefine.ADD_KILL_ENEMY_COUNT, AddKillEnemyCount);
    }

    /// <summary>
    /// 新建数据
    /// </summary>
    private void buildData()
    {
        if (summaryData != null)
        {
            Debug.Log("summaryData数据不为空, 正在清空");
            summaryData = null;
        }
        summaryData = new SummaryData(0, 0, 0, 0);
    }

    /// <summary>
    /// 增加敌人数量
    /// </summary>
    public void AddKillEnemyCount()
    {
        summaryData.KillEnemyCount++;
    }

    /// <summary>
    /// 增加蓝宝石获取数量
    /// </summary>
    public void AddGetSapphireCount(int value)
    {
        summaryData.GetSapphireCount += value;
    }

    /// <summary>
    /// 增加金币数量
    /// </summary>
    /// <param name="value"></param>
    public void AddGetCoinCount(int value)
    {
        summaryData.GetCoinCount += value;
    }

    /// <summary>
    /// 增加总伤害
    /// </summary>
    /// <param name="value"></param>
    public void AddTotalAttack(int value)
    {
        summaryData.TotalAttack += value;
    }

    /// <summary>
    /// 获取总结数据
    /// </summary>
    /// <returns></returns>
    public SummaryData GetSummaryData()
    {
        return summaryData;
    }

    private void OnDestroy()
    {
        MessageManager.Instance.RemoveFunctionInAction(MessageDefine.GAME_START, buildData);
        MessageManager.Instance.RemoveFunctionInAction(MessageDefine.GAME_START, buildData);
        MessageManager.Instance.RemoveFunctionInAction<int>(MessageDefine.ADD_GET_COIN_COUNT, AddGetCoinCount);
        MessageManager.Instance.RemoveFunctionInAction<int>(MessageDefine.ADD_GET_SAPPHIRE_COUNT, AddGetSapphireCount);
        MessageManager.Instance.RemoveFunctionInAction<int>(MessageDefine.ADD_TOTAL_ATTACK, AddTotalAttack);
        MessageManager.Instance.RemoveFunctionInAction(MessageDefine.ADD_KILL_ENEMY_COUNT, AddKillEnemyCount);
    }
}
