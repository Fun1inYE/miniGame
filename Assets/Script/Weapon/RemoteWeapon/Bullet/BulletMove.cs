using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 子弹移动的逻辑
/// </summary>
public class BulletMove : MonoBehaviour
{
    /// <summary>
    /// 子弹的飞行速度
    /// </summary>
    public float speed;

    /// <summary>
    /// 攻击目标
    /// </summary>
    public GameObject target;

    /// <summary>
    /// 子弹有效范围
    /// </summary>
    public float effectiveRange;

    /// <summary>
    /// 子弹是否跟踪
    /// </summary>
    public bool canTrack;

    /// <summary>
    /// 子弹生成的位置
    /// </summary>
    protected Vector2 generatePos;

    /// <summary>
    /// 追踪锁(默认为false)
    /// </summary>
    protected bool trackLock = false;

    protected Rigidbody2D rb;

    private void OnEnable()
    {
        //记录子弹的生成位置    
        generatePos = transform.position;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        AimAndFlyToTarget();
    }

    private void LateUpdate()
    {
        CheckLoseEfficacy();
    }

    /// <summary>
    /// 瞄准并且射向目标的方法
    /// </summary>
    public virtual void AimAndFlyToTarget()
    {
        if(target == null)
        {
            Debug.Log("目标丢失");
            //如果子弹可以追踪的话，就找离子弹最近的敌人
            if(canTrack)
            {
                Debug.Log("子弹正在重新寻找目标");
                target = EnemyManager.Instance.GetNearestEnemy(transform.position, float.MaxValue);
            }
            return; 
        }

        //只有当追踪锁关闭的时候才会计算方向
        if(!trackLock)
        {
            //计算目标的方向
            Vector2 dir = (target.transform.position - transform.position).normalized;
            //这个子弹没有追踪功能
            if(!canTrack)
            {
                //打开跟踪锁
                trackLock = true;
            }

            /// 计算目标方向与 Vector3.up 之间的夹角
            float angle = Vector2.SignedAngle(Vector2.up, dir);
            // 创建旋转
            Quaternion rotation = Quaternion.Euler(0, 0, angle);
            // 应用旋转
            transform.rotation = rotation;

            //给子弹速度
            rb.velocity = dir * speed;
        }
    }

    /// <summary>
    /// 检查子弹是否失效的方法
    /// </summary>
    protected virtual void CheckLoseEfficacy()
    {
        if(effectiveRange != float.MaxValue)
        {
            //计算子弹的飞行距离，判断超没超过有效距离
            if(Vector2.Distance(generatePos, transform.position) > effectiveRange)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            //如果子弹飞出屏幕，就销毁子弹
            if(!JudgmentPoint.IsInScreen(transform.position))
            {
                Destroy(gameObject);
            }
        }
        
    }
}
