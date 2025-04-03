using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 飞行武器瞄准并且发射的逻辑
/// </summary>
public class EmitterSearchAndLaunch : MonoBehaviour
{
    /// <summary>
    /// 子弹的预制体
    /// </summary>
    public GameObject bulletPrefab;

    /// <summary>
    /// 攻击目标
    /// </summary>
    public GameObject target;

    /// <summary>
    /// 攻击范围
    /// </summary>
    public float range;

    /// <summary>
    /// 子弹有效范围
    /// </summary>
    public float effectiveRange;

    /// <summary>
    /// 发射频率
    /// </summary>
    public float rate;

    /// <summary>
    /// 子弹的飞行速度
    /// </summary>
    public float speed;

    /// <summary>
    /// 子弹是否跟踪
    /// </summary>
    public bool canTrack;

    /// <summary>
    /// 发射协程
    /// </summary>
    private Coroutine fireCoroutine;

    private void OnEnable()
    {
        
    }

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
        fireCoroutine = StartCoroutine(LaunchBullet());
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
    /// 按照频率发射子弹
    /// </summary>
    /// <returns></returns>
    protected virtual IEnumerator LaunchBullet()
    {
        while(true)
        {
            //查找最近的敌人
            GameObject nearestEnemy = FindNearestEnemy();
            if(nearestEnemy != null)
            {
                if(bulletPrefab == null)
                {
                    OnLoadBullet();
                }
                Debug.Log("射出了一个子弹");
                //生成子弹
                GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                Debug.Log("bullet" + bullet);
                //设置子弹的攻击目标
                bullet.GetComponent<BulletMove>().target = nearestEnemy;
            }
            yield return new WaitForSeconds(rate);
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

    protected virtual void OnLoadBullet()
    {
        Debug.Log("父类方法中的OnLoadBullet");
    }
}
