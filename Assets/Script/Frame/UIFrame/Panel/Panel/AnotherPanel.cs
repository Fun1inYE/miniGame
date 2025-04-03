using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnotherPanel : BasePanel
{
    public AnotherPanel() : base("AnotherPanel")
    {
    }

    //重写进入方法
    public override void OnEnter()
    {
        base.OnEnter();
        FindAndMoveObject.FindChildRecursive(activePanel.transform, "ExitButton").GetComponent<Button>().onClick.AddListener(() => 
        {
            panelManager.Pop();
        });
         
    }

    /// <summary>
    /// 重写退出方法
    /// </summary>
    public override void OnExit()
    {
        FindAndMoveObject.FindChildRecursive(activePanel.transform, "ExitButton").GetComponent<Button>().onClick.RemoveAllListeners();
        base.OnExit();
    }

}
