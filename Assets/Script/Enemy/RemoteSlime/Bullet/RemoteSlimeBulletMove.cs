using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 远程史莱姆子弹的逻辑
/// </summary>
public class RemoteSlimeBulletMove : BulletMove
{
    public override void AimAndFlyToTarget()
    {
        if(target == null)
        {
            Debug.Log("目标丢失");
            return;
        }

        //只有当追踪锁关闭的时候才会计算方向
        if(!trackLock)
        {
            //计算目标的方向
            Vector2 dir = (target.transform.position - transform.position).normalized;
            //这个子弹没有追踪功能
            if(!canTrack)
            {
                //打开跟踪锁
                trackLock = true;
            }

            /// 计算目标方向与 Vector3.up 之间的夹角
            float angle = Vector2.SignedAngle(Vector2.up, dir);
            // 创建旋转
            Quaternion rotation = Quaternion.Euler(0, 0, angle);
            // 应用旋转
            transform.rotation = rotation;

            //给子弹速度
            rb.velocity = dir * speed;
        }
    }
}