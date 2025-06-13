using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 宝箱的奖励逻辑
/// </summary>
public class TreasureChestReward : MonoBehaviour
{
    /// <summary>
    /// 宝箱拥有的蓝宝石数量
    /// </summary>
    public int sapphire;

    /// <summary>
    /// 是否被打开过
    /// </summary>
    private bool hasOpened = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !hasOpened)
        {
            //宝箱被打开过了
            hasOpened = true;

            //显示文字提示
            DisplayGemTip();
            //向EconomyManager传递获得的蓝宝石数据
            EconomyManager.Instance.ChangeSapphire(sapphire);
            //向总结管理器发送获得蓝宝石数据
            MessageManager.Instance.Send<int>(MessageDefine.ADD_GET_SAPPHIRE_COUNT, sapphire);
        }
    }

    private void DisplayGemTip()
    {
        //获取到TipCanvas
        Transform tipCanvas = FindAndMoveObject.FindFromFirstLayer("TipCanvas").transform;
        //获取到TipCanvas的RectTranform
        RectTransform tipCanvasRectTransform = ComponentFinder.GetOrAddComponent<RectTransform>(tipCanvas.gameObject);
        //确定宝箱位置
        Vector2 chestScreenPos = Camera.main.WorldToScreenPoint(transform.position);

        // 关键步骤：将屏幕坐标转换为 Canvas 的本地坐标
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            tipCanvasRectTransform,
            chestScreenPos,
            Camera.main,
            out Vector2 localPos
        );

        //生成tip
        GameObject tip = Instantiate(TipFloatFactory.CreateAChestRewardTip(sapphire.ToString()), tipCanvas);

        //获取到tip的RectTransform
        RectTransform tipRectTrans = tip.GetComponent<RectTransform>();

        //将UI位置调到localPos
        tipRectTrans.anchoredPosition = localPos;   
        
        DisplayWithDoTween.MoveAndFadeWithUI(tipRectTrans, 0.5f, 1f, 0.5f, 60f);
    }
}
