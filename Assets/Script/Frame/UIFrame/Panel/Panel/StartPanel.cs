using UnityEngine.UI;

/// <summary>
/// 主菜单的Panel
/// </summary>
public class StartPanel : BasePanel
{
    /// <summary>
    /// 重写构造函数
    /// </summary>
    public StartPanel() : base("StartPanel") { }
    /// <summary>
    /// 重写进入方法
    /// </summary>
    public override void OnEnter()
    {
        //执行窗口进入的父类方法
        base.OnEnter();
        //绑定一个按钮监听器，按下就能打开Another窗口
        FindAndMoveObject.FindChildRecursive(activePanel.transform, "Button").GetComponent<Button>().onClick.AddListener(() => 
        {
            panelManager.Push(new AnotherPanel());
        });
         
    }
}
