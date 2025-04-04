using System.Collections;
using UnityEngine;

/// <summary>
/// 远程史莱姆远程攻击逻辑
/// </summary>
public class RemoteSlimeRemoteAttack : EnemyRemoteAttack
{
    /// <summary>
    /// 要射击子弹的预制体
    /// </summary>
    public GameObject bulletPrefab;

    protected override void Update()
    {
        base.Update();
        bool findTarget = FindTarget();
        if(findTarget == true && searchAndLaunchCoro == null)
        {
            searchAndLaunchCoro = StartCoroutine(SearchAndLaunch());
            //使远程史莱姆停止运动
            gameObject.GetComponent<RemoteSlimeMove>().canMove = false;
            //进入攻击冷却
            StartAttackColdCoro();
        }
        else if(findTarget == false && searchAndLaunchCoro != null)
        {
            StopCoroutine(searchAndLaunchCoro);
            searchAndLaunchCoro = null;
            gameObject.GetComponent<RemoteSlimeMove>().canMove = true;
        }
    }

    /// <summary>
    /// 按照frequency寻找并且朝目标发射子弹
    /// </summary>
    /// <returns></returns>
    private IEnumerator SearchAndLaunch()
    {
        while(true)
        {
            //如果寻找到玩家的话
            if(FindTarget() && canAttack)
            {
                //开始攻击
                gameObject.GetComponent<RemoteSlimeAnimation>().isAttack = true;
            }

            yield return new WaitForSeconds(frequency);
        }
    }

    /// <summary>
    /// 发射子弹的方法(挂载到Animator中的Event事件中了)
    /// </summary>
    public void Fire()
    {
        //首先生成子弹
        if(bulletPrefab == null)
        {
            Debug.LogError("史莱姆没有子弹的预制体");
            return;
        }

        //生成子弹
        GameObject bulletObj =  Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bulletObj.GetComponent<BulletMove>().target = target;
    }
}
