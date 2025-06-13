using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureChestManager : Singleton<TreasureChestManager>
{
    private List<GameObject> chestList = new List<GameObject>();

    private void Start()
    {
        MessageManager.Instance.AddFunctionInAction(MessageDefine.BACK_TO_MAINMENU, RemoveAllChest);
    }

    /// <summary>
    /// 添加一个宝箱
    /// </summary>
    public void RegisterChest(GameObject chest)
    {
        chestList.Add(chest);
    }

    /// <summary>
    /// 消除一个宝箱的方法
    /// </summary>
    /// <param name="chest"></param>
    public void UnregisterChest(GameObject chest)
    {
        chestList.Remove(chest);
    }

    /// <summary>
    /// 消除所有塔的方法
    /// </summary>
    public void RemoveAllChest()
    {
        foreach (GameObject chest in chestList)
        {
            Destroy(chest);
        }
        //清除列表
        chestList.Clear();
    }

    private void OnDisable()
    {
        MessageManager.Instance.RemoveFunctionInAction(MessageDefine.BACK_TO_MAINMENU, RemoveAllChest);
    }
}
