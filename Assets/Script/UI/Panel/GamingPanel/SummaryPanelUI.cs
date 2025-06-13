using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 总结面板UI
/// </summary>
public class SummaryPanelUI : MonoBehaviour
{
    public GameObject summaryPanel;
    public List<GameObject> summaryContentList;

    /// <summary>
    /// 剩余时间值
    /// </summary>
    public Text remainTimeValue;

    /// <summary>
    /// 自动返回主菜单按钮
    /// </summary>
    private Timer gotoMainMenuTimer;

    /// <summary>
    /// 任务完成的面板
    /// </summary>
    public RectTransform taskFinishPanel;

    public Text getCoinCountValue;
    public Text getSapphireCountValue;
    public Text killEnemyCountValue;
    public Text totalAttackValue;

    /// <summary>
    /// 退出按钮
    /// </summary>
    public Button exitButton;

    /// <summary>
    /// 待总结的数据
    /// </summary>
    private SummaryData summaryData;

    private void OnEnable()
    {
        if (summaryPanel == null)
        {
            Debug.LogWarning("总结信息框是空的");
        }

        if (summaryContentList == null || summaryContentList.Count == 0)
        {
            Debug.LogWarning("总结信息小框是空的");
        }

        //先将所有的GameObject进行Active(false)
        foreach (GameObject summaryContent in summaryContentList)
        {
            summaryContent.SetActive(false);
        }

        //绑定方法
        exitButton.onClick.AddListener(() =>
        {
            GotoMainMenu();
        });

        //增加计时器
        gotoMainMenuTimer = gameObject.AddComponent<Timer>();
        gotoMainMenuTimer.OnTimerFinished += GotoMainMenu;
        gotoMainMenuTimer.TimeUpdateEvent += UpdateTimeValue;
        
        //注册事件
        MessageManager.Instance.AddFunctionInAction(MessageDefine.GAME_OVER, GameOver);
    }

    private void GotoMainMenu()
    {
        //将玩家传送回坐标为0，0的位置
        GameObject player = FindAndMoveObject.FindFromFirstLayer("Player");
        player.transform.position = Vector2.zero;
        //摄像机传送回坐标为0，0的位置
        Camera.main.transform.position = Vector2.zero;
        //存储经济
        EconomyManager.Instance.SaveCoinValue();

        //TODO: 将武器数据清除

        //清除地图上的实体
        MessageManager.Instance.Send(MessageDefine.BACK_TO_MAINMENU);

        //UI转回主菜单的位置
        UIManager.Instance.GetPanelManager().AllPop();
        //回到主菜单
        UIManager.Instance.GetPanelManager().Push(new MainPanel());
    }

    private void UpdateTimeValue(string value)
    {
        remainTimeValue.text = value;
    }

    /// <summary>
    /// 游戏结束的方法
    /// </summary>
    public void GameOver()
    {
        //任务完成的UI,然后回调出总结面板
        DisplayWithDoTween.MoveAndFadeWithUI(taskFinishPanel, 0.5f, 1f, 0.5f, 60f, GetSummaryData);
    }

    /// <summary>
    /// 获取总结数据
    /// </summary>
    /// <param name="summaryData"></param>
    public void GetSummaryData()
    {
        summaryData = SummaryManager.Instance.GetSummaryData();
        UpdateSummaryContentUI();
    }

    /// <summary>
    /// 更新总结UI
    /// </summary>
    private void UpdateSummaryContentUI()
    {
        if (summaryData == null)
        {
            Debug.LogError("summaryData是空的");
            return;
        }

        //更新UI
        getCoinCountValue.text = summaryData.GetCoinCount.ToString();
        getSapphireCountValue.text = summaryData.GetSapphireCount.ToString();
        killEnemyCountValue.text = summaryData.KillEnemyCount.ToString();
        totalAttackValue.text = summaryData.TotalAttack.ToString();

        StartListOneByOne();
    }

    /// <summary>
    /// 开始列表出现协程
    /// </summary>
    private void StartListOneByOne()
    {
        StartCoroutine(ListOneByOne());
    }

    /// <summary>
    /// 列出总结动画
    /// </summary>
    /// <returns></returns>
    private IEnumerator ListOneByOne()
    {
        //打开总结面板
        summaryPanel.SetActive(true);
        //启动倒计时
        gotoMainMenuTimer.StartCountdownTimer(10f, 1f);
        //等待1s
        yield return new WaitForSeconds(1f);

        //需要播放动画
        bool needPlay = true;
        while (needPlay)
        {
            for (int i = 0; i < summaryContentList.Count; i++)
            {
                summaryContentList[i].SetActive(true);
                //等待1s
                yield return new WaitForSeconds(1f);
            }
            needPlay = false;
        }
    }

    private void OnDestroy()
    {
        exitButton.onClick.RemoveAllListeners();
        MessageManager.Instance.RemoveFunctionInAction(MessageDefine.GAME_OVER, GameOver);
        gotoMainMenuTimer.OnTimerFinished -= GotoMainMenu;
        gotoMainMenuTimer.TimeUpdateEvent -= UpdateTimeValue;
    }

}
