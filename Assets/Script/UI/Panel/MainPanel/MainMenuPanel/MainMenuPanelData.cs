using System;
using UnityEngine;

/// <summary>
/// MainMenuPanel中的数据类
/// </summary>
public class MainMenuPanelData
{
    /// <summary>
    /// 金币的数量
    /// </summary>
    private int coin;

    public int Coin { get => coin; set => coin = value; }

    /// <summary>
    /// 数据更改的委托
    /// </summary>
    public event Action OnDataChange;

    /// <summary>
    /// 无参构造函数
    /// </summary>
    public MainMenuPanelData()
    {
        coin = 0;
    }

    public void NotifyCoin(int value)
    {
        coin = value;
        OnDataChange?.Invoke();
    }
}
