using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 暂停UI的相关逻辑
/// </summary>
public class PausePanel : BasePanel
{
    public PausePanel() : base("PausePanel") {}

    /// <summary>
    /// 引用PausePanel中的UI
    /// </summary>
    private PausePanelUI pausePanelUI;
    
    public override void OnEnter() 
    {
        base.OnEnter();
        pausePanelUI = activePanel.AddComponent<PausePanelUI>();
    }

    public override void OnExit()
    {
        base.OnExit();
    }
}   
