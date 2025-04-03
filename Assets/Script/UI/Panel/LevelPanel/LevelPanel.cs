using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 关卡选择页面
/// </summary>
public class LevelPanel : BasePanel
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public LevelPanel() : base("LevelPanel") {}

    /// <summary>
    /// 引用LevelPanel的UI类
    /// </summary>
    private LevelPanelUI levelPanelUI;

    public override void OnEnter()
    {
        levelPanelUI = ComponentFinder.GetOrAddComponent<LevelPanelUI>(activePanel);
    }
}
