using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 有关敌人血量的逻辑部分
/// </summary>
public class EnemyHealth : MonoBehaviour
{
    /// <summary>
    /// 敌人的血量
    /// </summary>
    public int health;

    protected virtual void Update()
    {
        if(health <= 0)
        {
            //向怪物生成器发送怪物死亡消息
            MessageManager.Instance.Send<int>(MessageDefine.ENEMY_COUNTDOWN, 1);
            EnemyManager.Instance.UnregisterEnemy(gameObject);
            //销毁gameObject
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// 被攻击的方法
    /// </summary>
    public virtual void Attacked(int attack)
    {
        health = health - attack;
        Debug.Log($"{gameObject.name}被攻击了! 掉血 {attack} 剩余血量 {health}");
    }
}
