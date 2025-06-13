/// <summary>
/// 总结数据类
/// </summary>
public class SummaryData
{
    /// <summary>
    /// 击杀敌人数
    /// </summary>
    private int killEnemyCount;

    /// <summary>
    /// 获取的蓝宝石数量
    /// </summary>
    private int getSapphireCount;

    /// <summary>
    /// 获取到的金币的数量
    /// </summary>
    private int getCoinCount;

    /// <summary>
    /// 总伤害
    /// </summary>
    private int totalAttack;

    public SummaryData(int killEnemyCount, int getSapphireCount, int getCoinCount, int totalAttack)
    {
        this.killEnemyCount = killEnemyCount;
        this.getSapphireCount = getSapphireCount;
        this.getCoinCount = getCoinCount;
        this.totalAttack = totalAttack;
    }

    public int KillEnemyCount { get => killEnemyCount; set => killEnemyCount = value; }
    public int GetSapphireCount { get => getSapphireCount; set => getSapphireCount = value; }
    public int GetCoinCount { get => getCoinCount; set => getCoinCount = value; }
    public int TotalAttack { get => totalAttack; set => totalAttack = value; }
}
