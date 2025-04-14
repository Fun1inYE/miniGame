using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 远程史莱姆移动逻辑
/// </summary>
public class RemoteSlimeMove : EnemyMove
{
    /// <summary>
    /// 判断远程史莱姆是否可以移动
    /// </summary>
    public bool canMove;

    protected override void OnEnable()
    {
        base.OnEnable();
        canMove = true;
    }

    /// <summary>
    /// 重写
    /// </summary>
    protected override void MoveToTarget()
    {   
        if(!canMove)
        {
            //速度变为0
            rb.velocity = Vector2.zero;
            return;
        } 
        base.MoveToTarget();
    }
}
