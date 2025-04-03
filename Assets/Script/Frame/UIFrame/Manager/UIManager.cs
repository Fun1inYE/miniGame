using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// UI管理器
/// </summary>
public class UIManager : MonoBehaviour
{
    /// <summary>
    /// 等待初始化的UI的List
    /// </summary>
    public List<GameObject> panelList;

    /// <summary>
    /// 要panelList中的UI生成在的canvas（可以手动拖入，也可以代码控制）
    /// </summary>
    public Canvas canvas;

    /// <summary>
    /// 记录UI信息的字典: 键为UI名字，值为UI
    /// </summary>
    private Dictionary<string, GameObject> UIDic;

    /// <summary>
    /// 创建PanelManager实例
    /// </summary>
    private PanelManager panelManager;

    /// <summary>
    /// UIManager的单例
    /// </summary>
    public static UIManager Instance;

    /// <summary>
    /// 脚本的初始化
    /// </summary>
    private void Awake()
    {
        //单例初始化
        if(Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        //初始化panelManager
        panelManager = new PanelManager();
        //初始化字典
        UIDic = new Dictionary<string, GameObject>();
    }

    /// <summary>
    /// 初始化UIDic
    /// </summary>
    public void InitUI()
    {
        //遍历初始化
        for(int i = 0; i < panelList.Count; i++)
        {
            //检查panelList中是否有没有绑定上的UI的GameObject
            if(panelList[i] == null)
            {
                Debug.LogError($"请检查panelList, 其中第{i}位元素没有挂载正确的GameObject");
                continue;
            }
            //判断UI字典中是否存在这个UI
            if(!UIDic.ContainsKey(panelList[i].name))
            {
                //没有的话就添加这个UI相关的信息
                UIDic.Add(panelList[i].name, panelList[i]);
            }
        }
    }

    /// <summary>
    /// 设定UI要生成在哪个Canvas下
    /// </summary>
    /// <param name="canvas"></param>
    public void SetCanvas(Canvas canvas)
    {
        this.canvas = canvas;
    }

    /// <summary>
    /// 返回目前的PanelManager
    /// </summary>
    /// <returns></returns>
    public PanelManager GetPanelManager()
    {
        return panelManager;
    }

    /// <summary>
    /// 每帧执行当前活跃的窗口
    /// </summary>
    public void Update()
    {
        panelManager.OnUpdate();
    }

    /// <summary>
    /// 返回并且显示对应名字的UI
    /// </summary>
    /// <param name="name"></param>
    public GameObject GetAndDisplayUI(string name)
    {
        //如果玩家没指定canvas，则不让生成
        if(canvas == null)
        {
            Debug.LogError($"没有指定{name}生成在哪个canvas中，请调用UIManager.Instance.SetCanvas()方法或者直接将canvas直接拖入UIManager脚本中");
            return null;
        }
        //如果字典中包含这个键的话
        if(UIDic.ContainsKey(name))
        {
            GameObject obj = Instantiate(UIDic[name]);
            //将UI的位置直接调整到canvas下
            obj.transform.SetParent(canvas.transform, false);
            obj.SetActive(true);
            return obj;
        }
        else
        {
            Debug.Log($"没有在panelList中查询到名字为{name}的UI");
            return null;
        }
    }

    /// <summary>
    /// 关闭UI的代码
    /// </summary>
    /// <param name="obj"></param>
    public void HideUI(GameObject obj)
    {
        obj.SetActive(false);
        Destroy(obj);
    }
}
