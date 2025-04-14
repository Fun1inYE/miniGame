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

    private void OnEnable()
    {
        if(fireCoroutine == null)
        {
            StartFire();
        }        
    }

    /// <summary>
    /// 开始开火
    /// </summary>
    private void StartFire()
    {
        fireCoroutine = StartCoroutine(SearchAndLaunchCoro());
    }

    /// <summary>
    /// 按照频率搜索敌人并且发射子弹
    /// </summary>
    /// <returns></returns>
    protected virtual IEnumerator SearchAndLaunchCoro()
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

    private void OnDisable()
    {
        StopFire();
    }
}
