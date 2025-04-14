using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 建造菜单的数据
/// </summary>
public class BuildMenuPanelData
{
    /// <summary>
    /// 蓝宝石的数量
    /// </summary>
    private int sapphire;

    /// <summary>
    /// 委托事件，提醒系统该更新数据或者UI了
    /// </summary>
    public event Action OnDataChange;

    public int Sapphire { get => sapphire; set => sapphire = value; }    

    /// <summary>
    /// 构造函数
    /// </summary>
    public BuildMenuPanelData()
    {
        sapphire = 0;
    }

    public void NotifySapphire(int value)
    {
        sapphire += value;
        OnDataChange?.Invoke();
    }
}
