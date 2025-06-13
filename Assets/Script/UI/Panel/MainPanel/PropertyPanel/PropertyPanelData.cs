using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 属性面板数据
/// </summary>
public class PropertyPanelData
{
    /// <summary>
    /// 金币数量
    /// </summary>
    private int coin;

    public int Coin { get => coin; set => coin = value; }

    /// <summary>
    /// 委托事件，提醒系统该更新数据或者UI了
    /// </summary>
    public event Action OnDataChange;

    /// <summary>
    /// 构造函数
    /// </summary>
    public PropertyPanelData()
    {
        coin = 0;
    }

    public void NotifyCoin(int value)
    {
        coin = value;
        OnDataChange?.Invoke();
    }

}   

