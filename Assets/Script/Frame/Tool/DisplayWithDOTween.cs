using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.Events;

/// <summary>
/// 使用DoTween显示
/// </summary>
public static class DisplayWithDoTween
{
    /// <summary>
    /// UI浅入停留浅出
    /// </summary>
    /// <param name="canvasGroup"></param>
    /// <param name="fadeInTime"></param>
    /// <param name="stayTime"></param>
    /// <param name="fadeOutTime"></param>
    public static void MoveAndFadeWithUI(RectTransform UITrans, float fadeInTime, float stayTime, float fadeOutTime, float moveDistance, UnityAction action = null)
    {
        //获取到GameObject的CanvasGroup组件
        CanvasGroup canvasGroup = UITrans.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
        //创建动画序列
        Sequence sequence = DOTween.Sequence();
        //渐入
        sequence.Append(canvasGroup.DOFade(1, fadeInTime)).SetEase(Ease.InOutSine);
        sequence.Join(UITrans.DOAnchorPosY(UITrans.anchoredPosition.y + moveDistance, fadeInTime));
        //停留
        sequence.AppendInterval(stayTime);
        //渐出
        sequence.Append(canvasGroup.DOFade(0, fadeOutTime)).SetEase(Ease.InOutSine);
        sequence.Join(UITrans.DOAnchorPosY(UITrans.anchoredPosition.y + moveDistance + moveDistance, fadeOutTime));

        // 动画结束回调
        sequence.OnComplete(() =>
        {
            //如果有回调方法的话，就执行（默认没有）
            action?.Invoke();
            
            Debug.Log("动画播放完毕！");
            //清除UI
            GameObject.Destroy(UITrans.gameObject);
        });

        //播放效果
        sequence.Play();
    }

    /// <summary>
    /// 闪烁文字
    /// </summary>
    /// <param name="orignalColor"></param>
    /// <param name="flashColor"></param>
    /// <param name="stayTime"></param>
    /// <param name="flashCount"></param>
    public static void FlashText(Text textUI, Color orignalColor, Color flashColor, float stayTime, int flashCount)
    {
        //先杀死之前正在播放的动画
        DOTween.Kill(textUI);
        //记录原始颜色
        Color originColor = orignalColor;

        Sequence flashSequence = DOTween.Sequence();

        //闪红特效
        for (int i = 0; i < flashCount; i++)
        {
            // 瞬间变红
            flashSequence.AppendCallback(() => textUI.color = flashColor);
            flashSequence.AppendInterval(stayTime); // 保持红色一段时间

            // 瞬间恢复
            flashSequence.AppendCallback(() => textUI.color = originColor);
            flashSequence.AppendInterval(stayTime); // 保持原色一段时间
        }

        //记录动画播放记录
        flashSequence.SetId(textUI);
        //播放闪红特效
        flashSequence.Play();
    }

    /// <summary>
    /// 摄像机抖动
    /// </summary>
    public static void CameraShake(Transform cameraTrans, float duration = 0.2f, float strength = 5f, int vibrato = 10, float randomness = 90f, bool snapping = false)
    {
        if (cameraTrans == null)
        {
            Debug.Log("要抖动的摄像机不存在");
            return;
        }

        // 使用 DOShakePosition 实现位置抖动
        cameraTrans.DOShakePosition(
            duration: duration,
            strength: strength,
            vibrato: vibrato,
            randomness: randomness,
            snapping: snapping
        );
        
    }

}
