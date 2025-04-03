using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敌人攻击逻辑
/// </summary>
public class EnemyAttack : MonoBehaviour
{
    /// <summary>
    /// 攻击数值
    /// </summary>
    public int attack;

    public BoxCollider2D col;

    private void Awake()
    {
        col = ComponentFinder.GetOrAddComponent<BoxCollider2D>(gameObject);
        col.isTrigger = true;
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            playerHealth.Attacked(attack);
        }
    }
}
