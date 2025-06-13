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
    /// 宝箱底部中心点
    /// </summary>
    Transform ringPoint;

    /// <summary>
    /// 判断动画是否需要播放
    /// </summary>
    private bool needPlay = true;

    /// <summary>
    /// 判断宝箱是否被开启过
    /// </summary>
    private bool hasOpened = false;


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
        ringPoint = FindAndMoveObject.FindChildRecursive(transform, "RingPoint");
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
        rb.velocity = new Vector2(0, -300f);
        //在0.5s内将color的透明度从0过渡到1
        spriteRenderer.DOFade(1, 0.5f);
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
            //摄像机摇动
            DisplayWithDoTween.CameraShake(Camera.main.transform);
            //绘制冲击波
            DrawShockWave();
        }
    }

    /// <summary>
    /// 绘制冲击波
    /// </summary>
    private void DrawShockWave()
    {
        //三连冲，频率为0.08s
        StartCoroutine(CreateTreasrue(3, 0.08f));
    }

    private IEnumerator CreateTreasrue(int times, float frequency)
    {
        int count = 0;
        while(count < times)
        {
            count++;
            //获取冲击波
            GameObject shockWave = ShockWaveFactory.CreateAShockWave();
            //生成冲击波
            Instantiate(shockWave, ringPoint);
            yield return new WaitForSeconds(frequency);
        }
        yield return null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && !hasOpened)
        {
            //转变贴图
            spriteRenderer.sprite = openSprite;
            //宝箱被打开了
            hasOpened = true;
            //播放动画
            FadeOut();
        }
    }

    /// <summary>
    /// 宝箱渐渐消失的方法
    /// </summary>
    private void FadeOut()
    {
        //创建一个动画序列
        Sequence sequence = DOTween.Sequence();
        //等待3s
        sequence.AppendInterval(3f);
        //让宝箱渐渐消失
        sequence.Append(spriteRenderer.DOFade(0, 0.5f));
        //动画结束后消除gameObject
        sequence.OnComplete(() => {
            Destroy(gameObject);
        });
    }
}
