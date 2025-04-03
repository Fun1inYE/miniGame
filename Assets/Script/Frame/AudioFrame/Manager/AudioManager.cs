using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// 音频片段信息
/// </summary>
[Serializable]
public class Sound
{
   /// <summary>
        /// 音频片段
        /// </summary>
        public AudioClip clip;
        /// <summary>
        /// 音频分组
        /// </summary>
        public AudioMixerGroup outputGroup;
        /// <summary>
        /// 声音大小
        /// </summary>
        [Range(0, 1)]
        public float volume;
        /// <summary>
        /// 音频是否开局播放
        /// </summary>
        public bool playOnAwake;
        /// <summary>
        /// 音频是否循环
        /// </summary>
        public bool loop;
}

/// <summary>
/// 音频管理器
/// </summary>
public class AudioManager : MonoBehaviour
{
    /// <summary>
    /// 所有音频的信息
    /// </summary>
    public List<Sound> sounds;
    /// <summary>
    /// 每一个音频片段对应一个音频组件
    /// </summary>
    private Dictionary<string, AudioSource> audioDic;

    /// <summary>
    /// 音频播放器的单例模式
    /// </summary>
    public static AudioManager Instance;

    /// <summary>
    /// 脚本初始化
    /// </summary>
    private void Awake()
    {
        //初始化音频字典
        audioDic = new Dictionary<string, AudioSource>();

        //单例模式的初始化
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
    /// 初始化字典
    /// </summary>
    private void Start()
    {
        for(int i = 0; i < sounds.Count; i++)
        {
            //先判断有没有绑定的clip有没有异常
            if(sounds[i].clip == null)
            {
                Debug.LogError($"sounds中的第{i}位元素中的clip参数是空的，请检查Hierarchy窗口");
                continue;
            }
            if(!audioDic.ContainsKey(sounds[i].clip.name))
            {
                //新建一个obj，名字为sound中的clip的名字
                GameObject obj = new GameObject(sounds[i].clip.name);
                //将这个obj放在AudioManager中
                obj.transform.SetParent(transform);

                //创建一个音频源
                AudioSource source = obj.AddComponent<AudioSource>();
                //给音频源相关参数赋值
                source.clip = sounds[i].clip;
                source.playOnAwake = sounds[i].playOnAwake;
                source.loop = sounds[i].loop;
                source.volume = sounds[i].volume;
                source.outputAudioMixerGroup = sounds[i].outputGroup;

                //如果sound设置为启动就播放
                if (sounds[i].playOnAwake)
                    source.Play();

                //将音频信息添加到字典中
                audioDic.Add(sounds[i].clip.name, source);
            }

        }
    }

    /// <summary>
    /// 播放名字为name的音频
    /// </summary>
    /// <param name="name">音频的名字</param>
    /// <param name="isWait">是否要等待音频播放完</param>
    /// <param name="loop">是否要音频循环播放</param>
    public void PlayAudio(string name, bool isWait = false, bool loop = false)
    {
        if(!audioDic.ContainsKey(name))
        {
            Debug.LogWarning($"名字为{name}的音频不存在");
            return;
        }

        //更改音频是否循环播放
        audioDic[name].loop = loop;

        //判断音频是否需要等待播放完
        if (isWait)
        {
            //让该音频完全播放完音频
            if (audioDic[name].isPlaying)
            {
                audioDic[name].Play();
            }
            audioDic[name].Play();
        }
        else
        {
            //播放音频
            audioDic[name].Play();
        }
    }

    /// <summary>
    /// 停止名字为name的音频
    /// </summary>
    /// <param name="name">音频的名字</param>
    /// <param name="isWait">是否要等待音频播放完</param>
    public void StopAudio(string name)
    {
        if (!audioDic.ContainsKey(name))
        {
            Debug.LogWarning($"名字为{name}的音频不存在");
            return;
        }

        //停止音频的播放
        audioDic[name].Stop();
    }

    /// <summary>
    /// 暂停音乐
    /// </summary>
    public void PauseAudio(string name)
    {
        if (!audioDic.ContainsKey(name))
        {
            Debug.LogWarning($"名字为{name}的音频不存在");
            return;
        }
        audioDic[name].Pause();
        
    }
}
