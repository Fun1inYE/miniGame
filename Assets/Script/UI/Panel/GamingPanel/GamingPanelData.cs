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
    /// 玩家获得的金币
    /// </summary>
    private int sapphire;

    /// <summary>
    /// 委托事件，提醒系统该更新数据或者UI了
    /// </summary>
    public event Action OnDataChange;

    /// <summary>
    /// 属性接口
    /// </summary>
    public int Sapphire { get => sapphire; set => sapphire = value; }

    /// <summary>
    /// 无参构造函数
    /// </summary>
    public GamingPanelData() 
    {
        sapphire = 0;
    }

    /// <summary>
    /// 更改coin且执行更改UI的方法
    /// </summary>
    /// <param name="value"></param>
    public void NotifySapphire(int value)
    {
        sapphire = value;
        OnDataChange?.Invoke();
    }
}
