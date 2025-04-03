using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 飞行武器瞄准并且发射的逻辑
/// </summary>
public class SearchAndLaunch : MonoBehaviour
{
    /// <summary>
    /// 子弹的预制体
    /// </summary>
    public GameObject bulletPrefab;

    /// <summary>
    /// 攻击范围
    /// </summary>
    public float range;

    /// <summary>
    /// 发射频率
    /// </summary>
    public float frequency;

    /// <summary>
    /// 发射协程
    /// </summary>
    private Coroutine fireCoroutine;

    private void Update()
    {
        GameObject nearestEnemy = FindNearestEnemy();
        if (nearestEnemy != null && fireCoroutine == null)
        {
            StartFire();
        }
        else if (nearestEnemy == null && fireCoroutine != null)
        {
            StopFire();
        }
    }

    /// <summary>
    /// 开始开火
    /// </summary>
    private void StartFire()
    {
        fireCoroutine = StartCoroutine(Launch());
    }

    /// <summary>
    /// 按照频率发射子弹
    /// </summary>
    /// <returns></returns>
    protected virtual IEnumerator Launch()
    {
        while(true)
        {
            //查找最近的敌人
            GameObject nearestEnemy = FindNearestEnemy();
            if(nearestEnemy != null)
            {
                if(bulletPrefab == null)
                {
                    Debug.LogError("子弹预制体为空, 无法发射子弹");
                }
                else
                {
                    Debug.Log("射出了一个子弹");
                    //生成子弹
                    GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                    //设置子弹的攻击目标
                    bullet.GetComponent<BulletMove>().target = nearestEnemy;
                }
            }
            yield return new WaitForSeconds(frequency);
        }
    }

    /// <summary>
    /// 停止开火
    /// </summary>
    private void StopFire()
    {
        if (fireCoroutine != null)
        {
            StopCoroutine(fireCoroutine);
            fireCoroutine = null;
        }
    }

    /// <summary>
    /// 查找摄像头视野内最近的敌人
    /// </summary>
    /// <returns>最近的敌人对象</returns>
    private GameObject FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject nearestEnemy = null;
        float minDistance = Mathf.Infinity;

        //如果范围为无穷大，则查找屏幕内的所有敌人
        if(range == float.MaxValue)
        {
            foreach (GameObject enemy in enemies)
            {
                if (JudgmentPoint.IsInScreen(enemy.transform.position))
                {
                    //进行距离比较
                    float distance = Vector3.Distance(transform.position, enemy.transform.position);
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
            foreach (GameObject enemy in enemies)
            {
                //进行距离比较
                float distance = Vector3.Distance(transform.position, enemy.transform.position);
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
