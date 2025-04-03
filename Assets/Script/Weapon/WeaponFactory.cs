using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 武器攻击方式
/// </summary>
public enum AttackType { Melee, Remote }

/// <summary>
/// 武器类型
/// </summary>
public enum WeaponType { Sword, Axe, FlyingKnife}

/// <summary>
/// 武器工厂类
/// </summary>
public static class WeaponFactory
{
    //返回一个完整的木剑的GameObject
    public static GameObject CreateAWoodSword()
    {
        //预制体的路径
        string prefabPath = "Prefab/Weapon/MeleeWeapon/sword_[wood]";
        // 加载预制体
        GameObject prefab = Resources.Load<GameObject>(prefabPath);
        //添加碰撞箱体
        BoxCollider2D col = ComponentFinder.GetOrAddComponent<BoxCollider2D>(prefab);
        col.isTrigger = true;

        //创建一个木剑的数据
        MeleeWeaponData data = new WoodSwordData("woodSword", "普通的木剑", 1, 1, AttackType.Melee, WeaponType.Sword, 30f, 200f, 10);

        //给木剑赋值
        RotateAroundToTarget rotateAroundToTarget = ComponentFinder.GetOrAddComponent<RotateAroundToTarget>(prefab);
        rotateAroundToTarget.speed = data.Speed;
        rotateAroundToTarget.radius = data.Radius;
        rotateAroundToTarget.maxCount = data.MaxCount;
        
        WoodSwordAttack woodSwordAttack = ComponentFinder.GetOrAddComponent<WoodSwordAttack>(prefab);
        woodSwordAttack.attack = data.Attack;
        woodSwordAttack.back = data.Back;
        //附上武器的其他逻辑
        WoodSword woodSword = ComponentFinder.GetOrAddComponent<WoodSword>(prefab);
        woodSword.attackType = data.AttackType;
        woodSword.weaponType = data.WeaponType;
        woodSword.weaponName = data.Name;
        woodSword.description = data.Description;

        return prefab;
    }

    //返回一个完整的铁斧的GameObject
    public static GameObject CreateAIronAxe()
    {
        //预制体的路径
        string prefabPath = "Prefab/Weapon/MeleeWeapon/axe_[iron]";
        // 加载预制体
        GameObject prefab = Resources.Load<GameObject>(prefabPath);
        //添加碰撞箱体
        BoxCollider2D col = ComponentFinder.GetOrAddComponent<BoxCollider2D>(prefab);
        col.isTrigger = true;

        //创建一个铁斧的数据
        MeleeWeaponData data = new IronAxeData("ironAxe", "普通的铁斧", 20, 10, AttackType.Melee, WeaponType.Axe, 50, 100f, 5);

        RotateAroundToTarget rotateAroundToTarget = ComponentFinder.GetOrAddComponent<RotateAroundToTarget>(prefab);
        rotateAroundToTarget.speed = data.Speed;
        rotateAroundToTarget.radius = data.Radius;
        rotateAroundToTarget.maxCount = data.MaxCount;
        
        IronAxeAttack ironAxeAttack = ComponentFinder.GetOrAddComponent<IronAxeAttack>(prefab);
        //给木剑赋值
        ironAxeAttack.attack = data.Attack;
        ironAxeAttack.back = data.Back;
        //附上武器的其他逻辑
        IronAxe ironAxe = ComponentFinder.GetOrAddComponent<IronAxe>(prefab);
        ironAxe.attackType = data.AttackType;
        ironAxe.weaponType = data.WeaponType;
        ironAxe.weaponName = data.Name;
        ironAxe.description = data.Description;

        return prefab;
    }

    /// <summary>
    /// 创建一个飞刀发射器
    /// </summary>
    /// <returns></returns>
    public static GameObject CreateAFlyingKnifeEmitter()
    {
        //预制体的路径
        string emitterPrefabPath = "Prefab/Weapon/RemoteWeapon/Emitter/flying_knife_emitter";

        //加载一个飞刀的发射器的GameObject
        GameObject emitterPrefab = Resources.Load<GameObject>(emitterPrefabPath);

        //创建一个飞刀发射器的数据
        RemoteWeaponData data = new FlyingKnifeData("flyingKnifeEmitter", "普通的飞刀发射器", 2, 0, AttackType.Remote, WeaponType.FlyingKnife, float.MaxValue, float.MaxValue, 1f, 120f, false);

        //添加飞刀发射器的搜索和发射的逻辑脚本
        FlyingKnifeEmitterSearchAndLaunch emitterSearchAndLaunch = ComponentFinder.GetOrAddComponent<FlyingKnifeEmitterSearchAndLaunch>(emitterPrefab);
        emitterSearchAndLaunch.range = data.Range;
        emitterSearchAndLaunch.frequency = data.AttackFrequency;

        //附上飞刀发射器的其他逻辑
        FlyinKnifeEmitter flyinKnifeEmitter = ComponentFinder.GetOrAddComponent<FlyinKnifeEmitter>(emitterPrefab);
        flyinKnifeEmitter.weaponName = data.Name;
        flyinKnifeEmitter.description = data.Description;
        flyinKnifeEmitter.attackType = data.AttackType;
        flyinKnifeEmitter.weaponType = data.WeaponType;

        //创建一个飞刀的数据
        string bulletPrefabPath = "Prefab/Weapon/RemoteWeapon/Bullet/flying_knife";
        //加载一个飞刀的预制体
        GameObject bulletPrefab = Resources.Load<GameObject>(bulletPrefabPath);
        //给预制体添加组件
        FlyingKnifeAttack flyingKnifeAttack = ComponentFinder.GetOrAddComponent<FlyingKnifeAttack>(bulletPrefab);
        flyingKnifeAttack.attack = data.Attack;
        flyingKnifeAttack.back = data.Back;

        FlyingKnifeMove flyingKnifeMove = ComponentFinder.GetOrAddComponent<FlyingKnifeMove>(bulletPrefab);
        flyingKnifeMove.speed = data.Speed;
        flyingKnifeMove.effectiveRange = data.EffectiveRange;
        flyingKnifeMove.canTrack = data.CanTrack;
        //绑定发射器的子弹预制体
        emitterSearchAndLaunch.bulletPrefab = bulletPrefab;

        return emitterPrefab;

    }

    public static void Test()
    {
        //预制体的路径
        string prefabPath = "Prefab/Weapon/MeleeWeapon/axe_[iron]";
        // 加载预制体
        GameObject prefab = Resources.Load<GameObject>(prefabPath);
        //添加碰撞箱体
        BoxCollider2D col = ComponentFinder.GetOrAddComponent<BoxCollider2D>(prefab);
        col.isTrigger = true;

        //创建一个铁斧的数据
        MeleeWeaponData data = new IronAxeData("ironAxe", "普通的铁斧", 20, 10, AttackType.Melee, WeaponType.Axe, 50, 100f, 5);

        RotateAroundToTarget rotateAroundToTarget = ComponentFinder.GetOrAddComponent<RotateAroundToTarget>(prefab);
    }
}
