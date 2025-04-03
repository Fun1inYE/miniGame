using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家的脚本
/// </summary>
public class PlayerMove : MonoBehaviour
{
    /// <summary>
    /// 引用摇杆脚本
    /// </summary>
    public Joystick joystick;

    /// <summary>
    /// 掌握玩家移动方向的变量
    /// </summary>
    private Vector2 moveDir;

    public Vector2 MoveDir { get => moveDir; set => moveDir = value; }

    /// <summary>
    /// 玩家移动速度
    /// </summary>
    public float speed;

    public Rigidbody2D rb;

    private void Awake()
    {
        //检测脚本
        if(joystick == null)
        {
            Debug.LogError("摇杆是空的");
        }   
        if(rb == null)
        {
            Debug.LogError("Rigidbody2D是空的");
            return;
        }
    }

    private void Update()
    {
        moveDir = new Vector2(joystick.Horizontal, joystick.Vertical);
        //通过外面的摇杆更改人物移动的方向
        rb.velocity = moveDir * speed;
    } 

}
