/// <summary>
/// 武器基础数值
/// </summary>
public class WeaponData
{
    /// <summary>
    /// 武器的名字
    /// </summary>
    private string name;

    /// <summary>
    /// 武器形容
    /// </summary>
    private string description;

    /// <summary>
    /// 武器攻击力
    /// </summary>
    private int attack;

    /// <summary>
    /// 武器击退力
    /// </summary>
    private float back;

    private AttackType attackType;
    private WeaponType weaponType;

    public string Name { get => name; set => name = value; }
    public string Description { get => description; set => description = value; }
    public int Attack { get => attack; set => attack = value; }
    public float Back { get => back; set => back = value; }
    public AttackType AttackType { get => attackType; set => attackType = value; }
    public WeaponType WeaponType { get => weaponType; set => weaponType = value; }

    public WeaponData(string name, string description, int attack, float back, AttackType attackType, WeaponType weaponType)
    {
        this.name = name;
        this.description = description;
        this.attack = attack;
        this.back = back;
        this.attackType = attackType;
        this.weaponType = weaponType;
    }

    /// <summary>
    /// 无参构造函数
    /// </summary>
    public WeaponData() {}
}
