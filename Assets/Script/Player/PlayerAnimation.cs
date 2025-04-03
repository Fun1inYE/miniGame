using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Player的动画调整
/// </summary>
public class PlayerAnimation : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    public Rigidbody2D rb;

    public Animator anim;

    /// <summary>
    /// 判断玩家贴图是否冲向右边
    /// </summary>
    private bool isFaceToRight = true;

    private void Start()
    {
        if(spriteRenderer == null)
        {
            Debug.LogError("spriteRenderer是空的");
        } 
        if(rb == null)
        {
            Debug.LogError("rb是空的");
        } 
        if(anim == null)
        {
             Debug.LogError("anim是空的");
        }
    }

    public void Update()
    {
        SwitchAnimation();
        ClipPlayer();
    } 

    /// <summary>
    /// 切换玩家动作的方法
    /// </summary>
    public void SwitchAnimation()
    {
        //当玩家的速度不为0的时候
        if(rb.velocity != default)
        {
            anim.SetFloat("Speed", 1);
        }
        else
        {
            anim.SetFloat("Speed", 0);
        }
    }

    /// <summary>
    /// 根据速度方向和bool值水平翻转玩家
    /// </summary>
    private void ClipPlayer()
    {
        //判断条件为如果人物的速度大于0,那就翻转sprite
        if(rb.velocity.x > 0.1f && !isFaceToRight)
        {
            //玩家朝右了
            isFaceToRight = true;
            //不翻转贴图
            spriteRenderer.flipX = false;
        }
        //另一种情况就是翻转
        else if(rb.velocity.x < -0.1f && isFaceToRight)
        {
            isFaceToRight = false;
            spriteRenderer.flipX = true;
        }
    }
}
