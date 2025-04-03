/// <summary>
/// 远程武器数据类
/// </summary>
public class RemoteWeaponData : WeaponData
{
    /// <summary>
    /// 远程武器的攻击范围
    /// </summary>
    private float range;

    /// <summary>
    /// 有效范围
    /// </summary>
    private float effectiveRange;

    /// <summary>
    /// 攻击频率
    /// </summary>
    private float attackFrequency;

    /// <summary>
    /// 子弹飞行速度
    /// </summary>
    private float speed;

    /// <summary>
    /// 子弹是否可以追踪
    /// </summary>
    private bool canTrack;

    public float Range { get => range; set => range = value; }
    public float AttackFrequency { get => attackFrequency; set => attackFrequency = value; }
    public float EffectiveRange { get => effectiveRange; set => effectiveRange = value; }
    public float Speed { get => speed; set => speed = value; }
    public bool CanTrack { get => canTrack; set => canTrack = value; }

    public RemoteWeaponData(string name, string description, int attack, float back, AttackType attackType, WeaponType weaponType, float range, float effectiveRange, float attackFrequency, float speed, bool canTrack) : base(name, description, attack, back, attackType, weaponType)
    {
        this.range = range;
        this.effectiveRange = effectiveRange;
        this.attackFrequency = attackFrequency;
        this.speed = speed;
        this.canTrack = canTrack;
    }
}
