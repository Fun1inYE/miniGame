using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

/// <summary>
/// 敌人远程攻击的行为逻辑
/// </summary>
public class EnemyRemoteAttack : MonoBehaviour
{   
    /// <summary>
    /// 该敌人拥有的子弹GameObject
    /// </summary>
    public Dictionary<string, GameObject> bulletDic;

    /// <summary>
    /// 攻击目标
    /// </summary>
    public GameObject target;

    /// <summary>
    /// 攻击范围
    /// </summary>
    public float range;

    /// <summary>
    /// 发射频率
    /// </summary>
    public float frequency;

    /// <summary>
    /// 寻找并且发射子弹的协程
    /// </summary>
    public Coroutine searchAndLaunchCoro;

    /// <summary>
    /// 攻击冷却的协程
    /// </summary>
    protected Coroutine attackColdCoro;

    /// <summary>
    /// 判断敌人是否可以远程攻击(默认为true)
    /// </summary>
    protected bool canAttack;

    protected virtual void OnEnable()
    {
        target = FindAndMoveObject.FindFromFirstLayer("Player");
        canAttack = true;
    }

    protected virtual void Update()
    {
        
    }

    /// <summary>
    /// 寻找目标的方法
    /// </summary>
    /// <returns>是否找到了玩家</returns>
    protected virtual bool FindTarget()
    {
        //如果范围为无穷大，则敌人出现在屏幕中就会向目标远程攻击
        if(range == float.MaxValue && JudgmentPoint.IsInScreen(transform.position))
        {
            return true;
        }
        //查找范围内的玩家(range不为float.max)
        else if(range != float.MaxValue)
        {
            //进行距离比较
            float distance = Vector3.Distance(transform.position, target.transform.position);
            if (distance < range)
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// 敌人远程攻击的冷却
    /// </summary>
    /// <returns></returns>
    protected virtual IEnumerator RemoteAttackCold()
    {
        //用来计总时间时
        float time = 0;
        while(!canAttack)
        {
            yield return new WaitForSeconds(1f);
            time ++;
            if(time >= frequency)
            {
                canAttack = true;
            }
        }

        yield return null;
    }

    /// <summary>
    /// 开始远程攻击冷却协程
    /// </summary>
    protected void StartAttackColdCoro()
    {
        canAttack = false;
        attackColdCoro = StartCoroutine(RemoteAttackCold());
    }

    /// <summary>
    /// 结束远程攻击冷却协程
    /// </summary>
    protected void StopAttackColdCoro()
    {
        StopCoroutine(attackColdCoro);
        attackColdCoro = null;
    }
}
