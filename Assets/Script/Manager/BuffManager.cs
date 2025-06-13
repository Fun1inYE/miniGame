using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Buff管理器
/// </summary>
public class BuffManager : Singleton<BuffManager>
{
    /// <summary>
    /// 玩家buff引用
    /// </summary>
    public PlayerBuffLevelData buffData;

    protected override void Awake()
    {
        base.Awake();
        //初始化buff
        InitBuffData();
    }

    /// <summary>
    /// 初始化buff的方法
    /// </summary>
    private void InitBuffData()
    {
        //读取buff数据
        buffData = LoadBuffData();
    }

    /// <summary>
    /// 通过外部主动保存的方法
    /// </summary>
    public void Save()
    {
        SaveBuffData();
    }

    public void DeletAllData()
    {
        RemoveBuffData();
    }

    private void OnDestroy()
    {

    }

    #region 读取与保存

    /// <summary>
    /// 读取buff数据
    /// </summary>
    private PlayerBuffLevelData LoadBuffData()
    {
        //直接进行读取
        PlayerBuffLevelData buffData = new PlayerBuffLevelData(
            SaveWithPlayerPref.LoadByPlayerPrefs<int>(SaveNameDefine.Player_Hp_Buff_Level),
            SaveWithPlayerPref.LoadByPlayerPrefs<int>(SaveNameDefine.Player_Attack_Buff_Level),
            SaveWithPlayerPref.LoadByPlayerPrefs<int>(SaveNameDefine.Player_Speed_Buff_Level),
            SaveWithPlayerPref.LoadByPlayerPrefs<int>(SaveNameDefine.Player_AttackSpeed_Buff_Level)
        );
        //返回buff的数据
        return buffData;
    }

    /// <summary>
    /// 保存buff数据
    /// </summary>
    private void SaveBuffData()
    {
        SaveWithPlayerPref.SaveByPlayerPrefs(SaveNameDefine.Player_Hp_Buff_Level, buffData.HpLevel);
        SaveWithPlayerPref.SaveByPlayerPrefs(SaveNameDefine.Player_Attack_Buff_Level, buffData.AttackLevel);
        SaveWithPlayerPref.SaveByPlayerPrefs(SaveNameDefine.Player_Speed_Buff_Level, buffData.SpeedLevel);
        SaveWithPlayerPref.SaveByPlayerPrefs(SaveNameDefine.Player_AttackSpeed_Buff_Level, buffData.AttackSpeedLevel);
    }

    /// <summary>
    /// 清除buff数据
    /// </summary>
    private void RemoveBuffData()
    {
        if (PlayerPrefs.HasKey(SaveNameDefine.Player_Hp_Buff_Level))
        {
            PlayerPrefs.DeleteKey(SaveNameDefine.Player_Hp_Buff_Level);
        }
        if (PlayerPrefs.HasKey(SaveNameDefine.Player_Attack_Buff_Level))
        {
            PlayerPrefs.DeleteKey(SaveNameDefine.Player_Attack_Buff_Level);
        }
        if (PlayerPrefs.HasKey(SaveNameDefine.Player_Speed_Buff_Level))
        {
            PlayerPrefs.DeleteKey(SaveNameDefine.Player_Speed_Buff_Level);
        }
        if (PlayerPrefs.HasKey(SaveNameDefine.Player_AttackSpeed_Buff_Level))
        {
            PlayerPrefs.DeleteKey(SaveNameDefine.Player_AttackSpeed_Buff_Level);
        }
    }

    #endregion
}
