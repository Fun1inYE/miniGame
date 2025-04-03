using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RotateAroundToTarget : MonoBehaviour
{
    /// <summary>
    /// 要围着旋转的目标
    /// </summary>
    private Transform target;

    /// <summary>
    /// 武器数量
    /// </summary>
    public int maxCount;

    /// <summary>
    /// 武器旋转速度
    /// </summary>
    public float speed;

    /// <summary>
    /// 旋转半径
    /// </summary>
    public float radius;

    private void Awake()
    {
        target = FindAndMoveObject.FindFromFirstLayer("Player").transform;
    }

    private void LateUpdate()
    {
        // 围绕中心点旋转
        transform.RotateAround(target.position, Vector3.forward, speed * Time.deltaTime);
    }
}
