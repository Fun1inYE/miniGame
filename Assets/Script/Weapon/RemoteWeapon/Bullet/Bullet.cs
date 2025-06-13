using System.Collections;
using UnityEngine;

/// <summary>
/// 子弹的逻辑
/// </summary>
public class Bullet : MonoBehaviour
{
    /// <summary>
    /// 子弹拖尾组件（非必要）
    /// </summary>
    private TrailRenderer trail;

    /// <summary>
    /// 子弹撞毁粒子（非必要）
    /// </summary>
    private ParticleSystem hitPartical;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    protected virtual void Awake()
    {
        //获取到拖尾
        trail = FindAndMoveObject.FindChildBreadthFirst(transform, "Trail").GetComponent<TrailRenderer>();
        //获取到粒子
        hitPartical = FindAndMoveObject.FindChildBreadthFirst(transform, "HitPartical").GetComponent<ParticleSystem>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// 根据拖尾使子弹GameObject销毁
    /// </summary>
    /// <returns></returns>
    public virtual IEnumerator DestroyAfterTrailFinished()
    {
        //贴图透明
        spriteRenderer.color = new Color(255, 255, 255, 0f);
        //速度为0
        rb.velocity = Vector2.zero;

        //播放粒子效果
        hitPartical.Play();

        //每0.5s检查一次拖尾是否播放结束
        while (trail != null && trail.positionCount > 0)
        {
            yield return new WaitForSeconds(0.5f);
        }

        Destroy(gameObject);
    }
}
