using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 宝箱的工厂类
/// </summary>
public static class TreasureChestFactory
{
    /// <summary>
    /// 根据类型生成宝箱类型
    /// </summary>
    /// <param name="chestType"></param>
    /// <param name="endPos"></param>
    /// <param name="sapphire"></param>
    /// <returns></returns>
    public static GameObject CreateAChest(ChestType chestType, Vector2 endPos, int sapphire)
    {
        switch(chestType)
        {
            case ChestType.Normal:
                return CreateANormalChest(endPos, sapphire);
            case ChestType.Gold:
                return CreateAGoldChest(endPos, sapphire);
            default:
                Debug.LogWarning("没有找到对应的宝箱类型");
                return null;
        }
    }

    /// <summary>
    /// 按照endPos的位置, 和蓝宝石的数量生成一个宝箱
    /// </summary>
    /// <param name="endPos"></param>
    /// <returns></returns>
    public static GameObject CreateANormalChest(Vector2 endPos, int sapphire)
    {
        string prefabPath = "Prefab/TreasureChest/NormalTreasureChest";
        GameObject go = Resources.Load<GameObject>(prefabPath);

        TreasureChest treasureChest = ComponentFinder.GetOrAddComponent<TreasureChest>(go);
        
        //绑定宝箱奖励逻辑
        TreasureChestReward treasureChestReward = ComponentFinder.GetOrAddComponent<TreasureChestReward>(go);
        //给宝箱传送蓝宝石的数据
        treasureChestReward.sapphire = sapphire;

        TreasureChestAnimation treasureChestAnimation = ComponentFinder.GetOrAddComponent<TreasureChestAnimation>(go);
        treasureChestAnimation.endPos = endPos;

        YPositionSorting yPositionSorting = ComponentFinder.GetOrAddComponent<YPositionSorting>(go);

        return go;
    }

    /// <summary>
    /// 生成一个金色的宝箱
    /// </summary>
    /// <param name="endPos"></param>
    /// <param name="sapphire">宝石的数量</param>
    /// <returns></returns>
    public static GameObject CreateAGoldChest(Vector2 endPos, int sapphire)
    {
        string prefabPath = "Prefab/TreasureChest/GoldTreasureChest";
        GameObject go = Resources.Load<GameObject>(prefabPath);

        TreasureChest treasureChest = ComponentFinder.GetOrAddComponent<TreasureChest>(go);
        
        //绑定宝箱奖励逻辑
        TreasureChestReward treasureChestReward = ComponentFinder.GetOrAddComponent<TreasureChestReward>(go);
        //给宝箱传送蓝宝石的数据
        treasureChestReward.sapphire = sapphire;

        TreasureChestAnimation treasureChestAnimation = ComponentFinder.GetOrAddComponent<TreasureChestAnimation>(go);
        treasureChestAnimation.endPos = endPos;

        YPositionSorting yPositionSorting = ComponentFinder.GetOrAddComponent<YPositionSorting>(go);

        return go;
    }
}
