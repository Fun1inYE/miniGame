using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

/// <summary>
/// Panel的基类
/// </summary>
[Serializable]
public class BasePanel
{
    /// <summary>
    /// 每个Panel的名字
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 当前活跃的面板
    /// </summary>
    public GameObject activePanel { get; set; }

    /// <summary>
    /// panel的管理器
    /// </summary>
    public PanelManager panelManager { get; set; }

    /// <summary>
    /// BasePanel的构造函数
    /// </summary>
    /// <param name="Name"></param>
    public BasePanel(string Name)
    {
        this.Name = Name;
    }

    /// <summary>
    /// 进入UI要执行的方法
    /// </summary>
    public virtual void OnEnter() { }

    /// <summary>
    /// 正在UI界面中要执行的方法
    /// </summary>
    public virtual void OnUpdate() 
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            panelManager.Pop();
        }
    }

    /// <summary>
    /// UI暂停的时候要执行的代码
    /// </summary>
    public virtual void OnPasue()
    {
        //暂停Panel的射线检测
        ComponentFinder.GetOrAddComponent<CanvasGroup>(activePanel).blocksRaycasts = false;
    }

    /// <summary>
    /// UI继续的时候要执行的代码
    /// </summary>
    public virtual void OnResume()
    {
        //开始Panel的射线检测
        ComponentFinder.GetOrAddComponent<CanvasGroup>(activePanel).blocksRaycasts = true;
    }

    /// <summary>
    /// UI退出的时候要执行的代码
    /// </summary>
    public virtual void OnExit()
    {
        UIManager.Instance.HideUI(activePanel);
    }
}