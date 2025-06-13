using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 冲击波的工厂
/// </summary>
public static class ShockWaveFactory
{
    //创建一个冲击波
    public static GameObject CreateAShockWave()
    {
        string prefabPath = "Prefab/ShockWave/ShockWave";
        GameObject go = Resources.Load<GameObject>(prefabPath);

        DrawRing drawRing = ComponentFinder.GetOrAddComponent<DrawRing>(go);

        return go;
    }
}
