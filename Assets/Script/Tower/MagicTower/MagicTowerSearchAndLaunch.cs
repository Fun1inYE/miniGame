using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 魔法塔的搜索和发射逻辑
/// </summary>
public class MagicTowerSearchAndLaunch : TowerSearchAndLaunch
{
    /// <summary>
    /// 魔法塔的子弹prefab
    /// </summary>
    public GameObject bulletPrefab;

    /// <summary>
    /// 重写搜索方法
    /// </summary>
    protected override IEnumerator SearchAndLaunchCoro()
    {
        while(true)
        {
            //查找最近的敌人
            GameObject nearestEnemy = EnemyManager.Instance.GetNearestEnemy(transform.position, range);
            if(nearestEnemy != null)
            {
                if(bulletPrefab == null)
                {
                    Debug.LogError("子弹预制体为空, 无法发射子弹");
                }
                else
                {
                    Debug.Log("射出了一个子弹");
                    Debug.Log(nearestEnemy.transform.position);
                    //生成子弹
                    GameObject bullet = Instantiate(bulletPrefab, launchPoint.position, Quaternion.identity);
                    //设置子弹的攻击目标
                    bullet.GetComponent<BulletMove>().target = nearestEnemy;
                }
            }
            yield return new WaitForSeconds(frequency);
        }
    }
}
