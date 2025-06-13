using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 宝箱画圆的方法
/// </summary>
public class DrawRing : MonoBehaviour
{
    [Header("外观设置")]
    //圆环的颜色
    public Color ringColor = Color.white;
    //线的宽度
    public float lineWidth = 1.5f;
    [Range(16, 128)] public int segments = 64;

    [Header("扩张设置")]
    public float startRadius = 0.5f;
    public float maxRadius = 1080f;
    public float expandSpeed = 0.3f;
    public AnimationCurve expandCurve = AnimationCurve.Linear(0, 0, 1, 1);

    private LineRenderer lineRenderer;
    private EdgeCollider2D edgeCollider;
    private Rigidbody2D rb;

    /// <summary>
    /// 当前半径
    /// </summary>
    private float currentRadius;
    private float progress;
    private Vector2[] colliderPoints;

    private void Awake()
    {
        InitShockWave();
        InitCollider();
        InitRigidBody();
    }

    private void InitShockWave()
    {
        // 获取组件
        lineRenderer = GetComponent<LineRenderer>();
        // 初始化LineRenderer
        lineRenderer.positionCount = segments + 1;
        lineRenderer.useWorldSpace = false;
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;
        lineRenderer.startColor = ringColor;
        lineRenderer.endColor = ringColor;
    }

    private void InitCollider()
    {
        edgeCollider = GetComponent<EdgeCollider2D>();
        edgeCollider.isTrigger = true;
        // 初始化碰撞点数组
        colliderPoints = new Vector2[segments + 1];
        currentRadius = startRadius;
    }

    private void InitRigidBody()
    {
        rb = GetComponent<Rigidbody2D>();
        //重力改为0
        rb.gravityScale = 0;
        //更改碰撞检测方式
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rb.sleepMode = RigidbodySleepMode2D.NeverSleep;
        rb.interpolation = RigidbodyInterpolation2D.Extrapolate;
        //防止旋转
        rb.freezeRotation = true;
    }

    private void Update()
    {
        // 更新扩张进度
        progress = Mathf.Clamp01(progress + Time.deltaTime * expandSpeed);
        currentRadius = Mathf.Lerp(startRadius, maxRadius, expandCurve.Evaluate(progress));
        
        // 绘制圆环
        UpdateRing();
        
        // 检查是否达到最大半径
        if (progress >= 1f)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// 更新圆环的方法
    /// </summary>
    private void UpdateRing()
    {
        float angle = 0f;
        float angleIncrement = 360f / segments;
        
        for (int i = 0; i <= segments; i++)
        {
            float x = Mathf.Sin(Mathf.Deg2Rad * angle) * currentRadius;
            float y = Mathf.Cos(Mathf.Deg2Rad * angle) * currentRadius;
            
            // 更新LineRenderer
            lineRenderer.SetPosition(i, new Vector3(x, y, 0));
            
            // 更新碰撞点
            colliderPoints[i] = new Vector2(x, y);
            
            angle += angleIncrement;
        }
        
        // 更新碰撞体
        edgeCollider.points = colliderPoints;
    }

    /// <summary>
    /// 发生碰撞的方法
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //如果碰撞到敌人的话
        if(collision.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = collision.GetComponent<EnemyHealth>();
            enemyHealth.Attacked(99999);
        }
        //清除子弹
        else if(collision.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
        }
    }
}
