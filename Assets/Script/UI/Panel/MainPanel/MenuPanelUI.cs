using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// 主菜单的菜单栏中的UI类
/// </summary>
public class MenuPanelUI : MonoBehaviour
{
    /// <summary>
    /// 准备界面按钮
    /// </summary>
    private Button readyPanelButton;
    /// <summary>
    /// 属性界面按钮
    /// </summary>
    private Button propertyPanelButton;
    /// <summary>
    /// 设置界面按钮
    /// </summary>
    private Button optionPanelButton;

    public event Action<MainPanelType> OnSwitchPanel;

    private void Awake()
    {
        InitUI();
    }

    private void InitUI()
    {
        readyPanelButton = ComponentFinder.GetChildComponent<Button>(gameObject, "ReadyPanelButton");
        propertyPanelButton = ComponentFinder.GetChildComponent<Button>(gameObject, "PropertyPanelButton");
        optionPanelButton = ComponentFinder.GetChildComponent<Button>(gameObject, "OptionPanelButton");

        SetPanelButtonFunction();
    }

    /// <summary>
    /// 绑定按钮方法
    /// </summary>
    /// <param name="action"></param>
    public void SetPanelButtonFunction()
    {
        readyPanelButton.onClick.AddListener(() =>
        {
            OnSwitchPanel?.Invoke(MainPanelType.MainMenu);
        });
        propertyPanelButton.onClick.AddListener(() =>
        {
            OnSwitchPanel?.Invoke(MainPanelType.PropertyPanel);
        });
        optionPanelButton.onClick.AddListener(() =>
        {
            OnSwitchPanel?.Invoke(MainPanelType.OptionPanel);
        });
    }
}
