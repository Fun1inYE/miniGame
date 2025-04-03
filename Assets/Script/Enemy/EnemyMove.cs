using UnityEngine;

/// <summary>
/// 敌人移动的类
/// </summary>
public class EnemyMove : MonoBehaviour
{
    /// <summary>
    /// 敌人要寻找的目标
    /// </summary>
    public GameObject target;

    /// <summary>
    /// 敌人移动的速度
    /// </summary>
    public float speed = 10f;

    /// <summary>
    /// 被击退的判断
    /// </summary>
    protected bool backing = false;

    public Rigidbody2D rb;

    protected virtual void OnEnable()
    {
        //寻找玩家的GameObject
        target = FindAndMoveObject.FindFromFirstLayer("Player");
        if(target == null)
        {
            Debug.LogError("没有再hirarchy窗口中寻找到Player");
        }
        
        rb = ComponentFinder.GetOrAddComponent<Rigidbody2D>(gameObject);
        rb.gravityScale = 0;
    }

    private void Update()
    {
        MoveToTarget();
    }

    /// <summary>
    /// 敌人向目标移动的方法
    /// </summary>
    protected virtual void MoveToTarget()
    {
        //计算移动方向
        Vector2 direction = (target.transform.position - transform.position).normalized;

        //如果敌人不处于击退状态的话
        if(!backing)
        {
            //给予敌人速度
            rb.velocity = direction * speed;
        }
        else
        {
            direction = (target.transform.position - transform.position).normalized;
            rb.velocity = Vector2.Lerp(rb.velocity, direction * speed, 0.03f);

            //判断当前速度等不等于怪物满速
            if(rb.velocity == direction * speed)
            {
                //退出击退状态
                backing = false;
            }
        }
    }

    /// <summary>
    /// 被击退的方法
    /// </summary>
    public virtual void Back(float back)
    {
        backing = true;
        //计算击退方向
        Vector2 direction = (transform.position - target.transform.position).normalized;
        //施加力
        rb.AddForce(direction * back, ForceMode2D.Impulse);
    }
}
