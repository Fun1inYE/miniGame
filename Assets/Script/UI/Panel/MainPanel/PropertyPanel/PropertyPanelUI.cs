using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 属性列表类
/// </summary>
public class PropertyPanelUI : MonoBehaviour
{
    /// <summary>
    /// 蓝宝石的数量
    /// </summary>
    private Text coinValue;

    /// <summary>
    /// 生命基础属性信息格
    /// </summary>
    private Transform healthPropertyInfoGrid;
    /// <summary>
    /// 移动速度基础属性信息格
    /// </summary>
    private Transform speedPropertyInfoGrid;
    /// <summary>
    /// 攻击力基础属性信息格
    /// </summary>
    private Transform attackPropertyInfoGrid;
    /// <summary>
    /// 攻击速度基础属性信息格
    /// </summary>
    private Transform attackSpeedPropertyInfoGrid;

    /// <summary>
    /// 没点亮的技能点图像
    /// </summary>
    private GameObject skillImage_0;
    /// <summary>
    /// 点亮的技能点图像
    /// </summary>
    private GameObject skillImage_1;

    private PropertyPanelData data;


    private void Awake()
    {
        InitUI();
    }

    /// <summary>
    /// 初始化UI
    /// </summary>
    private void InitUI()
    {
        coinValue = ComponentFinder.GetChildComponent<Text>(gameObject, "CoinValue");

        //获取对应的技能点的图像
        skillImage_0 = Resources.Load<GameObject>("Prefab/UI/SkillImage_0");
        skillImage_1 = Resources.Load<GameObject>("Prefab/UI/SkillImage_1");

        //获取到各种技能信息格
        healthPropertyInfoGrid = FindAndMoveObject.FindChildRecursive(transform, "HealthPropertyInfoGrid");
        speedPropertyInfoGrid = FindAndMoveObject.FindChildRecursive(transform, "SpeedPropertyInfoGrid");
        attackPropertyInfoGrid = FindAndMoveObject.FindChildRecursive(transform, "AttackPropertyInfoGrid");
        attackSpeedPropertyInfoGrid = FindAndMoveObject.FindChildRecursive(transform, "AttackSpeedPropertyInfoGrid");

        //获取到并初始化技能点指示面板
        GameObject healthProperty_SkillImagePanel = FindPanel_SkillImagePanel(healthPropertyInfoGrid);
        UpdateSkillImagePanel(healthProperty_SkillImagePanel, BuffManager.Instance.buffData.HpLevel);

        GameObject speedProperty_SkillImagePanel = FindPanel_SkillImagePanel(speedPropertyInfoGrid);
        UpdateSkillImagePanel(speedProperty_SkillImagePanel, BuffManager.Instance.buffData.SpeedLevel);

        GameObject attackProperty_SkillImagePanel = FindPanel_SkillImagePanel(attackPropertyInfoGrid);
        UpdateSkillImagePanel(attackProperty_SkillImagePanel, BuffManager.Instance.buffData.AttackLevel);

        GameObject attackSpeedProperty_SkillImagePanel = FindPanel_SkillImagePanel(attackSpeedPropertyInfoGrid);
        UpdateSkillImagePanel(attackSpeedProperty_SkillImagePanel, BuffManager.Instance.buffData.AttackSpeedLevel);

        //获取到按钮并且添加功能
        Button addHealthProperty = FindButton_AddPointButton(healthPropertyInfoGrid);
        //获取对应Text并且添加功能
        Text healthCashValue = FindText_CashValue(healthPropertyInfoGrid);
        //更新CashValueUI
        UpdateSkillCashValue(healthCashValue, BuffManager.Instance.buffData.HpLevel);

        addHealthProperty.onClick.AddListener(() =>
        {
            if (EconomyManager.Instance.coin >= (BuffManager.Instance.buffData.HpLevel + 1) * PlayerBuffLevelData.SPANDING_BOOST)
            {
                if (BuffManager.Instance.buffData.HpLevel < PlayerBuffLevelData.MAX_LEVEL)
                {
                    EconomyManager.Instance.ChangeCoin(-((BuffManager.Instance.buffData.HpLevel + 1) * PlayerBuffLevelData.SPANDING_BOOST));
                    //升级对应属性
                    BuffManager.Instance.buffData.HpLevel++;
                    //更新UI
                    UpdateSkillCashValue(healthCashValue, BuffManager.Instance.buffData.HpLevel);
                    //更新等级UI
                    UpdateSkillImagePanel(healthProperty_SkillImagePanel, BuffManager.Instance.buffData.HpLevel);
                    //保存buff信息
                    BuffManager.Instance.Save();
                }
            }
            else
            {
                DisplayWithDoTween.FlashText(healthCashValue, Color.white, Color.red, 0.1f, 3);
            }
        });

        Button addSpeedProperty = FindButton_AddPointButton(speedPropertyInfoGrid);
        //获取对应Text并且添加功能
        Text speedCashValue = FindText_CashValue(speedPropertyInfoGrid);
        //更新CashValueUI
        UpdateSkillCashValue(speedCashValue, BuffManager.Instance.buffData.SpeedLevel);

        addSpeedProperty.onClick.AddListener(() =>
        {
            if (EconomyManager.Instance.coin >= (BuffManager.Instance.buffData.SpeedLevel + 1) * PlayerBuffLevelData.SPANDING_BOOST)
            {
                if (BuffManager.Instance.buffData.SpeedLevel < PlayerBuffLevelData.MAX_LEVEL)
                {
                    EconomyManager.Instance.ChangeCoin(-((BuffManager.Instance.buffData.SpeedLevel + 1) * PlayerBuffLevelData.SPANDING_BOOST));
                    BuffManager.Instance.buffData.SpeedLevel++;

                    Debug.Log(BuffManager.Instance.buffData.SpeedLevel);
                    Debug.Log(BuffManager.Instance.buffData.AttackSpeedLevel);

                    //更新UI
                    UpdateSkillCashValue(speedCashValue, BuffManager.Instance.buffData.SpeedLevel);
                    UpdateSkillImagePanel(speedProperty_SkillImagePanel, BuffManager.Instance.buffData.SpeedLevel);

                    //保存buff信息
                    BuffManager.Instance.Save();
                }
            }
            else
            {
                DisplayWithDoTween.FlashText(speedCashValue, Color.white, Color.red, 0.1f, 3);
            }
        });

        Button addAttackProperty = FindButton_AddPointButton(attackPropertyInfoGrid);
        Text attackCashValue = FindText_CashValue(attackPropertyInfoGrid);

        //更新CashValueUI
        UpdateSkillCashValue(attackCashValue, BuffManager.Instance.buffData.AttackLevel);

        addAttackProperty.onClick.AddListener(() =>
        {
            if (EconomyManager.Instance.coin >= (BuffManager.Instance.buffData.AttackLevel + 1) * PlayerBuffLevelData.SPANDING_BOOST)
            {
                if (BuffManager.Instance.buffData.AttackLevel < PlayerBuffLevelData.MAX_LEVEL)
                {
                    EconomyManager.Instance.ChangeCoin(-((BuffManager.Instance.buffData.AttackLevel + 1) * PlayerBuffLevelData.SPANDING_BOOST));
                    BuffManager.Instance.buffData.AttackLevel++;
                    //更新CashValueUI
                    UpdateSkillCashValue(attackCashValue, BuffManager.Instance.buffData.AttackLevel);
                    UpdateSkillImagePanel(attackProperty_SkillImagePanel, BuffManager.Instance.buffData.AttackLevel);

                    //保存buff信息
                    BuffManager.Instance.Save();
                }
            }
            else
            {
                DisplayWithDoTween.FlashText(attackCashValue, Color.white, Color.red, 0.1f, 3);
            }
        });

        Button addAttackSpeedProperty = FindButton_AddPointButton(attackSpeedPropertyInfoGrid);
        Text attackSpeedCashValue = FindText_CashValue(attackSpeedPropertyInfoGrid);

        //更新CashValueUI
        UpdateSkillCashValue(attackSpeedCashValue, BuffManager.Instance.buffData.AttackSpeedLevel);

        addAttackSpeedProperty.onClick.AddListener(() =>
        {
            if (EconomyManager.Instance.coin >= (BuffManager.Instance.buffData.AttackSpeedLevel + 1) * PlayerBuffLevelData.SPANDING_BOOST)
            {
                if (BuffManager.Instance.buffData.AttackSpeedLevel < PlayerBuffLevelData.MAX_LEVEL)
                {
                    //消费金币
                    EconomyManager.Instance.ChangeCoin(-((BuffManager.Instance.buffData.AttackSpeedLevel + 1) * PlayerBuffLevelData.SPANDING_BOOST));
                    BuffManager.Instance.buffData.AttackSpeedLevel++;
                    //更新CashValueUI
                    UpdateSkillCashValue(attackSpeedCashValue, BuffManager.Instance.buffData.AttackSpeedLevel);
                    UpdateSkillImagePanel(attackSpeedProperty_SkillImagePanel, BuffManager.Instance.buffData.AttackSpeedLevel);

                    //保存buff信息
                    BuffManager.Instance.Save();
                }
               
            }
            else
            {
                DisplayWithDoTween.FlashText(attackSpeedCashValue, Color.white, Color.red, 0.1f, 3);
            }
        });
    }

