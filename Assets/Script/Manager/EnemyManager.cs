using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敌人的管理器
/// </summary>
public class EnemyManager : Singleton<EnemyManager>
{
    /// <summary>
    /// 生成的敌人列表
    /// </summary>
    private List<GameObject> enemyList = new List<GameObject>();

    protected override void Awake()
    {
        //单例初始化
        base.Awake();
    }

    /// <summary>
    /// 添加一个敌人的方法
    /// </summary>
    /// <param name="enemy"></param>
    public void RegisterEnemy(GameObject enemy)
    {
        enemyList.Add(enemy);
    }

    /// <summary>
    /// 消除一个敌人的方法
    /// </summary>
    /// <param name="enemy"></param>
    public void UnregisterEnemy(GameObject enemy)
    {
        enemyList.Remove(enemy);
    }
    
    /// <summary>
    /// 寻找距离position最近的敌人
    /// </summary>
    /// <param name="position"></param>
    /// <param name="range"></param>
    /// <returns></returns>
    public GameObject GetNearestEnemy(Vector3 position, float range)
    {
        GameObject nearestEnemy = null;
        float minDistance = Mathf.Infinity;

        //如果范围为无穷大，则查找屏幕内的所有敌人
        if(range == float.MaxValue)
        {
            foreach (GameObject enemy in enemyList)
            {
                if (JudgmentPoint.IsInScreen(enemy.transform.position))
                {
                    //进行距离比较
                    float distance = Vector3.Distance(position, enemy.transform.position);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        nearestEnemy = enemy;
                    }
                }
            }
        }
        //查找范围内的敌人
        else
        {
            foreach (GameObject enemy in enemyList)
            {
                //进行距离比较
                float distance = Vector3.Distance(position, enemy.transform.position);
                if (distance < minDistance && distance <= range)
                {
                    minDistance = distance;
                    nearestEnemy = enemy;
                }
            }
        }

        return nearestEnemy;
    }
}
