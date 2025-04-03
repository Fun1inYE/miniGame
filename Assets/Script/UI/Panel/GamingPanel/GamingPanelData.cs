using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// GamingPanel的类
/// </summary>
public class GamingPanelData
{
    /// <summary>
    /// 玩家的得分
    /// </summary>
    private int score;

    /// <summary>
    /// 玩家获得的金币
    /// </summary>
    private int coin;

    /// <summary>
    /// 委托事件，提醒系统该更新数据或者UI了
    /// </summary>
    public event Action OnDataChange;

    /// <summary>
    /// 属性接口
    /// </summary>
    public int Score { get => score; set => score = value; }
    public int Coin { get => coin; set => coin = value; }

    /// <summary>
    /// 无参构造函数
    /// </summary>
    public GamingPanelData() 
    {
        score = 0;
        coin = 0;
    }

    /// <summary>
    /// 更改score且执行更改UI的方法
    /// </summary>
    /// <param name="value"></param>
    public void NotifyScore(int value)
    {
        score += value;
        OnDataChange?.Invoke();
    }

    /// <summary>
    /// 更改coin且执行更改UI的方法
    /// </summary>
    /// <param name="value"></param>
    public void NotifyCoin(int value)
    {
        coin += value;
        OnDataChange?.Invoke();
    }
}
