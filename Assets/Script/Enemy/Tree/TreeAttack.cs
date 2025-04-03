using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 树人的攻击逻辑
/// </summary>
public class TreeAttack : EnemyAttack
{
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            playerHealth.Attacked(attack);
        }
    }
}
