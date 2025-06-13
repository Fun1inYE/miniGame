using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 主菜单的子菜单枚举
/// </summary>
public enum MainPanelType { MainMenu, PropertyPanel, OptionPanel }

/// <summary>
/// 属于MainMenu的子窗口要继承这个方法
/// </summary>
public interface MainMenuPanelUIType
{
    public void CloseUI();
}

/// <summary>
/// 主菜单类
/// </summary>
public class MainPanel : BasePanel
{
    public MainPanel() : base("MainPanel") { }

    /// <summary>
    /// 引用主菜单栏的UI
    /// </summary>
    private MenuPanelUI menuPanelUI;

    /// <summary>
    /// 主菜单中的数据
    /// </summary>
    private MainMenuPanelData mainMenuPanelData;
    private PropertyPanelData propertyPanelData;

    /// <summary>
    /// 引用主菜单的的UI
    /// </summary>
    private MainMenuPanelUI mainMenuPanelUI;
    /// <summary>
    /// 引用属性面板的UI
    /// </summary>
    private PropertyPanelUI propertyPanelUI;
    /// <summary>
    /// 设置面板的UI
    /// </summary>
    private OptionPanelUI optionPanelUI;

    private List<GameObject> panelList = new List<GameObject>();

    // 主菜单下的三个panel
    private GameObject mainMenuPanel;
    private GameObject propertyPanel;
    private GameObject optionPanel;

    public override void OnEnter()
    {
        base.OnEnter();
        //给菜单栏添加对应的脚本
        menuPanelUI = activePanel.AddComponent<MenuPanelUI>();

        InitChildPanelData();
        InitChildPanel();

        //初始化并且注册MenuPanelUI中的按钮功能
        menuPanelUI.OnSwitchPanel += DisplayPanel;
    }

    /// <summary>
    /// 初始化子窗口面板数据
    /// </summary>
    private void InitChildPanelData()
    {
        mainMenuPanelData = new MainMenuPanelData();
        MessageManager.Instance.AddFunctionInAction<int>(MessageDefine.ECO_COIN_CHANGE, mainMenuPanelData.NotifyCoin);
        propertyPanelData = new PropertyPanelData();
        MessageManager.Instance.AddFunctionInAction<int>(MessageDefine.ECO_COIN_CHANGE, propertyPanelData.NotifyCoin);

    }

    /// <summary>
    /// 初始化子窗口
    /// </summary>
    private void InitChildPanel()
    {
        //添加对应的GameObject和对应的UI逻辑脚本
        mainMenuPanel = FindAndMoveObject.FindChildRecursive(activePanel.transform, "MainMenuPanel").gameObject;
        mainMenuPanelUI = ComponentFinder.GetOrAddComponent<MainMenuPanelUI>(mainMenuPanel);
        mainMenuPanelUI.SetData(mainMenuPanelData);

        propertyPanel = FindAndMoveObject.FindChildRecursive(activePanel.transform, "PropertyPanel").gameObject;
        propertyPanelUI = ComponentFinder.GetOrAddComponent<PropertyPanelUI>(propertyPanel);
        propertyPanelUI.SetData(propertyPanelData);

        optionPanel = FindAndMoveObject.FindChildRecursive(activePanel.transform, "OptionPanel").gameObject;
        optionPanelUI = ComponentFinder.GetOrAddComponent<OptionPanelUI>(optionPanel);

        panelList.Add(mainMenuPanel);
        panelList.Add(propertyPanel);
        panelList.Add(optionPanel);
        //默认先启动MainMenuPanel
        DisplayPanel(MainPanelType.MainMenu);
    }

    /// <summary>
    /// 调控MainMenuPanel下的子窗口的方法
    /// </summary>
    /// <param name="mainPanelType"></param>
    private void DisplayPanel(MainPanelType mainPanelType)
    {
        //在启动某一个panel的时候先关闭所有的panel
        foreach (var panel in panelList)
        {
            panel.SetActive(false);
        }

        switch (mainPanelType)
        {
            case MainPanelType.MainMenu:
                mainMenuPanel.SetActive(true);
                break;
            case MainPanelType.PropertyPanel:
                //刷新属性窗口
                propertyPanelUI.UpdateSkillImage();
                propertyPanel.SetActive(true);
                break;
            case MainPanelType.OptionPanel:
                optionPanel.SetActive(true);
                break;
            default:
                Debug.LogError("传入了一个未知的子窗口信息");
                break;
        }

        //切换完窗口更新经济数值的UI
        EconomyManager.Instance.UpdateEcoValue();
    }

    /// <summary>
    /// 重写退出方法
    /// </summary>
    public override void OnExit()
    {
        MessageManager.Instance.RemoveFunctionInAction<int>(MessageDefine.ECO_COIN_CHANGE, mainMenuPanelData.NotifyCoin);
        MessageManager.Instance.RemoveFunctionInAction<int>(MessageDefine.ECO_COIN_CHANGE, propertyPanelData.NotifyCoin);
        mainMenuPanelUI.CloseUI();
        propertyPanelUI.CloseUI();
        optionPanelUI.CloseUI();
        base.OnExit();
    }
}
