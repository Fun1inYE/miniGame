using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using WeChatWASM;

/// <summary>
/// 装备武器类
/// </summary>
public class EquipWeapon : MonoBehaviour
{
    /// <summary>
    /// 圆心
    /// </summary>
    private Transform center;

    /// <summary>
    /// 装备的近战武器种类 键为武器名字 值为武器
    /// </summary>
    private Dictionary<string, List<GameObject>> weaponDic;

    /// <summary>
    /// 近战武器容器 键为武器名字 值为武器容器节点
    /// </summary>
    private Dictionary<string, GameObject> weaponContainerDic;

    /// <summary>
    /// 武器容器的transform
    /// </summary>
    public Transform weaponContainerTransform;

    private void Awake()
    {
        //初始化武器字典
        weaponDic = new Dictionary<string, List<GameObject>>();
        weaponContainerDic = new Dictionary<string, GameObject>();
        center = FindAndMoveObject.FindChildBreadthFirst(FindAndMoveObject.FindFromFirstLayer("Player").transform, "WeaponContainer");
    }

    private void OnEnable()
    {
        //向全局消息管理器注册装备武器的方法
        MessageManager.Instance.AddFunctionInAction<GameObject>(MessageDefine.EQUIPMENT_WEAPON, EquipAWeapon);
    }

    /// <summary>
    /// 装备新武器的方法
    /// </summary>
    public void EquipAWeapon(GameObject obj)
    {
        if(weaponDic.Count > 10)
        {
            //TODO:提醒背包已经满了
            Debug.Log("装备已经达到最大值");
            return;
        }
        //首先识别武器攻击方式，然后装备
        IdentifyAttackTypeAndEquipmentWeapon(obj);
    }

    /// <summary>
    /// 识别武器攻击方式并且装备武器
    /// </summary>
    /// <param name="obj"></param>
    private void IdentifyAttackTypeAndEquipmentWeapon(GameObject obj)
    {
        //识别武器的攻击形式
        AttackType type = obj.GetComponent<Weapon>().attackType;
        //获取武器名字
        string weaponName = obj.GetComponent<Weapon>().weaponName;

        //装备武器
        switch(type)
        {
            case AttackType.Melee:
                //获取武器的旋转数据
                RotateAroundToTarget rotateAroundToTarget = obj.GetComponent<RotateAroundToTarget>();
                //检测武器字典中是否已经有该武器
                if(!weaponDic.ContainsKey(weaponName))
                {
                    //添加字典
                    weaponDic.Add(weaponName, new List<GameObject>());
                    //添加武器
                    weaponDic[weaponName].Add(obj);
                    //刷新武器容器
                    RefreshWeaponContainer(weaponName);
                    //装备武器
                    CreateMeleeWeapon(weaponDic[weaponName], weaponName);
                }
                else
                {
                    if(weaponDic[weaponName].Count < rotateAroundToTarget.maxCount)
                    {
                        //同种武器数量加一
                        weaponDic[weaponName].Add(obj);
                        //刷新武器容器
                        RefreshWeaponContainer(weaponName);
                        //装备武器
                        CreateMeleeWeapon(weaponDic[weaponName], weaponName);
                    }
                    else
                    {
                        //TODO: 武器数量已经达到最大值
                        Debug.Log($"武器{weaponName}数量已经达到最大值");
                    }
                }
                break;
            
            case AttackType.Remote:
                //检测武器字典中是否已经有该武器
                if(!weaponDic.ContainsKey(weaponName))
                {
                    //添加字典
                    weaponDic.Add(weaponName, new List<GameObject>());
                    //添加武器
                    weaponDic[weaponName].Add(obj);
                    //刷新武器容器
                    RefreshWeaponContainer(weaponName);
                    //装备武器
                    CreateRemoteWeapon(obj, weaponName);
                }
                else
                {
                    Debug.Log("已经拥有该武器");
                }
                break;
        }
    }

    /// <summary>
    /// 装备近战武器的方法
    /// </summary>
    private void CreateMeleeWeapon(List<GameObject> weaponList, string weaponName)
    {
        //读取这类武器的旋转数据
        RotateAroundToTarget rotateAroundToTarget = weaponList[0].GetComponent<RotateAroundToTarget>();

        //圆的半径
        float radius = rotateAroundToTarget.radius;

        // 计算每个武器之间的角度
        float angleStep = 360f / weaponList.Count;
        
        for (int i = 0; i < weaponList.Count; i++)
        {
            // 计算当前武器的角度
            float angle = i * angleStep;
            
            // 将角度转换为弧度
            float radian = angle * Mathf.Deg2Rad;
            
            // 计算武器在圆上的位置
            float x = radius * Mathf.Cos(radian);
            float y = radius * Mathf.Sin(radian);
            
            // 创建武器
            GameObject weapon = Instantiate(weaponList[i], center.position, Quaternion.identity);
            
            // 将武器设为当前武器容器的子物体
            weapon.transform.SetParent(weaponContainerDic[weaponName].transform);
            weapon.transform.localPosition = new Vector3(x, y, 0);
            
            // 设置武器头部朝向（朝外）
            weapon.transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
        }
    }

    /// <summary>
    /// 装备远程武器的方法
    /// </summary>
    private void CreateRemoteWeapon(GameObject weaponObj, string weaponName)
    {
        // 创建武器
        GameObject weapon = Instantiate(weaponObj, center.position, Quaternion.identity);
        // 将武器设为当前武器容器的子物体
        weapon.transform.SetParent(weaponContainerDic[weaponName].transform);
        weapon.transform.localPosition = Vector3.zero;
    }

    /// <summary>
    /// 刷新武器容器的方法
    /// </summary>
    private void RefreshWeaponContainer(string weaponName)
    {
        //创建一个武器容器
        GameObject container;
        //将容器录入到字典中
        if(weaponContainerDic.ContainsKey(weaponName))
        {
            Destroy(weaponContainerDic[weaponName]);
            weaponContainerDic.Remove(weaponName);
            container = new GameObject(weaponName);
        }
        else
        {
            container = new GameObject(weaponName);
        }
        weaponContainerDic.Add(weaponName, container);

        //将容器设为武器容器的子物体
        container.transform.SetParent(weaponContainerTransform);
        container.transform.localPosition = Vector3.zero;
    }

    private void OnDisable()
    {
        MessageManager.Instance.RemoveFunctionInAction<GameObject>(MessageDefine.EQUIPMENT_WEAPON, EquipAWeapon);
    }
}
