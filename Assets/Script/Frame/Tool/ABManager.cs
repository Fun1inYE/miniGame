using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using WeChatWASM;

/// <summary>
/// AB包管理器
/// </summary>
public class ABManager : Singleton<ABManager>
{
    /// <summary>
    /// 主包
    /// </summary>
    private AssetBundle mainAB = null;

    /// <summary>
    /// 主包文件的依赖文件
    /// </summary>
    private AssetBundleManifest manifest = null;

    /// <summary>
    /// AB包的字典，ab包名字为键，ab包为值
    /// </summary>
    private Dictionary<string, AssetBundle> abDic = new Dictionary<string, AssetBundle>();

    /// <summary>
    /// AB包路径
    /// </summary>
    private string PathUrl
    {
        get
        {
            return Application.streamingAssetsPath + "/";
        }
    }

    /// <summary>
    /// 主包名字
    /// </summary>
    private string MainABName
    {
        get
        {
            #if UNITY_WEBGL
                return "WebGl";
            #else
                reutrn "PC";
            #endif
        }
    }

    /// <summary>
    /// 同步加载的方法,直接返回资源
    /// </summary>
    /// <param name="abName">ab包的名子</param>
    /// <param name="resName">ab包中的资源名字</param>
    public Object LoadRes(string abName, string resName)
    {
        //加载主AB包
        if(mainAB == null)
        {
            mainAB = AssetBundle.LoadFromFile(PathUrl + MainABName);
            manifest = mainAB.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        }
        //获取依赖包相关信息
        AssetBundle ab;
        string[] strs = manifest.GetAllDependencies(abName);
        for(int i = 0; i < strs.Length; i++)
        {
            //判断包是否被加载过
            if(!abDic.ContainsKey(strs[i]))
            {
                ab = AssetBundle.LoadFromFile(PathUrl + strs[i]);
                abDic.Add(strs[i], ab);
            }
        }
        //加载资源来源包
        if(!abDic.ContainsKey(abName))
        {
            ab = AssetBundle.LoadFromFile(PathUrl + abName);
        }

        //加载资源
        Object obj = abDic[abName].LoadAsset(resName);

        return obj;
    }

    /// <summary>
    /// 根据类型返回资源
    /// </summary>
    /// <param name="abName"></param>
    /// <param name="resName"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    public Object LoadRes(string abName, string resName, System.Type type)
    {
        //加载主AB包
        if(mainAB == null)
        {
            mainAB = AssetBundle.LoadFromFile(PathUrl + MainABName);
            manifest = mainAB.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        }
        //获取依赖包相关信息
        AssetBundle ab;
        string[] strs = manifest.GetAllDependencies(abName);
        for(int i = 0; i < strs.Length; i++)
        {
            //判断包是否被加载过
            if(!abDic.ContainsKey(strs[i]))
            {
                ab = AssetBundle.LoadFromFile(PathUrl + strs[i]);
                abDic.Add(strs[i], ab);
            }
        }
        //加载资源来源包
        if(!abDic.ContainsKey(abName))
        {
            ab = AssetBundle.LoadFromFile(PathUrl + abName);
        }

        //加载资源
        Object obj = abDic[abName].LoadAsset(resName, type);

        return obj;
    }

    /// <summary>
    /// 根据泛型指定类型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="abName"></param>
    /// <param name="resName"></param>
    /// <returns></returns>
    public Object LoadRes<T>(string abName, string resName) where T : Object
    {
        //加载主AB包
        if(mainAB == null)
        {
            mainAB = AssetBundle.LoadFromFile(PathUrl + MainABName);
            manifest = mainAB.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        }
        //获取依赖包相关信息
        AssetBundle ab;
        string[] strs = manifest.GetAllDependencies(abName);
        for(int i = 0; i < strs.Length; i++)
        {
            //判断包是否被加载过
            if(!abDic.ContainsKey(strs[i]))
            {
                ab = AssetBundle.LoadFromFile(PathUrl + strs[i]);
                abDic.Add(strs[i], ab);
            }
        }
        //加载资源来源包
        if(!abDic.ContainsKey(abName))
        {
            ab = AssetBundle.LoadFromFile(PathUrl + abName);
        }

        //加载资源
        Object obj = abDic[abName].LoadAsset<T>(resName);

        return obj;
    }

    /// <summary>
    /// 异步加载资源的方法
    /// </summary>
    /// <param name="abName"></param>
    /// <param name="resName"></param>
    /// <param name="callBack"></param>
    public void LoadResAsync(string abName, string resName, UnityAction<Object> callBack)
    {
        StartCoroutine(LoadResCoro(abName, resName, callBack));
    }

    public void LoadResAsync(string abName, string resName, System.Type type, UnityAction<Object> callBack)
    {
        StartCoroutine(LoadResCoro(abName, resName, type, callBack));
    }

    /// <summary>
    /// 异步加载资源的协程
    /// </summary>
    /// <param name="abName"></param>
    /// <param name="resName"></param>
    /// <param name="callBack"></param>
    /// <returns></returns>
    private IEnumerator LoadResCoro(string abName, string resName, UnityAction<Object> callBack)
    {
        //加载主AB包
        if(mainAB == null)
        {
            mainAB = AssetBundle.LoadFromFile(PathUrl + MainABName);
            manifest = mainAB.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        }
        //获取依赖包相关信息
        AssetBundle ab;
        string[] strs = manifest.GetAllDependencies(abName);
        for(int i = 0; i < strs.Length; i++)
        {
            //判断包是否被加载过
            if(!abDic.ContainsKey(strs[i]))
            {
                ab = AssetBundle.LoadFromFile(PathUrl + strs[i]);
                abDic.Add(strs[i], ab);
            }
        }
        //加载资源来源包
        if(!abDic.ContainsKey(abName))
        {
            ab = AssetBundle.LoadFromFile(PathUrl + abName);
        }

        //加载资源
        AssetBundleRequest abr = abDic[abName].LoadAssetAsync(resName);

        yield return abr;

        callBack(abr.asset);
       
    }

    private IEnumerator LoadResCoro(string abName, string resName, System.Type type, UnityAction<Object> callBack)
    {
        //加载主AB包
        if(mainAB == null)
        {
            mainAB = AssetBundle.LoadFromFile(PathUrl + MainABName);
            manifest = mainAB.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        }
        //获取依赖包相关信息
        AssetBundle ab;
        string[] strs = manifest.GetAllDependencies(abName);
        for(int i = 0; i < strs.Length; i++)
        {
            //判断包是否被加载过
            if(!abDic.ContainsKey(strs[i]))
            {
                ab = AssetBundle.LoadFromFile(PathUrl + strs[i]);
                abDic.Add(strs[i], ab);
            }
        }
        //加载资源来源包
        if(!abDic.ContainsKey(abName))
        {
            ab = AssetBundle.LoadFromFile(PathUrl + abName);
        }

        //加载资源
        AssetBundleRequest abr = abDic[abName].LoadAssetAsync(resName, type);

        yield return abr;

        callBack(abr.asset);
       
    }

    /// <summary>
    /// 卸载AB包
    /// </summary>
    /// <param name="abName">AB包的名字</param>
    public void UnLoad(string abName)
    {
        if(abDic.ContainsKey(abName))
        {
            abDic[abName].Unload(false);
            abDic.Remove(abName);
        }
    }

    /// <summary>
    /// 卸载所有AB包
    /// </summary>
    public void UnLoadAll()
    {
        AssetBundle.UnloadAllAssetBundles(false);
        abDic.Clear();
        mainAB = null;
        manifest = null;
    }
}
