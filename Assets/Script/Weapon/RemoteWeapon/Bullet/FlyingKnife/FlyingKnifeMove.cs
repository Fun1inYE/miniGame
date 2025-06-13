using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 飞刀移动逻辑
/// </summary>
public class FlyingKnifeMove : BulletMove
{
    protected override void CheckLoseEfficacy()
    {
        if(effectiveRange != float.MaxValue)
        {
            //计算子弹的飞行距离，判断超没超过有效距离
            if(Vector2.Distance(generatePos, transform.position) > effectiveRange)
            {
                //获取到飞刀的其他逻辑
                FlyingKnife flyingKnife = GetComponent<FlyingKnife>();
                //开启协程
                StartCoroutine(flyingKnife.DestroyAfterTrailFinished());
            }
        }
        else
        {
            //如果子弹飞出屏幕，就销毁子弹
            if(!JudgmentPoint.IsInScreen(transform.position))
            {
                //获取到飞刀的其他逻辑
                FlyingKnife flyingKnife = GetComponent<FlyingKnife>();
                //开启协程
                StartCoroutine(flyingKnife.DestroyAfterTrailFinished());
            }
        }
    }
}
