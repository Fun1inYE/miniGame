using UnityEngine;

/// <summary>
/// 单例模式类
/// </summary>
public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    /// <summary>
    /// 类型为T的单例
    /// </summary>
    private static T instance;

    /// <summary>
    /// 用一个Instance接收instance
    /// </summary>
    public static T Instance {get => instance;}

    protected virtual void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = (T)this;
            Debug.Log($"单例{instance.gameObject.name}初始化成功");
        }
    }

    /// <summary>
    /// 查看是否已经生成对应的实例
    /// </summary>
    public static bool IsInitialized
    {
        get { return instance != null; }
    }
}
