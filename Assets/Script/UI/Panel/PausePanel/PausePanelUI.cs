using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 暂停Panel的页面
/// </summary>
public class PausePanelUI : MonoBehaviour
{
    /// <summary>
    /// 暂停按钮
    /// </summary>
    private Button continueButton;

    /// <summary>
    /// 退出游戏按钮
    /// </summary>
    private Button exitButton;

    private void Awake()
    {
        InitUI();
    }

    private void InitUI()
    {
        continueButton = ComponentFinder.GetChildComponent<Button>(gameObject, "ContinueButton");
        continueButton.onClick.AddListener(() => {
            //弹出当前暂停页面
            UIManager.Instance.GetPanelManager().Pop();
        });

        exitButton = ComponentFinder.GetChildComponent<Button>(gameObject, "ExitButton");
        exitButton.onClick.AddListener(() => {
            //TODO:保存玩家数据或直接结算
            //TODO:退出游戏逻辑
        });
    }

    private void OnDisable()
    {
        continueButton.onClick.RemoveAllListeners();
        exitButton.onClick.RemoveAllListeners();
    }
}
