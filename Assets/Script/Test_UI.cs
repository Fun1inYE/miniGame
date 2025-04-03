using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WeChatWASM;

/// <summary>
/// 测试代码
/// </summary>
public class Test_UI : MonoBehaviour
{
    public Canvas mainCanvas;

    public void Start()
    {
        UIManager.Instance.InitUI();
        UIManager.Instance.SetCanvas(mainCanvas);
        UIManager.Instance.GetPanelManager().PushFirstPanel(new MainPanel());
    }
    public void Update()
    {
        // if(Input.GetKeyDown(KeyCode.Space))
        // {
        //     Debug.Log("按下按钮了");
        //     if(UIManager.Instance.GetPanelManager().panel is GamingPanel gamingPanel)
        //     {
        //         Debug.Log("获取到GamingPanel了");
        //         gamingPanel.ChangeScoreValue(100);
        //     }
        // }
    }
}
