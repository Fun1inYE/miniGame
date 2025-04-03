using System;

/// <summary>
/// MainMenuPanel中的数据类
/// </summary>
public class MainMenuPanelData
{
    /// <summary>
    /// 蓝宝石的数量
    /// </summary>
    private int sapphire;

    public int Sapphire { get => sapphire; set => sapphire = value; }

    /// <summary>
    /// 数据更改的委托
    /// </summary>
    public event Action OnDataChange;

    /// <summary>
    /// 无参构造函数
    /// </summary>
    public MainMenuPanelData()
    {
        sapphire = 0;
    }

    public void NotifySapphire(int value)
    {
        this.sapphire += value;
        OnDataChange?.Invoke();
    }
}
