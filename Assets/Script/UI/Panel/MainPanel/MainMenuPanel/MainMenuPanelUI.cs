using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// MainMenu窗口中的UI逻辑（MainPanel的子窗口）
/// </summary>
public class MainMenuPanelUI : MonoBehaviour
{
    /// <summary>
    /// 开始游戏的按钮
    /// </summary>
    private Button startGameButton;

    /// <summary>
    /// 蓝宝石的数量
    /// </summary>
    private Text coinValue;

    private MainMenuPanelData data;

    private void Awake()
    {
        InitUI();
    }

    /// <summary>
    /// 初始化UI
    /// </summary>
    private void InitUI()
    {
        startGameButton = ComponentFinder.GetChildComponent<Button>(gameObject, "StartButton");
        startGameButton.onClick.AddListener(() =>
        {
            //压入LevelPanel
            UIManager.Instance.GetPanelManager().Push(new LevelPanel());
        });

        coinValue = ComponentFinder.GetChildComponent<Text>(gameObject, "CoinValue");
    }

    /// <summary>
    /// 这里要通过控制器来对进行赋值
    /// </summary>
    /// <param name="data"></param>
    public void SetData(MainMenuPanelData data)
    {
        //对Data进行赋值
        this.data = data;
        //初始化或更新PlayerUI
        UpdateUI();
        //对玩家操作进行注册
        this.data.OnDataChange += UpdateUI;
    }

    /// <summary>
    /// 更新UI的方法
    /// </summary>
    private void UpdateUI()
    {
        coinValue.text = data.Coin.ToString();
    }

    /// <summary>
    /// 关闭UI的方法
    /// </summary>
    public void CloseUI()
    {
        this.data.OnDataChange -= UpdateUI;
    }
}
