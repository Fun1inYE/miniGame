using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 在游戏中显示提示的管理器
/// </summary>
public class TipManager : MonoBehaviour
{
    /// <summary>
    /// 等待初始化的TextUI提示
    /// </summary>
    public List<TextTip> textTips;
    /// <summary>
    /// 等待初始化的ImageUI提示
    /// </summary>
    public List<ImageTip> imageTips;
    /// <summary>
    /// TextUITip的字典: 键为名字，值为TextUI中的content
    /// </summary>
    private Dictionary<string, string> textTipUIDic;
    /// <summary>
    /// ImageUITip的字典: 键为名字，值为ImageUI中的content
    /// </summary>
    private Dictionary<string, Sprite> imageTipDic;

    /// <summary>
    /// 要渲染到的canvas
    /// </summary>
    public Canvas canvas;

    /// <summary>
    /// 单例
    /// </summary>
    public static TipManager Instance;

    /// <summary>
    /// 脚本初始化
    /// </summary>
    private void Awake()
    {
        //字典初始化
        textTipUIDic = new Dictionary<string, string>();
        imageTipDic = new Dictionary<string, Sprite>();

        //单例初始化
        if(Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    /// <summary>
    /// 初始化TipUI
    /// </summary>
    public void InitTipUI() 
    {
        InitTextUI();
        InitImageUI();
    }

    private void Update()
    {
        //CheckElement();
    }

    /// <summary>
    /// 初始化TextTipUI方法
    /// </summary>
    private void InitTextUI()
    {
        foreach(TextTip textUI in textTips)
        {
            //给每一个TextTipUI初始化，并且添加对应组件，然后填入字典
            if(!textTipUIDic.ContainsKey(textUI.name))
            {
                textTipUIDic.Add(textUI.name, textUI.content);
            }
        }
    }

    /// <summary>
    /// 初始化ImageTipUI方法
    /// </summary>
    private void InitImageUI()
    {
        foreach(ImageTip imageUI in imageTips)
        {
            if(!imageTipDic.ContainsKey(imageUI.name))
            {
                imageTipDic.Add(imageUI.name, imageUI.content);
            }
        }
    }

    /// <summary>
    /// 返回TextTipUI
    /// </summary>
    /// <param name="name">UI的名字</param>
    /// <returns>UI对应的GameObject</returns>
    public GameObject GetAndDisplayTextUI(string name)
    {
        if(textTipUIDic.ContainsKey(name) && canvas != null)
        {
            //生成对应UI
            GameObject obj = Instantiate(Resources.Load<GameObject>("Prefab/TipUI/TextTipUI"));
            obj.GetComponent<Text>().text = textTipUIDic[name];
            //将UI放到对应的canvas下
            obj.transform.SetParent(canvas.transform, false);
            obj.SetActive(true);
            return obj;
        }
        else
        {
            Debug.LogWarning($"字典中不存在{name}的TextUI, 或者没有配置正确的canvas, 当前canvas为{canvas.name}");
            return null;
        }
            
    }

    /// <summary>
    /// 返回ImageTipUI
    /// </summary>
    /// <param name="name">UI的名字</param>
    /// <returns>对应的GameObject</returns>
    public GameObject GetAndDisplayIamge(string name)
    {
        if(imageTipDic.ContainsKey(name) && canvas != null)
        {
            //生成对应UI
            GameObject obj = Instantiate(Resources.Load<GameObject>("Prefab/TipUI/ImageTipUI"));
            obj.GetComponent<Image>().sprite = imageTipDic[name];
            //将UI放到对应的canvas下
            obj.transform.SetParent(canvas.transform, false);
            return obj;
        }
        else
        {
            Debug.LogWarning($"字典中不存在{name}的imageUI");
            return null;
        }
    }

    /// <summary>
    /// 关闭TipUI的方法
    /// </summary>
    /// <param name="obj"></param>
    public void CloseTipUI(GameObject obj)
    {
        obj.SetActive(false);
        Destroy(obj);
    }

    /// <summary>
    /// 要将UI渲染到哪个canvas上
    /// </summary>
    public void SetCanvas(Canvas canvas)
    {
        this.canvas = canvas;
    }

    #region TestCode
    /// <summary>
    /// 检查字典中的元素
    /// </summary>
    public void CheckElement()
    {
        if(Input.GetKeyDown(KeyCode.O))
        {
            foreach(TextTip textUI in textTips)
            {
                Debug.Log(textUI.name);
            }
        }
    }
    #endregion
}
