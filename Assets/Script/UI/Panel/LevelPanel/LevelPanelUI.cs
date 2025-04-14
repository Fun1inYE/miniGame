using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// LevelPanel的相关UI和逻辑类
/// </summary>
public class LevelPanelUI : MonoBehaviour
{
    /// <summary>
    /// 关卡的按钮预制件
    /// </summary>
    private Button levelButton_Prefab;

    /// <summary>
    /// 关卡按钮的面板
    /// </summary>
    private Transform levelButtonPanel;

    /// <summary>
    /// 用于存储所有的levelButton的GameObject
    /// </summary>
    private List<Button> levelButtonList = new List<Button>();
    
    /// <summary>
    /// 退出按钮
    /// </summary>
    private Button exitButton;

    private void Awake()
    {
        //获取按钮的Button的Prefab
        levelButton_Prefab = Resources.Load<GameObject>("Prefab/UI/LevelButton").GetComponent<Button>();
        //寻找到关卡按钮的面板
        levelButtonPanel = FindAndMoveObject.FindChildRecursive(transform, "LevelButtonPanel");
        //初始化UI
        InitUI();
    }

    private void InitUI()
    {
        //初始化exitButton
        exitButton = FindAndMoveObject.FindChildRecursive(transform, "ExitButton").GetComponent<Button>();
        exitButton.onClick.AddListener(() => {
            //弹出当前窗口
            UIManager.Instance.GetPanelManager().Pop();
        });
        
        //初始化LevelButton
        InitLevelButton();
    }

    /// <summary>
    /// 初始化关卡的按钮
    /// </summary>
    private void InitLevelButton()
    {
        //寻找到levelEditor
        GameObject levelEditor = FindAndMoveObject.FindFromFirstLayer("LevelEditor");
        //找到对应脚本
        LevelEditor editor = levelEditor.GetComponent<LevelEditor>();
        //判断LevelEditor是否初始化成功
        if(!editor.IsInitLevel)
        {
            Debug.LogError("关卡编辑器初始化没成功, 关卡按钮没有正确生成");
            return;
        }

        //获取到关卡数量
        int levelCount = editor.levels.Count;

        //开始生成levelButton
        for(int i = 0; i < levelCount; i++)
        {
            int index = i;
            GameObject buttonObj = Instantiate(levelButton_Prefab.gameObject, levelButtonPanel);
            Button button = buttonObj.GetComponent<Button>();

            // 获取按钮上的 Text 组件
            Text buttonText = buttonObj.GetComponentInChildren<Text>();
            if (buttonText != null)
            {
                // 设置按钮的文本为当前序号 + 1
                buttonText.text = (i + 1).ToString();
            }

            Debug.Log(i);

            button.onClick.AddListener(() => {
                //让GenerateEnemyManager读取到这一关的数据情况
                MessageManager.Instance.Send(MessageDefine.GET_WAVE_INFO, editor.levels[index].waves);
                MessageManager.Instance.Send(MessageDefine.GAMESTART);
                //退出所有窗口
                UIManager.Instance.GetPanelManager().AllPop();
                UIManager.Instance.GetPanelManager().Push(new GamingPanel());
                //打开摇杆
                FindAndMoveObject.FindFromFirstLayer("JoystickCanvas").SetActive(true);
            });

            //给列表中添加该按钮
            levelButtonList.Add(button);
        }
    }

    private void OnDisable()
    {
        //移除所有按钮的监听事件
        exitButton.onClick.RemoveAllListeners();
        
        foreach(var obj in levelButtonList)
        {
            obj.onClick.RemoveAllListeners();
        }
    }
}
