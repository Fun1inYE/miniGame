using UnityEngine;

/// <summary>
/// 飞刀的攻击逻辑
/// </summary>
public class FlyingKnifeAttack : WeaponAttack
{
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        //识别敌人
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            EnemyHealth enemyHealth = other.gameObject.GetComponent<SlimeHealth>();
            enemyHealth.Attacked(attack);
            EnemyMove enemyMove = other.gameObject.GetComponent<EnemyMove>();
            enemyMove.Back(back);

            //销毁子弹
            Destroy(gameObject);
        }
    }
}
