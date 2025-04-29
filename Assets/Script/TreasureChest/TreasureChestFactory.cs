using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 宝箱的工厂类
/// </summary>
public static class TreasureChestFactory
{
    /// <summary>
    /// 按照endPos的位置, 和蓝宝石的数量生成一个宝箱
    /// </summary>
    /// <param name="endPos"></param>
    /// <returns></returns>
    public static GameObject CreateAChest(Vector2 endPos, int sapphire)
    {
        string prefabPath = "Prefab/TreasureChest/NormalTreasureChest";
        GameObject go = Resources.Load<GameObject>(prefabPath);

        TreasureChest treasureChest = ComponentFinder.GetOrAddComponent<TreasureChest>(go);
        treasureChest.sapphire = sapphire;

        TreasureChestAnimation treasureChestAnimation = ComponentFinder.GetOrAddComponent<TreasureChestAnimation>(go);
        treasureChestAnimation.endPos = endPos;

        YPositionSorting yPositionSorting_Nou = ComponentFinder.GetOrAddComponent<YPositionSorting>(go);

        return go;
    }   
}
