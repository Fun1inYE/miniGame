using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// GamingPanelUI类
/// </summary>
public class GamingPanelUI : MonoBehaviour
{
    /// <summary>
    /// 分数值
    /// </summary>
    private Text scoreValue;
    /// <summary>
    /// 金币值
    /// </summary>
    private Text coinValue;

    /// <summary>
    /// 暂停按钮
    /// </summary>
    private Button pasueButton;

    /// <summary>
    /// UI负责更新的数据
    /// </summary>
    private GamingPanelData data;

    private void Awake()
    {
        InitUI();
    }

    /// <summary>
    /// 初始化UI
    /// </summary>
    private void InitUI()
    {
        scoreValue = ComponentFinder.GetChildComponent<Text>(gameObject, "ScoreValue");
        coinValue = ComponentFinder.GetChildComponent<Text>(gameObject, "CoinValue");

        pasueButton = ComponentFinder.GetChildComponent<Button>(gameObject, "PauseButton");
        //给暂停按钮绑定监听事件
        pasueButton.onClick.AddListener(() => 
        {
            //压入暂停UI
            UIManager.Instance.GetPanelManager().Push(new PausePanel());
        });
    }

    /// <summary>
    /// 这里要通过控制器来对playerData进行赋值
    /// </summary>
    /// <param name="data"></param>
    public void SetData(GamingPanelData data)
    {
        //对playerData进行赋值
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
        scoreValue.text = data.Score.ToString();
        coinValue.text = data.Coin.ToString();
    }

    private void OnDisable()
    {
        //防止内存泄露
        data.OnDataChange -= UpdateUI;
        pasueButton.onClick.RemoveAllListeners();
    }
}
