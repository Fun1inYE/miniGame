using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

/// <summary>
/// 塔的搜索和发射影响的逻辑
/// </summary>
public class TowerSearchAndLaunch : MonoBehaviour
{
    /// <summary>
    /// 塔的影响范围
    /// </summary>
    public float range;

    /// <summary>
    /// 影响频率
    /// </summary>
    public float frequency;

    /// <summary>
    /// 搜索并且发射的协程
    /// </summary>
    private Coroutine searchAndLaunchCoro;

    /// <summary>
    /// 塔的发射点
    /// </summary>
    protected Transform launchPoint;

    private void OnEnable()
    {
        launchPoint = FindAndMoveObject.FindChildBreadthFirst(transform, "LaunchPoint");
    }

    public void StartSearchAndLaunchCoro()
    {
        searchAndLaunchCoro = StartCoroutine(SearchAndLaunchCoro());
    }

    public void StopSearchAndLaunchCoro()
    {
        StopCoroutine(searchAndLaunchCoro);
    }

    /// <summary>
    /// 搜索和发射（影响）的方法
    /// </summary>
    /// <returns></returns>
    protected virtual IEnumerator SearchAndLaunchCoro()
    {
        Debug.Log("执行的塔父类的发射的协程方法");
        yield return null;
    }

    private void OnDisable()
    {
        StopAllCoroutines();   
    }
}
