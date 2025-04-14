using UnityEngine;

/// <summary>
/// 武器攻击逻辑
/// </summary>
public class WeaponAttack : MonoBehaviour
{   
    /// <summary>
    /// 武器的攻击力
    /// </summary>
    public int attack;

    /// <summary>
    /// 武器的击退力
    /// </summary>
    public float back;

    /// <summary>
    /// 武器碰到敌人执行的方法
    /// </summary>
    /// <param name="other"></param>
    protected virtual void OnTriggerEnter2D(Collider2D other) 
    {
        //识别敌人
        if(other.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
            enemyHealth.Attacked(attack);
            EnemyMove enemyMove = other.gameObject.GetComponent<EnemyMove>();
            enemyMove.Back(back);
        }
    }
    
}
