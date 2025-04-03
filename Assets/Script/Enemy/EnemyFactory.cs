using UnityEngine;

/// <summary>
/// 敌人类型
/// </summary>
public enum EnemyType
{
    Null, Slime, Tree, RemoteSlime, Test
}

/// <summary>
/// 敌人生成工厂
/// </summary>
public static class EnemyFactory
{   
    public static GameObject CreateEnemyWithType(EnemyType enmeyType)
    {
        switch(enmeyType)
        {
            case EnemyType.Slime:
                return CreateASlime();
            case EnemyType.Test:
                return CreateATest();
            case EnemyType.Tree:
                return CreateATree();
            case EnemyType.RemoteSlime:
                return CreateARemoteSlime();
            default:
                Debug.LogError("EnemyFactory没有找到对应的敌人类型");
                return null;
        }
    }

    /// <summary>
    /// 创建一个史莱姆
    /// </summary>
    /// <param name="Slime"></param>
    /// <returns></returns>
    public static GameObject CreateASlime()
    {
        string prefabPath = "Prefab/Enemy/Slime";
        //首先要获取到没有修饰过的Slime的GameObject
        GameObject go = Resources.Load<GameObject>(prefabPath);
        //创建一个SlimeData
        EnemyData data = new SlimeData("Slime", 10f, 10, 2, 10);

        //获取Silme相关组件,并且赋予数值
        Slime slime = ComponentFinder.GetOrAddComponent<Slime>(go);
        slime.enemyType = EnemyType.Slime;
        SlimeMove move = ComponentFinder.GetOrAddComponent<SlimeMove>(go);
        move.speed = data.Speed;
        SlimeAttack attack = ComponentFinder.GetOrAddComponent<SlimeAttack>(go);
        attack.attack = data.Attack;
        SlimeHealth health = ComponentFinder.GetOrAddComponent<SlimeHealth>(go);
        health.health = data.Health;

        return go;
    }
    /// <summary>
    /// 返回一个完整的树桩怪
    /// </summary>
    /// <returns></returns>
    public static GameObject CreateATree()
    {
        string prefabPath = "Prefab/Enemy/Tree";
        //同上
        GameObject gameObject = Resources.Load<GameObject>(prefabPath);
        //同上
        EnemyData data = new TreeData("Tree", 5f, 5, 5,10);

        //获取Tree相关组件并赋予数值
        Tree tree = ComponentFinder.GetOrAddComponent<Tree>(gameObject);
        tree.enemyType = EnemyType.Tree;
        TreeMove move = ComponentFinder.GetOrAddComponent<TreeMove>(gameObject);
        move.speed = data.Speed;
        TreeAttack attack = ComponentFinder.GetOrAddComponent<TreeAttack>(gameObject);
        attack.attack = data.Attack;
        TreeHealth health = ComponentFinder.GetOrAddComponent<TreeHealth>(gameObject);
        health.health = data.Health;

        return gameObject;
    }

    /// <summary>
    /// 创建一个远程史莱姆
    /// </summary>
    /// <returns></returns>
    public static GameObject CreateARemoteSlime()
    {
        string prefabPath = "Prefab/Enemy/RemoteSlime";
        GameObject go = Resources.Load<GameObject>(prefabPath);
        //创建数据
        RemoteSlimeData data = new RemoteSlimeData("RemoteSlime", 10f, 10, 2, 10, 30f, 5f, 5, float.MaxValue, 1080f);
        //远程史莱姆的其他逻辑
        RemoteSlime remoteSlime = ComponentFinder.GetOrAddComponent<RemoteSlime>(go);
        remoteSlime.enemyType = EnemyType.RemoteSlime;
        //有关远程史莱姆的逻辑
        RemoteSlimeMove remoteSlimeMove = ComponentFinder.GetOrAddComponent<RemoteSlimeMove>(go);
        remoteSlimeMove.speed = data.Speed;
        RemoteSlimeAttack remoteSlimeAttack = ComponentFinder.GetOrAddComponent<RemoteSlimeAttack>(go);
        remoteSlimeAttack.attack = data.Attack;
        RemoteSlimeRemoteAttack remoteSlimeRemoteAttack = ComponentFinder.GetOrAddComponent<RemoteSlimeRemoteAttack>(go);
        remoteSlimeRemoteAttack.range = data.FireRange;
        remoteSlimeRemoteAttack.frequency = data.FireFrequency;
        RemoteSlimeHealth remoteSlimeHealth = ComponentFinder.GetOrAddComponent<RemoteSlimeHealth>(go);
        remoteSlimeHealth.health = data.Health;
        RemoteSlimeAnimation remoteSlimeAnimation = ComponentFinder.GetOrAddComponent<RemoteSlimeAnimation>(go);
        

        string bulletPrefabPath = "Prefab/EnemyBullet/RemoteSlimeBullet";
        GameObject bulletGo = Resources.Load<GameObject>(bulletPrefabPath);

        //有关远程史莱姆子弹的相关逻辑
        RemoteSlimeBulletMove remoteSlimeBullet = ComponentFinder.GetOrAddComponent<RemoteSlimeBulletMove>(bulletGo);
        remoteSlimeBullet.speed = data.BulletSpeed;
        remoteSlimeBullet.effectiveRange = data.EffectiveRange;
        RemoteSlimeBulletAttack remoteSlimeBulletAttack = ComponentFinder.GetOrAddComponent<RemoteSlimeBulletAttack>(bulletGo);
        remoteSlimeBulletAttack.attack = data.BulletAttack;
        //给对应逻辑脚本添加上对应子弹
        remoteSlimeRemoteAttack.bulletPrefab = bulletGo;

        return go;
    }

    /// <summary>
    /// 创建一个测试用例
    /// </summary>
    /// <returns></returns>
    public static GameObject CreateATest()
    {
        string prefabPath = "Prefab/Enemy/Test";
        GameObject go = Resources.Load<GameObject>(prefabPath);

        return go;
    }
}



