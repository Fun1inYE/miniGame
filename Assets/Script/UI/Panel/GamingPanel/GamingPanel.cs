using UnityEngine;
using UnityEngine.UI;

public class GamingPanel : BasePanel
{
    /// <summary>
    /// 继承父类的构造函数
    /// </summary>
    /// <param name="Name"></param>
    public GamingPanel() : base("GamingPanel") {}

    /// <summary>
    /// GamingPanel的数据
    /// </summary>
    private GamingPanelData gamingPanelData;

    /// <summary>
    /// GamingPanel的数据UI
    /// </summary>
    private GamingPanelUI gamingPanelUI;

    public override void OnEnter()
    {
        base.OnEnter();
        //先初始化对应的UI组件(给当前活跃UI添加对应组件)
        gamingPanelUI = activePanel.AddComponent<GamingPanelUI>();
        //初始化对应的面板UI的数据
        gamingPanelData = new GamingPanelData();
        //设定data
        gamingPanelUI.SetData(gamingPanelData);
    }

    public override void OnPasue()
    {
        base.OnPasue();
        //这里实现游戏暂停相关逻辑
        gamingPanelUI.BuildMenuButtonPanel.SetActive(false);
    }

    public override void OnResume()
    {
        base.OnResume();
        gamingPanelUI.BuildMenuButtonPanel.SetActive(true);
    }

    /// <summary>
    /// 更改Score的方法
    /// </summary>
    /// <param name="value"></param>
    public void ChangeScoreValue(int value)
    {
        gamingPanelData.NotifyScore(value);
    }

    /// <summary>
    /// 更改Score的方法
    /// </summary>
    /// <param name="value"></param>
    public void ChangeCoinValue(int value)
    {
        gamingPanelData.NotifyCoin(value);
    }
}
