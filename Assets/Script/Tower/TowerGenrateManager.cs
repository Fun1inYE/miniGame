using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 建造防御塔的状态枚举
/// </summary>
public enum BuildTowerState
{
    //没有选择防御塔，选择建造位置, 正在建造, 完成建造
    None, SelectPosition, Building, Finish
}

/// <summary>
/// 防御塔生成的管理器
/// </summary>
public class TowerGenrateManager : MonoBehaviour
{
    /// <summary>
    /// 建造状态
    /// </summary>
    private BuildTowerState state;

    /// <summary>
    /// 要建造的塔
    /// </summary>
    private GameObject tower; 

    /// <summary>
    /// 用于判断塔是否可以建设
    /// </summary>
    private Tower buildState;

    /// <summary>
    /// 塔的towerSpriteRenderer
    /// </summary>
    private SpriteRenderer towerSpriteRenderer;

    /// <summary>
    /// 范围指示器的SpriteRenderer
    /// </summary>
    private SpriteRenderer rangeIndicatorSpriteRenderer;

    /// <summary>
    /// 塔的范围中心点
    /// </summary>
    private Transform rangePoint;

    /// <summary>
    /// 玩家的位置
    /// </summary>
    private Transform playerTransform;

    private void Awake()
    {
        //初始化状态
        state = BuildTowerState.None;
    }


    private void LateUpdate()
    {
        //通过不同的枚举状态选择不同应该进行的状态方法
        switch(state)
        {
            case BuildTowerState.None:
                break;
            case BuildTowerState.SelectPosition:
                TowerFollowPlayer();
                break;
            case BuildTowerState.Building:
                BuildTower();
                break;
            case BuildTowerState.Finish: 
                FinishBuild();
                break;
        }
    }   

    /// <summary>
    /// 根据选择的塔类型建造塔
    /// </summary>
    /// <param name="towerType"></param>
    public void SelectATower(TowerType towerType)
    {
        //如果是第一次建造塔的话，需要找一次玩家的位置
        if(playerTransform == null)
        {
            playerTransform = FindAndMoveObject.FindFromFirstLayer("Player").transform;
        }

        //将这个塔生成出来
        tower = Instantiate(TowerFactory.CreateATowerWithTowerType(towerType));

        //获取到Tower状态
        buildState = tower.GetComponent<Tower>();

        //获取到范围中心点
        rangePoint = FindAndMoveObject.FindChildRecursive(tower.transform, "RangeIndicatorPoint");
        //获取到tower的spriteRenderer
        towerSpriteRenderer = tower.GetComponent<SpriteRenderer>();
        //将sprite更改成半透明
        towerSpriteRenderer.color = new Color(255, 255, 255, 0.5f);

        //获取到防御塔的范围指示器
        GameObject rangeIndicator = FindAndMoveObject.FindChildRecursive(tower.transform, "RangeIndicator").gameObject;
        rangeIndicatorSpriteRenderer = rangeIndicator.GetComponent<SpriteRenderer>();
        //改变范围指示器半径和颜色
        //获取到这个防御塔的范围
        float range = tower.GetComponent<TowerSearchAndLaunch>().range;
        //改变范围指示器的大小
        rangeIndicator.transform.localScale = new Vector3(range, range, 1);
        //改变范围指示器颜色和透明度
        rangeIndicatorSpriteRenderer.color = new Color(255, 255, 255, 0.3f);

        //给塔移动位置
        tower.transform.position = playerTransform.position - rangePoint.localPosition - new Vector3(0, 6f, 0f);
        
        //转换状态
        state = BuildTowerState.SelectPosition;
    }

    /// <summary>
    /// 塔跟随玩家的效果方法
    /// </summary>
    private void TowerFollowPlayer()
    {
        //改变相机的正交值
        Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, 300, 0.01f);
        if(Camera.main.orthographicSize + 1 > 300)
        {
            Camera.main.orthographicSize = 300f;
        }
        
        //判断位置
        if (Vector3.Distance(tower.transform.position, playerTransform.position - rangePoint.localPosition - new Vector3(0, 6f, 0f)) < 0.01f)
        {
            tower.transform.position = playerTransform.position - rangePoint.localPosition - new Vector3(0, 6f, 0f);
        }
        else
        {
            //要让塔范围指示器中心的位置和玩家的位置重合,6f是对玩家的偏移
            //使用插值，防止抖动
            tower.transform.position = Vector3.Lerp(tower.transform.position, playerTransform.position - rangePoint.localPosition - new Vector3(0, 6f, 0f), 0.01f);
        }

        //不能建造的时候就要更改塔范围圈的颜色
        if(buildState.canbuild == false && rangeIndicatorSpriteRenderer.color == new Color(255, 255, 255, 0.3f))
        {
            rangeIndicatorSpriteRenderer.color = new Color(255, 0, 0, 0.3f);
        }
        //能建造塔的时候要更改范围圈的颜色
        else if(buildState.canbuild == true && rangeIndicatorSpriteRenderer.color == new Color(255, 0, 0, 0.3f))
        {
            rangeIndicatorSpriteRenderer.color = new Color(255, 255, 255, 0.3f);
        }
    }

    /// <summary>
    /// 改变状态到建造状态
    /// </summary>
    public void SwitchStateToBuilding()
    {
        if(buildState.canbuild)
        {
            state = BuildTowerState.Building;
        }
    }

    /// <summary>
    /// 建造塔的方法
    /// </summary>
    private void BuildTower()
    {
        //塔改成不透明
        towerSpriteRenderer.color = new Color(255, 255, 255, 1f);
        //范围圈完全透明
        rangeIndicatorSpriteRenderer.color = new Color(255, 255, 255, 0f);
        //初始化魔法塔的攻击逻辑
        tower.GetComponent<TowerSearchAndLaunch>().StartSearchAndLaunchCoro();

        //将这个塔加入到塔的管理器当中
        TowerManager.Instance.RegisterTower(tower);

        //然后去除各种缓存
        tower = null;
        buildState = null;
        towerSpriteRenderer = null;
        rangeIndicatorSpriteRenderer = null;

        //转换状态到完成建造
        state = BuildTowerState.Finish;
    }

    /// <summary>
    /// 完成建造的方法
    /// </summary>
    private void FinishBuild()
    {
        //改变摄像机的正交值
        Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, 150, 0.01f);

        if(Camera.main.orthographicSize - 0.1f < 150f)
        {
            Camera.main.orthographicSize = 150f;
        }

        if(Camera.main.orthographicSize == 150f)
        {
            //转换状态
            state = BuildTowerState.None;
        }
    }

    /// <summary>
    /// 终止建设的方法
    /// </summary>
    public void CancelBuild()
    {
        //直接去除tower的GameObject
        Destroy(tower);
        //清缓存
        tower = null;
        buildState = null;
        towerSpriteRenderer = null;
        rangeIndicatorSpriteRenderer = null;
        rangePoint = null;

        //直接将状态转换到Finish的状态
        state = BuildTowerState.Finish;
    }
}
