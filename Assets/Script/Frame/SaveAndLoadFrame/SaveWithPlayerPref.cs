using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 通过PlayerPrefs存储游戏数据的类
/// </summary>
public static class SaveWithPlayerPref
{
    #region PlayerPrefs

    /// <summary>
    /// 通过PlayerPrefs进行存储数据
    /// </summary>
    /// <param name="key">数据的名字</param>
    /// <param name="value">数据</param>
    public static void SaveByPlayerPrefs(string key, object value)
    {
        //如果数据类型为int
        if(value is int)
        {
            PlayerPrefs.SetInt(key, (int)value);
        }
        else if(value is float)
        {
            PlayerPrefs.SetFloat(key, (float)value);
        }
        else if(value is string)
        {
            PlayerPrefs.SetString(key, (string)value);
        }
        else
        {
            Debug.LogError("请传入int, float, string类型数值");
        }

        //通过PlayerPrefs保存数据
        PlayerPrefs.Save();
    }

    /// <summary>
    /// 通过对应键读取对应数值
    /// </summary>
    /// <typeparam name="T">尧都区数值的类型</typeparam>
    /// <param name="key">对应的键</param>
    /// <param name="defaultValue">默认值</param>
    /// <returns>返回读取出来的值</returns>
    public static T LoadByPlayerPrefs<T>(string key)
    {
        //检查存不存在这个键
        if(!PlayerPrefs.HasKey(key))
        {
            Debug.LogWarning($"不存在 {key} 的键");
            return default;
        }

        //以下检查T为什么类型
        if(typeof(T) == typeof(int))
        {
            return (T)(object)PlayerPrefs.GetInt(key);
        }
        else if(typeof(T) == typeof(float))
        {
            return (T)(object)PlayerPrefs.GetFloat(key);
        }
        else if(typeof(T) == typeof(string))
        {
            return (T)(object)PlayerPrefs.GetString(key);
        }
        else
        {
            Debug.LogError("请读取int float string值, PlayerPrefs只支持这三种数值存储");
            return default;
        }
    }
    #endregion
}
