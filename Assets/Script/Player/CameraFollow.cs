using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// 摄像机跟随类
/// </summary>
public class CameraFollow : MonoBehaviour
{
    /// <summary>
    /// 要跟随的对象
    /// </summary>
    public GameObject followTarget;
    /// <summary>
    /// 摄像机（默认挂载主摄像机）
    /// </summary>
    public Camera cam;

    private void Start()
    {
        //如果没有绑定摄像机的话就默认绑定主摄像机
        if(cam == null)
        {
            cam = Camera.main;
        }
    }

    private void LateUpdate()
    {
        FollowTarget();
    }

    /// <summary>
    /// 摄像机跟随目标做插值移动
    /// </summary>
    private void FollowTarget()
    {
        if(followTarget == null)
        {
            Debug.LogError("没有跟随的目标");
            return;
        }

        //获取目标位置
        Vector3 newPosition = new Vector3(followTarget.transform.position.x, followTarget.transform.position.y, -10);
        //将位置赋予给摄像机(插值)
        cam.transform.position = Vector3.Lerp(cam.transform.position, newPosition, Time.deltaTime * 5f);
    }
}
