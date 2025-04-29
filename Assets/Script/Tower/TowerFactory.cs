using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 防御塔的类别
/// </summary>
public enum TowerType
{
    Null, MagicTower
}

/// <summary>
/// 建筑塔的工厂类
/// </summary>
public static class TowerFactory
{
    /// <summary>
    /// 根据传入防御塔来创建一个防御塔的GameObject
    /// </summary>
    /// <param name="towerType"></param>
    /// <returns></returns>
    public static GameObject CreateATowerWithTowerType(TowerType towerType)
    {
        switch(towerType)
        {
            case TowerType.MagicTower:
                return CreateAMagicTower();
            default:
                Debug.LogError("传入了一个未知的防御塔！");
                return null;
        }
    }

    /// <summary>
    /// 创建一个魔法塔
    /// </summary>
    /// <returns></returns>
    public static GameObject CreateAMagicTower()
    {
        string prefabPath = "Prefab/Tower/MagicTower";
        //首先要获取到没有修饰过的GameObject
        GameObject go = Resources.Load<GameObject>(prefabPath);

        MagicTower magicTower = ComponentFinder.GetOrAddComponent<MagicTower>(go);
        magicTower.towerType = TowerType.MagicTower;

        //创建一个魔法塔的数据
        MagicTowerData data = new MagicTowerData("MagicTower", "普通的魔法塔", 350f, 2f, 100, 5, 0f, 50f, 1080f, true);

        MagicTowerSearchAndLaunch magicTowerSearchAndLaunch = ComponentFinder.GetOrAddComponent<MagicTowerSearchAndLaunch>(go);
        magicTowerSearchAndLaunch.range = data.Range;
        magicTowerSearchAndLaunch.frequency = data.Frequency;

        YPositionSorting yPositionSorting_Nou = ComponentFinder.GetOrAddComponent<YPositionSorting>(go);

        string bulletPrefabPath = "Prefab/TowerBullet/MagicTowerBullet";
        //首先要获取到没有修饰过的GameObject
        GameObject bulletGo = Resources.Load<GameObject>(bulletPrefabPath);

        MagicTowerBulletMove magicTowerBulletMove = ComponentFinder.GetOrAddComponent<MagicTowerBulletMove>(bulletGo);
        magicTowerBulletMove.speed = data.BulletSpeed;
        magicTowerBulletMove.effectiveRange = data.EffectiveRange;
        magicTowerBulletMove.canTrack = data.CanTrack;

        MagicTowerBulletAttack magicTowerBulletAttack = ComponentFinder.GetOrAddComponent<MagicTowerBulletAttack>(bulletGo);
        magicTowerBulletAttack.attack = data.Attack;
        magicTowerBulletAttack.back = data.Back;
        
        //给魔法塔赋予子弹
        magicTowerSearchAndLaunch.bulletPrefab = bulletGo;

        YPositionSorting yPositionSorting_Sub = ComponentFinder.GetOrAddComponent<YPositionSorting>(bulletGo);

        Debug.Log("完成塔的脚本逻辑");

        return go;
    }
}
