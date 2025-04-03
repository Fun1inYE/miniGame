using UnityEngine.Events;

/// <summary>
/// 在消息管理中心的信息基类接口
/// </summary>
public interface IMessageData {}

/// <summary>
/// 继承自IMessageData，要传达的信息数据类（有泛型）
/// </summary>
public class MessageData<T> : IMessageData
{
    /// <summary>
    /// 传递信息的事件（泛型）
    /// </summary>
    private UnityAction<T> messageEvent;

    /// <summary>
    /// 属性的封装
    /// </summary>
    public UnityAction<T> MessageEvent { get => messageEvent; set => messageEvent = value; }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="action">传进来的方法的泛型参数类型</param>
    public MessageData(UnityAction<T> action)
    {
        messageEvent += action;
    }
}

/// <summary>
/// 继承自IMessageData，要传达的信息数据类（有两个泛型）
/// </summary>
public class MessageData<T, U> : IMessageData
{
    /// <summary>
    /// 传递信息的事件（泛型）
    /// </summary>
    private UnityAction<T, U> messageEvent;
    /// <summary>
    /// 属性的封装
    /// </summary>
    public UnityAction<T, U> MessageEvent { get => messageEvent; set => messageEvent = value; }
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="action">传进来的方法的泛型参数类型</param>
    public MessageData(UnityAction<T, U> action)
    {
        messageEvent += action;
    }
}

/// <summary>
/// 继承自IMessageData，要传达的信息数据类（无泛型）
/// </summary>
public class MessageData : IMessageData
{
    /// <summary>
    /// 传递信息的事件（无泛型）
    /// </summary>
    private UnityAction messageEvent;

    /// <summary>
    /// 属性封装
    /// </summary>
    public UnityAction MessageEvent { get => messageEvent; set => messageEvent = value; }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="action">传进来的方法</param>
    public MessageData(UnityAction action)
    {
        messageEvent += action;
    }
}