using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// GamingPanelUI类
/// </summary>
public class GamingPanelUI : MonoBehaviour
{
    /// <summary>
    /// 金币值
    /// </summary>
    private Text sapphireValue;

    /// <summary>
    /// 时间提醒文字
    /// </summary>
    private Image timePromptImage;

    /// <summary>
    /// 时间提示文字
    /// </summary>
    private Text timePromptValue;

    /// <summary>
    /// 暂停按钮
    /// </summary>
    private Button pasueButton;

    /// <summary>
    /// 建造菜单的按钮
    /// </summary>
    private Button buildMenuButton;

    /// <summary>
    /// 建造菜单按钮的面板
    /// </summary>
    private GameObject buildMenuButtonPanel;

    /// <summary>
    /// UI负责更新的数据
    /// </summary>
    private GamingPanelData data;

    /// <summary>
    /// 要得知游戏内的时间
    /// </summary>
    private EnemyGenerateManager enemyGenerateManager;

    public GameObject BuildMenuButtonPanel { get => buildMenuButtonPanel; set => buildMenuButtonPanel = value; }

    private void Awake()
    {
        InitUI();
        enemyGenerateManager = FindAndMoveObject.FindFromFirstLayer("EnemyGenerateManager").GetComponent<EnemyGenerateManager>();
    }

    /// <summary>
    /// 初始化UI
    /// </summary>
    private void InitUI()
    {
        //获取Text和Image
        sapphireValue = ComponentFinder.GetChildComponent<Text>(gameObject, "SapphireValue");
        timePromptImage = ComponentFinder.GetChildComponent<Image>(gameObject, "TimePromptImage");
        timePromptValue = ComponentFinder.GetChildComponent<Text>(gameObject, "TimePromptValue");

        //获取按钮
        pasueButton = ComponentFinder.GetChildComponent<Button>(gameObject, "PauseButton");
        //给暂停按钮绑定监听事件
        pasueButton.onClick.AddListener(() =>
        {
            //压入暂停UI
            UIManager.Instance.GetPanelManager().Push(new PausePanel());
        });

        buildMenuButton = ComponentFinder.GetChildComponent<Button>(gameObject, "BuildTowerButton");
        buildMenuButton.onClick.AddListener(() =>
        {
            //不允许摇杆使用
            FindAndMoveObject.FindFromFirstLayer("Player").GetComponent<PlayerMove>().joystick.GetComponent<CanvasGroup>().blocksRaycasts = false;
            //压入建造UI
            UIManager.Instance.GetPanelManager().Push(new BuildMenuPanel());
        });

        buildMenuButtonPanel = FindAndMoveObject.FindChildRecursive(transform, "BuildButtonPanel").gameObject;
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
        enemyGenerateManager.CurrentTimer.TimeUpdateEvent += SetTimeUI;
        enemyGenerateManager.NextWaveTimer.TimeUpdateEvent += SetTimeUI;
    }

    /// <summary>
    /// 更新UI的方法
    /// </summary>
    private void UpdateUI()
    {
        sapphireValue.text = data.Sapphire.ToString();
    }

    /// <summary>
    /// 绑定时间UI
    /// </summary>
    public void SetTimeUI(string time)
    {
        timePromptValue.text = time;
    }

    private void OnDisable()
    {
        //防止内存泄露
        data.OnDataChange -= UpdateUI;
        pasueButton.onClick.RemoveAllListeners();
        enemyGenerateManager.CurrentTimer.TimeUpdateEvent -= SetTimeUI;
        enemyGenerateManager.NextWaveTimer.TimeUpdateEvent -= SetTimeUI;
    }
}
