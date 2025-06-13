using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 用于接收玩家的基础数值与游戏之间的逻辑关系
/// </summary>
public class Player : MonoBehaviour
{
    /// <summary>
    /// 引用玩家的数值
    /// </summary>
    private PlayerData playerData;

    private void Awake()
    {

    }

    /// <summary>
    /// 初始化玩家基础数值
    /// </summary>
    private void InitPlayerData()
    {
        //TODO：初始化玩家基础数值的操作
        //TODO: 在打开游戏的时候就读取玩家基础数值


    }

    /// <summary>
    /// 当游戏开始的时候进行数值分配
    /// </summary>
    public void StartGameToAllocateData()
    {
        if(playerData == null)
        {
            Debug.Log("玩家数据是空的! 请检查代码!");
            return;
        }
        
        //给玩家分配数值
        PlayerHealth playerHealth = GetComponent<PlayerHealth>();
        playerHealth.health = playerData.Hp;
        PlayerMove playerMove = GetComponent<PlayerMove>();
        playerMove.speed = playerData.Speed;

        
    }
}
