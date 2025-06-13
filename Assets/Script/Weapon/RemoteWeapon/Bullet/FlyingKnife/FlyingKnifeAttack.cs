using UnityEngine;

/// <summary>
/// 飞刀的攻击逻辑
/// </summary>
public class FlyingKnifeAttack : WeaponAttack
{
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        //识别敌人
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            //获取到飞刀的其他逻辑
            FlyingKnife flyingKnife = GetComponent<FlyingKnife>();
            //开启协程
            StartCoroutine(flyingKnife.DestroyAfterTrailFinished());
        }
    }
}
