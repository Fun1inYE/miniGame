using UnityEngine;

/// <summary>
/// 塔的逻辑
/// </summary>
public class Tower : MonoBehaviour
{
    /// <summary>
    /// 塔的类别
    /// </summary>
    public TowerType towerType;

    /// <summary>
    /// 判断是否可以建造(默认可以建造)
    /// </summary>
    public bool canbuild = true;

    /// <summary>
    /// 判断是塔是否建造完了
    /// </summary>
    public bool built = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //只要跟玩家之外的东西发生重合就变状态
        if(!collision.CompareTag("Player"))
        {
            canbuild = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //只要跟玩家之外的东西发生碰撞就变状态
        if(!collision.CompareTag("Player"))
        {
            canbuild = true;
        }
    }
}
