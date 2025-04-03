
/// <summary>
/// 近战武器数据类
/// </summary>
public class MeleeWeaponData : WeaponData
{   
    /// <summary>
    /// 近战武器的攻击范围
    /// </summary>
    private float radius;

    /// <summary>
    /// 近战武器的旋转速度
    /// </summary>
    private float speed;

    /// <summary>
    /// 近战武器最大数量
    /// </summary>
    private int maxCount;

    public float Radius { get => radius; set => radius = value; }
    public float Speed { get => speed; set => speed = value; }
    public int MaxCount { get => maxCount; set => maxCount = value; }

    public MeleeWeaponData(string name, string description, int attack, float back, AttackType attackType, WeaponType weaponType, float radius, float speed, int maxCount) : base(name, description, attack, back, attackType, weaponType)
    {
        this.radius = radius;
        this.speed = speed;
        this.maxCount = maxCount;
    }

    public MeleeWeaponData() {}
}
