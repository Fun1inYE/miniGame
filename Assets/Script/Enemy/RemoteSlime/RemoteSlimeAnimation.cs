using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 管理远程史莱姆的动画
/// </summary>
public class RemoteSlimeAnimation : MonoBehaviour
{
    /// <summary>
    /// 远程史莱姆的
    /// </summary>
    private Animator anim;

    /// <summary>
    /// 判断远程史莱姆是否在攻击
    /// </summary>
    public bool isAttack;

    private void OnEnable()
    {   
        //获取到Animator
        anim = ComponentFinder.GetOrAddComponent<Animator>(gameObject);
        isAttack = false;
    }

    private void Update()
    {
        SwitchAnimation();
    }

    /// <summary>
    /// 转换动画的方法
    /// </summary>
    private void SwitchAnimation()
    {
        anim.SetBool("IsAttack", isAttack);
        //获取到动画状态机的信息，获取到第一层
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        //如果名字为Attack的AnimationClip并且播放即将结束
        if(stateInfo.IsName("Attack") && stateInfo.normalizedTime >= 0.9)
        {
            isAttack = false;
        }
    }
}
