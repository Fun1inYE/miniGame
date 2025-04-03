using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 尘埃粒子系统类
/// </summary>
public class DirtParticale : MonoBehaviour
{
    /// <summary>
    /// 尘埃粒子
    /// </summary>
    public ParticleSystem dirtParticale;

    public Rigidbody2D rb;

    /// <summary>
    /// 判断粒子效果有没有正在播放
    /// </summary>
    private bool isPlaying = false;

    private void Start()
    {
        if(dirtParticale == null)
        {
            Debug.LogError("dirtParticale是空的");
        }
        if(rb == null)
        {
            Debug.LogError("rb是空的");
        }
    }

    private void Update()
    {
        ParticaleOption();
    }

    /// <summary>
    /// 播放尘埃粒子
    /// </summary>
    private void ParticaleOption()
    {
        //如果玩家速度不为0的话就播放尘埃粒子效果
        if(rb.velocity != default && !isPlaying)
        {
            dirtParticale.Play();
            isPlaying = true;
        }
        else if(rb.velocity == default && isPlaying)
        {
            dirtParticale.Stop();
            isPlaying = false;
        }
    }
}
