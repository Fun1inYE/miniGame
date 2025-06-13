using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// 墙体的Y轴上的排序
/// </summary>
public class WallYPositionSorting : MonoBehaviour
{
    /// <summary>
    /// 墙体的SpriteRenderer
    /// </summary>
    private TilemapRenderer tilemapRenderer;

    /// <summary>
    /// 玩家的位置
    /// </summary>
    private SpriteRenderer spriteRenderer;

    /// <summary>
    /// 玩家所处纵坐标是否为正数
    /// </summary>
    private bool playerYisPositive = true;

    private void Awake()
    {
        tilemapRenderer = GetComponent<TilemapRenderer>();
        spriteRenderer = FindAndMoveObject.FindFromFirstLayer("Player").GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        StartCoroutine(GetPlayerPosition());
    }

    private void LateUpdate()
    {
        if (playerYisPositive)
        {
            tilemapRenderer.sortingOrder = spriteRenderer.sortingOrder - 10;
        }
        else
        {
            tilemapRenderer.sortingOrder = spriteRenderer.sortingOrder + 10;
        }
    }

    /// <summary>
    /// 每隔1s检测玩家位置
    /// </summary>
    /// <returns></returns>
    private IEnumerator GetPlayerPosition()
    {
        while (true)
        {
            //获取玩家位置
            Transform playerTrans = FindAndMoveObject.FindFromFirstLayer("Player").transform;
            if (playerTrans.position.y >= 0)
            {
                playerYisPositive = true;
            }
            else
            {
                playerYisPositive = false;
            }

            yield return new WaitForSeconds(1f);
        }
    }
}
