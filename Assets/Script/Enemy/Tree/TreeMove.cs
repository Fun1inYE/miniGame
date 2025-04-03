using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 树桩怪的移动逻辑
/// </summary>
public class TreeMove : EnemyMove
{
   protected override void MoveToTarget()
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
            rb.velocity = Vector2.Lerp(rb.velocity, direction * speed, 0.07f);

            //判断当前速度等不等于怪物满速
            if(rb.velocity == direction * speed)
            {
                //退出击退状态
                backing = false;
            }
        }
    }
}
