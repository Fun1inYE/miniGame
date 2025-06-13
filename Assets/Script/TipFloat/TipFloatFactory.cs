using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 飘字工厂
/// </summary>
public static class TipFloatFactory
{
    /// <summary>
    /// 按照所给数值创建一个value
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static GameObject CreateAChestRewardTip(string value)
    {
        string prefabPath = "Prefab/UI/GemTip";
        GameObject go = Resources.Load<GameObject>(prefabPath);

        //获取到GemTip的Text节点
        Text valueText = ComponentFinder.GetChildComponent<Text>(go, "Value");
        valueText.text = value;
        return go;
    }
}