    public void UpdateSkillImage()
    {
        //获取到并初始化技能点指示面板
        GameObject healthProperty_SkillImagePanel = FindPanel_SkillImagePanel(healthPropertyInfoGrid);
        UpdateSkillImagePanel(healthProperty_SkillImagePanel, BuffManager.Instance.buffData.HpLevel);

        GameObject speedProperty_SkillImagePanel = FindPanel_SkillImagePanel(speedPropertyInfoGrid);
        UpdateSkillImagePanel(speedProperty_SkillImagePanel, BuffManager.Instance.buffData.SpeedLevel);

        GameObject attackProperty_SkillImagePanel = FindPanel_SkillImagePanel(attackPropertyInfoGrid);
        UpdateSkillImagePanel(attackProperty_SkillImagePanel, BuffManager.Instance.buffData.AttackLevel);

        GameObject attackSpeedProperty_SkillImagePanel = FindPanel_SkillImagePanel(attackSpeedPropertyInfoGrid);
        UpdateSkillImagePanel(attackSpeedProperty_SkillImagePanel, BuffManager.Instance.buffData.AttackSpeedLevel);
    }


    /// <summary>
    /// 更新技能等级指示器面板
    /// </summary>
    /// <param name="skillImagePanel"></param>
    private void UpdateSkillImagePanel(GameObject skillImagePanel, int level)
    {
        // 循环删除所有子对象(确保每次删除的时候能正常显示技能点效果)
        for (int i = skillImagePanel.transform.childCount - 1; i >= 0; i--)
        {
            // 使用Destroy立即销毁对象
            Destroy(skillImagePanel.transform.GetChild(i).gameObject);
        }

        //生成对应数量的点亮技能数量
        for (int i = 0; i < level; i++)
        {
            Instantiate(skillImage_1, skillImagePanel.transform);
        }
        //生成对应数量的非点亮技能数量
        for (int i = 0; i < 5 - level; i++)
        {
            Instantiate(skillImage_0, skillImagePanel.transform);
        }
    }

