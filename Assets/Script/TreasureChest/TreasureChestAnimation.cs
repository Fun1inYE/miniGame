using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// 宝箱的动画逻辑
/// </summary>
public class TreasureChestAnimation : MonoBehaviour
{
    public Rigidbody2D rb;

    public Transform tran;

    public SpriteRenderer spriteRenderer;

    /// <summary>
    /// 宝箱被打开的贴图
    /// </summary>
    public Sprite openSprite;

    /// <summary>
    /// 宝箱停止的位置
    /// </summary>
    public Vector2 endPos;

    /// <summary>
    /// 判断动画是否需要播放
    /// </summary>
    private bool needPlay = true;


    private void Awake()
    {
        if(spriteRenderer != null)
        {
            //将贴图设置为透明
            spriteRenderer.color = new Color(255, 255, 255, 0f);
        }
    }

    private void OnEnable()
    {
        StartAnimaiton();
    }

    /// <summary>
    /// 开始宝箱的位置
    /// </summary>
    public void StartAnimaiton()
    {
        if(endPos == default)
        {
            Debug.LogError("宝箱没有最终落点");
            return;
        }

        //给宝箱一个向下的速度
        rb.velocity = new Vector2(0, -500f);
        //在0.5s内将color的透明度从0过渡到1
        spriteRenderer.DOFade(1, 0.2f);
    }

    private void Update()
    {
        //检测宝箱是否y轴小于中点位置
        if(needPlay && transform.position.y <= endPos.y)
        {
            //将宝箱停下来
            rb.velocity = Vector2.zero;
            //不需要继续播放动画了
            needPlay = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            //转变贴图
            spriteRenderer.sprite = openSprite;
            //
        }
    }
}
