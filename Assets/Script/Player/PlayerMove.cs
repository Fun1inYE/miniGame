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

    private bool canMove = false;

    /// <summary>
    /// 掌握玩家移动方向的变量
    /// </summary>
    private Vector2 moveDir;

    public Vector2 MoveDir { get => moveDir; set => moveDir = value; }

    /// <summary>
    /// 玩家移动速度
    /// </summary>
    public float speed;

    private float orignalspeed;

    public Rigidbody2D rb;

    private void Awake()
    {
        //检测脚本
        if (joystick == null)
        {
            Debug.LogError("摇杆是空的");
        }
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D是空的");
            return;
        }
    }

    private void Start()
    {
        MessageManager.Instance.AddFunctionInAction(MessageDefine.GAME_START, GameStart);
        MessageManager.Instance.AddFunctionInAction(MessageDefine.GAME_OVER, GameOver);

        //摇杆默认不允许使用
        joystick.gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
        orignalspeed = speed;
    }

    private void Update()
    {
        //判断是否可以移动
        if (canMove)
        {
            moveDir = new Vector2(joystick.Horizontal, joystick.Vertical);
            //通过外面的摇杆更改人物移动的方向
            rb.velocity = moveDir * speed;
        }
    }

    /// <summary>
    /// 游戏开始执行的方法
    /// </summary>
    public void GameStart()
    {
        //可以移动
        canMove = true;
        //允许摇杆使用
        joystick.gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
        //计算行动速度
        speed = orignalspeed + (BuffManager.Instance.buffData.SpeedLevel * PlayerBuffDataMultiplier.multi_Speed);
    }

    public void GameOver()
    {
        //不能移动
        canMove = false;
        //不允许点击摇杆
        joystick.gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
        joystick.gameObject.SetActive(false);
        joystick.gameObject.SetActive(true);
        //将玩家速度置零
        rb.velocity = Vector2.zero;
    }

}
