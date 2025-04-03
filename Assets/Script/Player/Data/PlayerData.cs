/// <summary>
/// 玩家的基础数值
/// </summary>
public class PlayerData
{
    /// <summary>
    /// 玩家的名字
    /// </summary>
    private string name;

    /// <summary>
    /// 玩家的血量
    /// </summary>
    private float hp;

    /// <summary>
    /// 玩家的移速
    /// </summary>
    private float speed;

    /// <summary>
    /// 玩家的护盾
    /// </summary>
    private float shell;

    /// <summary>
    /// 玩家基础数值的构造函数
    /// </summary>
    public PlayerData(string name, float hp, float speed, float shell)
    {
        this.name = name;
        this.hp = hp;
        this.speed = speed;
        this.shell = shell;
    }

    /// <summary>
    /// 无参构造函数
    /// </summary>
    public PlayerData() {}

}
