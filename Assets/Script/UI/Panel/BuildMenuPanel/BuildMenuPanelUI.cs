using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 建筑菜单的UI
/// </summary>
public class BuildMenuPanelUI : MonoBehaviour
{
    /// <summary>
    /// 建造按钮的预制件
    /// </summary>
    private Button buildTowerButton_Prefab;

    /// <summary>
    /// 蓝宝石的数量
    /// </summary>
    private Text sapphireValue;

    /// <summary>
    /// 退回按钮
    /// </summary>
    private Button backButton;

    /// <summary>
    /// 建造塔的方法
    /// </summary>
    private Button buildTowerButton;

    /// <summary>
    /// 返回到建造菜单的按钮
    /// </summary>
    private Button backToMenuButton;

    /// <summary>
    /// 建造菜单面板
    /// </summary>
    private GameObject buildMenuPanel;

    /// <summary>
    /// 退回菜单的按钮
    /// </summary>
    private GameObject backToMenuButtonPanel;

    private TowerGenrateManager towerGenrateManager;

    /// <summary>
    /// UI负责更新的数据
    /// </summary>
    private BuildMenuPanelData data;

    private void Awake()
    {
        towerGenrateManager = ComponentFinder.GetOrAddComponent<TowerGenrateManager>(FindAndMoveObject.FindFromFirstLayer("TowerGenrateManager"));
        InitUI();
    }

    /// <summary>
    /// 初始化UI
    /// </summary>
    private void InitUI()
    {
        //寻找对应UI节点
        sapphireValue = ComponentFinder.GetChildComponent<Text>(gameObject, "ResourcesValue");
        //寻找返回按钮
        backButton = ComponentFinder.GetChildComponent<Button>(gameObject, "BackButton");
        //寻找两个按钮
        buildTowerButton = ComponentFinder.GetChildComponent<Button>(gameObject, "BuildTowerButton");
        backToMenuButton = ComponentFinder.GetChildComponent<Button>(gameObject, "BackToMenuButton");

        //按钮绑定监听事件
        backButton.onClick.AddListener(() => {
            //弹出当前窗口
            UIManager.Instance.GetPanelManager().Pop();
        });

        buildTowerButton.onClick.AddListener(() => {
            //确定建造塔
            towerGenrateManager.SwitchStateToBuilding();
            //然后返回菜单(花费资源了)
            BackToBuildMenu(true);
        });

        backToMenuButton.onClick.AddListener(() => {
            //取消建设
            towerGenrateManager.CancelBuild();
            //返回菜单(没有花费资源)
            BackToBuildMenu(false);
        });

        //寻找两个面板
        buildMenuPanel = FindAndMoveObject.FindChildRecursive(transform, "BuildMenuPanel").gameObject;
        backToMenuButtonPanel = FindAndMoveObject.FindChildRecursive(transform, "BuildButtonPanel").gameObject;
        //设置面板是否可见
        buildMenuPanel.SetActive(true);
        backToMenuButtonPanel.SetActive(false);

        InitBuildMenu();
    }

    /// <summary>
    /// 初始化建筑菜单
    /// </summary>
    private void InitBuildMenu()
    {
        //获取到建造按钮的预制件
        buildTowerButton_Prefab = Resources.Load<GameObject>("Prefab/UI/BuildTowerButton").GetComponent<Button>();
        //获取对应的编辑器GameObject
        GameObject buildTowerInventoryEditor = FindAndMoveObject.FindFromFirstLayer("BuildTowerInventoryEditor");
        BuildTowerInventoryEditor editor = buildTowerInventoryEditor.GetComponent<BuildTowerInventoryEditor>();

        //判断BuildTowerInventoryEditor是否初始化成功
        if(editor.IsInitInventory == false)
        {
            Debug.LogError("建筑菜单编辑器初始化没成功, 关卡按钮没有正确生成");
            return;
        }

        //获取到按钮应该生成在的位置
        Transform content = FindAndMoveObject.FindChildRecursive(transform, "Content");

        //获取到塔的数量
        int towerCount = editor.towerList.Count;

        //开始生成塔的按钮
        for(int i = 0; i < towerCount; i++)
        {
            int index = i;
            GameObject buttonObj = Instantiate(buildTowerButton_Prefab.gameObject, content);
            //更改对应贴图和价格
            Text price = FindAndMoveObject.FindChildBreadthFirst(buttonObj.transform, "TowerPrice").GetComponent<Text>();
            price.text = editor.towerList[index].price.ToString();
            Image image = FindAndMoveObject.FindChildBreadthFirst(buttonObj.transform, "TowerImage").GetComponent<Image>();
            image.sprite = editor.towerList[index].sprite;
            
            //给按钮绑定对应功能
            buttonObj.GetComponent<Button>().onClick.AddListener(() => {
                //设置面板可见性
                buildMenuPanel.SetActive(false);
                backToMenuButtonPanel.SetActive(true);
                //通过towerGenrateManager生成塔
                towerGenrateManager.SelectATower(editor.towerList[index].towerType);
            });
        }
    }

    /// <summary>
    /// 返回建造菜单的方法
    /// </summary>
    /// <param name="isSpend"></param>
    private void BackToBuildMenu(bool isSpend)
    {
        //设置面板可见性
        buildMenuPanel.SetActive(true);
        backToMenuButtonPanel.SetActive(false);

        //是否是建设中途取消的呢
        if(isSpend)
        {
            //TODO:扣除资源的逻辑
        }
    }

    /// <summary>
    /// 这里要通过控制器来对playerData进行赋值
    /// </summary>
    /// <param name="data"></param>
    public void SetData(BuildMenuPanelData data)
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
        sapphireValue.text = data.Sapphire.ToString();
    }
}
