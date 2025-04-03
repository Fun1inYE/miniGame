/// <summary>
/// 敌人数值类
/// </summary>
public class EnemyData
{
    /// <summary>
    /// 敌人的名字
    /// </summary>
    private string name;

    /// <summary>
    /// 敌人的移动速度
    /// </summary>
    private float speed;

    /// <summary>
    /// 敌人的血量
    /// </summary>
    private int health;

    /// <summary>
    /// 敌人的攻击力
    /// </summary>
    private int attack;

    /// <summary>
    /// 敌人的等级（数字越高，怪物越弱，生成的数量越高，最高等级为10）
    /// </summary>
    private int level;

    public string Name { get => name; set => name = value; }
    public float Speed { get => speed; set => speed = value; }
    public int Attack { get => attack; set => attack = value; }
    public int Level { get => level; set => level = value; }
    public int Health { get => health; set => health = value; }

    /// <summary>
    /// 生成敌人数值的构造函数
    /// </summary>
    public EnemyData(string name, float speed, int health, int attack, int level)
    {
        this.name = name;
        this.speed = speed;
        this.health = health;
        this.attack = attack;
        this.level = level;
    }

    /// <summary>
    /// 空构造函数
    /// </summary>
    public EnemyData() {}
}