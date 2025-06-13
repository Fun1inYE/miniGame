using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

/// <summary>
/// 全局信息管理器
/// </summary>
public class MessageManager : Singleton<MessageManager>
{
    /// <summary>
    /// 信息通知管理字典
    /// </summary>
    private Dictionary<string, IMessageData> actionDic;

    /// <summary>
    /// 脚本初始化
    /// </summary>
    protected override void Awake()
    {
        base.Awake();
        //给字典初始化
        actionDic = new Dictionary<string, IMessageData>();
        //单例初始化
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// 通过名字寻找对应的MessageData类，然后读取其中的事件
    /// </summary>
    /// <typeparam name="T">数据类型</typeparam>
    /// <param name="name">要寻找事件的名字</param>
    /// <param name="action">要向对应事件中添加的方法</param>
    public void AddFunctionInAction<T>(string name, UnityAction<T> action)
    {
        //如果信息管理器包含对应的Action,并且传进来的方法属于MessageData
        if(actionDic.TryGetValue(name, out var previousAction))
        {
            if(previousAction is MessageData<T> messageData)
            {
                messageData.MessageEvent += action;
            }
        }
        else
        {
            actionDic.Add(name, new MessageData<T>(action));
        }
    }

    /// <summary>
    /// 通过名字寻找对应的MessageData类，然后读取其中的事件
    /// </summary>
    /// <typeparam name="T">数据类型</typeparam>
    /// <param name="name">要寻找事件的名字</param>
    /// <param name="action">要向对应事件中添加的方法</param>
    public void AddFunctionInAction<T, U>(string name, UnityAction<T, U> action)
    {
        //如果信息管理器包含对应的Action,并且传进来的方法属于MessageData
        if(actionDic.TryGetValue(name, out var previousAction))
        {
            if(previousAction is MessageData<T, U> messageData)
            {
                messageData.MessageEvent += action;
            }
        }
        else
        {
            actionDic.Add(name, new MessageData<T, U>(action));
        }
    }

    /// <summary>
    /// 重写方法
    /// </summary>
    /// <param name="name"></param>
    /// <param name="action"></param>
    public void AddFunctionInAction(string name, UnityAction action)
    {
        if(actionDic.TryGetValue(name, out var previousAction))
        {
            if(previousAction is MessageData messageData)
            {
                messageData.MessageEvent += action;
            }
        }
        else
        {
            actionDic.Add(name, new MessageData(action));
        }
    }

    /// <summary>
    /// 从对应的MessageData中的事件移除对应的方法
    /// </summary>
    /// <typeparam name="T">数据类型</typeparam>
    /// <param name="name">要寻找事件的名字</param>
    /// <param name="action">要向对应事件中添加的方法</param>
    public void RemoveFunctionInAction<T>(string name, UnityAction<T> action)
    {
        //如果字典中包含这个名字的话
        if(actionDic.TryGetValue(name, out var previousAction))
        {
            if(previousAction is MessageData<T> messageData)
            {
                messageData.MessageEvent -= action;
                //检测委托字典中有没有空委托，如果有就删除
                if(messageData.MessageEvent == null)
                {
                    actionDic.Remove(name);
                }
            }
        }
    }

    /// <summary>
    /// 从对应的MessageData中的事件移除对应的方法
    /// </summary>
    /// <typeparam name="T">数据类型</typeparam>
    /// <typeparam name="U">数据类型</typeparam>
    /// <param name="name">要寻找事件的名字</param>
    /// <param name="action">要向对应事件中添加的方法</param>
    public void RemoveFunctionInAction<T, U>(string name, UnityAction<T, U> action)
    {
        //如果字典中包含这个名字的话
        if(actionDic.TryGetValue(name, out var previousAction))
        {
            if(previousAction is MessageData<T, U> messageData)
            {
                messageData.MessageEvent -= action;
                //检测委托字典中有没有空委托，如果有就删除
                if(messageData.MessageEvent == null)
                {
                    actionDic.Remove(name);
                }
            }
        }
    }

    /// <summary>
    /// 重写方法
    /// </summary>
    /// <param name="name"></param>
    /// <param name="action"></param>
    public void RemoveFunctionInAction(string name, UnityAction action)
    {
        if(actionDic.TryGetValue(name, out var previousAction))
        {
            if(previousAction is MessageData messageData)
            {
                messageData.MessageEvent -= action;
            }
        }
    }

    /// <summary>
    /// 执行name对应的事件中的所有方法
    /// </summary>
    /// <typeparam name="T">数据类型</typeparam>
    /// <param name="name">对应的事件中的方法</param>
    /// <param name="data">要传递的数据</param>
    public void Send<T>(string name, T data)
    {
        if(actionDic.TryGetValue(name, out var previousAction))
        {
            (previousAction as MessageData<T>)?.MessageEvent.Invoke(data);
        }
    }

    /// <summary>
    /// 重写方法
    /// </summary>
    /// <param name="name"></param>
    /// <param name="action"></param>
    public void Send<T, U>(string name, T data1, U data2)
    {
        if(actionDic.TryGetValue(name, out var previousAction))
        {
            (previousAction as MessageData<T, U>)?.MessageEvent.Invoke(data1, data2);
        }
    }

    /// <summary>
    /// 重写方法
    /// </summary>
    /// <param name="name"></param>
    public void Send(string name)
    {
        if(actionDic.TryGetValue(name, out var previousAction))
        {
            (previousAction as MessageData)?.MessageEvent.Invoke();
        }
    }

    /// <summary>
    /// 清除字典中所有委托
    /// </summary>
    public void ClearAllAction()
    {
        actionDic.Clear();
    }
}
