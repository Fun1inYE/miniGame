using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 经济管理器
/// </summary>
public class EconomyManager : Singleton<EconomyManager>
{
    /// <summary>
    /// 当前蓝宝石数量
    /// </summary>
    public int sapphire;

    /// <summary>
    /// 当前金币数量
    /// </summary>
    public int coin;

    protected override void Awake()
    {
        base.Awake();
        sapphire = 0;
        coin = 0;
    }

    private void Start()
    {
        //初始化读档
        LoadCoinValue();
        //注册开始游戏方法
        MessageManager.Instance.AddFunctionInAction(MessageDefine.GAME_START, GameStart);
    }

    //游戏开始时，要清空蓝宝石
    public void GameStart()
    {
        sapphire = 0;
        UpdateEcoValue();
    }

    /// <summary>
    /// 消费蓝宝石的方法的方法
    /// </summary>
    public void ChangeSapphire(int value)
    {
        sapphire += value;
        NotifySapphireValueChange();
    }

    public void ChangeCoin(int value)
    {
        coin += value;
        NotifyCoinValueChange();
    }

    /// <summary>
    /// 向外部更新数值的方法
    /// </summary>
    public void UpdateEcoValue()
    {
        NotifySapphireValueChange();
        NotifyCoinValueChange();
        Debug.Log($"更新了经济系统数值 sapphire: {sapphire}, coin: {coin}");
    }

    /// <summary>
    /// 提醒蓝宝石数据更新了
    /// </summary>
    private void NotifySapphireValueChange()
    {
        MessageManager.Instance.Send<int>(MessageDefine.ECO_SAPPHIRE_CHANGE, sapphire);
    }

    /// <summary>
    /// 提醒金币数据更新了
    /// </summary>
    private void NotifyCoinValueChange()
    {
        MessageManager.Instance.Send<int>(MessageDefine.ECO_COIN_CHANGE, coin);
    }

    private void OnDisable()
    {
        MessageManager.Instance.RemoveFunctionInAction(MessageDefine.GAME_START, GameStart);
        
    }

    /// <summary>
    /// 读取金币数量
    /// </summary>
    private void LoadCoinValue()
    {
        coin = SaveWithPlayerPref.LoadByPlayerPrefs<int>(SaveNameDefine.Coin_Count);
        UpdateEcoValue();
    }

    /// <summary>
    /// 保存金币数量
    /// </summary>
    public void SaveCoinValue()
    {
        SaveWithPlayerPref.SaveByPlayerPrefs(SaveNameDefine.Coin_Count, coin);
    }
}