    /// <summary>
    /// 更新技能升级的UI
    /// </summary>
    /// <param name="cashValue"></param>
    /// <param name="level"></param>
    private void UpdateSkillCashValue(Text cashValue, int level)
    {   
        if (level == 5)
        {
            cashValue.text = "已满级";
        }
        else
        {
            //默认读取下一级数据
            level++;
            cashValue.text = "升级所需金币: " + level * PlayerBuffLevelData.SPANDING_BOOST;
        }
    }

    public void UpdateUI()
    {
        coinValue.text = data.Coin.ToString();
    }

    public void SetData(PropertyPanelData data)
    {
        this.data = data;
        UpdateUI();
        this.data.OnDataChange += UpdateUI;
    }

    public void CloseUI()
    {
        this.data.OnDataChange -= UpdateUI;
    }

    #region 寻找UI的方法

    /// <summary>
    /// 寻找基础技能点指示面板
    /// </summary>
    /// <returns></returns>
    private GameObject FindPanel_SkillImagePanel(Transform infoGrid)
    {
        return FindAndMoveObject.FindChildBreadthFirst(infoGrid, "SkillImagePanel").gameObject;
    }

    /// <summary>
    /// 寻找介绍技能的按钮
    /// </summary>
    /// <param name="infoGrid"></param>
    private Button FindButton_DescriptionButton(Transform infoGrid)
    {
        return FindAndMoveObject.FindChildBreadthFirst(infoGrid, "SkillDescriptionButton").GetComponent<Button>();
    }

    /// <summary>
    /// 寻找升级所需要的价格的Text节点
    /// </summary>
    /// <param name="infoGrid"></param>
    /// <returns></returns>
    private Text FindText_CashValue(Transform infoGrid)
    {
        return FindAndMoveObject.FindChildBreadthFirst(infoGrid, "CashValue").GetComponent<Text>();
    }

    /// <summary>
    /// 寻找增添技能点的按钮
    /// </summary>
    /// <param name="infoGrid">信息格子</param>
    private Button FindButton_AddPointButton(Transform infoGrid)
    {
        return FindAndMoveObject.FindChildBreadthFirst(infoGrid, "AddPointButton").GetComponent<Button>();
    }
    
    #endregion
}
