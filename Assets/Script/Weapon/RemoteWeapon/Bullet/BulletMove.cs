using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
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
        Debug.Log("子弹没有实现瞄准方法");
    }

    /// <summary>
    /// 检查子弹是否失效的方法
    /// </summary>
    private void CheckLoseEfficacy()
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
