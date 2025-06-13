using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 根据y轴排列贴图的sortingLayer的逻辑
/// </summary>
public class YPositionSorting : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    /// <summary>
    /// 上一次的位置
    /// </summary>
    private float lastYPosition;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if(spriteRenderer == null)
        {
            Debug.LogError($"{gameObject.name}上没有spriteRenderer组件");
        }
        // 初始化时立即更新一次
        UpdateSortingOrder(); 
    }

    private void LateUpdate()
    {
        // 仅在Y位置变化时更新
        if (transform.position.y != lastYPosition)
        {
            UpdateSortingOrder();
        }
    }

    /// <summary>
    /// 通过y坐标计算
    /// </summary>
    private void UpdateSortingOrder()
    {
        // 计算底部Y坐标（根据中心点向下偏移精灵高度的一半）
        float bottomY = transform.position.y - spriteRenderer.bounds.extents.y;
        
        // 动态调整Sorting Order，确保Y越高数值越小
        spriteRenderer.sortingOrder = Mathf.FloorToInt(-bottomY * 10);

        lastYPosition = transform.position.y;
    }
}
