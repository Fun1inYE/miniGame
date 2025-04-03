using Unity.VisualScripting.FullSerializer;

/// <summary>
/// 远程史莱姆的数据类
/// </summary>
public class RemoteSlimeData : EnemyData
{
    /// <summary>
    /// 子弹速度
    /// </summary>
    private float bulletSpeed;
    /// <summary>
    /// 射击频率
    /// </summary>
    private float fireFrequency;
    /// <summary>
    /// 子弹伤害
    /// </summary>
    private int bulletAttack;

    /// <summary>
    /// 子弹的攻击范围
    /// </summary>
    private float fireRange;

    /// <summary>
    /// 子弹有效范围
    /// </summary>
    private float effectiveRange;

    public float BulletSpeed { get => bulletSpeed; set => bulletSpeed = value; }
    public float FireFrequency { get => fireFrequency; set => fireFrequency = value; }
    public int BulletAttack { get => bulletAttack; set => bulletAttack = value; }
    public float EffectiveRange { get => effectiveRange; set => effectiveRange = value; }
    public float FireRange { get => fireRange; set => fireRange = value; }

    public RemoteSlimeData(string name, float speed, int health, int attack, int level, float bulletSpeed, float fireFrequency, int bulletAttack, float fireRange, float effectiveRange) : base(name, speed, health, attack, level)
    {
        this.bulletSpeed = bulletSpeed;
        this.fireFrequency = fireFrequency;
        this.bulletAttack = bulletAttack;
        this.fireRange = fireRange;
        this.effectiveRange = effectiveRange;
    }

    public RemoteSlimeData()
    {

    }
}
