using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// BuildMenuPanel窗口的逻辑
/// </summary>
public class BuildMenuPanel : BasePanel
{
    public BuildMenuPanel() : base("BuildMenuPanel") {}

    /// <summary>
    /// 窗口的数据
    /// </summary>
    private BuildMenuPanelData buildMenuPanelData;

    /// <summary>
    /// 窗口的UI
    /// </summary>
    private BuildMenuPanelUI buildMenuPanelUI;

    public override void OnEnter()
    {
        base.OnEnter();
        //先初始化对应的UI组件(给当前活跃UI添加对应组件)
        buildMenuPanelUI = activePanel.AddComponent<BuildMenuPanelUI>();
        //初始化对应的面板UI的数据
        buildMenuPanelData = new BuildMenuPanelData();
        //设定data
        buildMenuPanelUI.SetData(buildMenuPanelData);
    }

    public void ChangeCoinValue(int value)
    {
        buildMenuPanelData.NotifySapphire(value);
    }
}
