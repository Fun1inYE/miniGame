using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 计时器工具类（通用计时器）
/// </summary>
public class Timer : MonoBehaviour
{
    /// <summary>
    /// 计时器协程
    /// </summary>
    private Coroutine timerCoroutine;

    /// <summary>
    /// 计时时间
    /// </summary>
    public float remainingTime;

    /// <summary>
    /// 计时器是否正在计时
    /// </summary>
    private bool isRunning = false;

    /// <summary>
    /// 委托和事件，通知计时结束，然后执行委托中的内容
    /// </summary>
    public delegate void TimerFinishedHandler();
    public event TimerFinishedHandler OnTimerFinished;

    /// <summary>
    /// 用于时间更新提醒外部更新的方法
    /// </summary>
    public UnityAction<string> TimeUpdateEvent;

    /// <summary>
    /// 启动计时器
    /// </summary>
    /// <param name="time"></param>
    public void StartCountdownTimer(float time, float frequency)
    {
        //如果计时器还在运转的话，就停止计时器
        if (isRunning)
        {
            StopCountdownTimer();
        }

        remainingTime = time;
        isRunning = true;
        timerCoroutine = StartCoroutine(CountdownCoroutine(frequency));
    }

    /// <summary>
    /// 开始加时器
    /// </summary>
    /// <param name="frequency"></param>
    public void StartExtendTimer(float time, float frequency)
    {
        if(isRunning)
        {
            StopExtendTimer();
        }
        remainingTime = 0;
        isRunning = true;
        timerCoroutine = StartCoroutine(ExtenderCoroutine(time, frequency));
    }

    /// <summary>
    /// 停止加时器
    /// </summary>
    public void StopExtendTimer()
    {
        if(timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine);
            isRunning = false;
        }
    }

    /// <summary>
    /// 停止计时器的方法
    /// </summary>
    public void StopCountdownTimer()
    {
        if(timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine);
            isRunning = false;
        }
    }

    /// <summary>
    /// 协程减时计时器
    /// </summary>
    /// <returns></returns>
    private IEnumerator CountdownCoroutine(float frequency)
    {
        while (remainingTime > 0)
        {
            yield return new WaitForSeconds(frequency);
            remainingTime -= frequency;

            //提醒外部UI更新
            TimeUpdateEvent?.Invoke(remainingTime.ToString());
        }

        isRunning = false;
        OnTimerFinished?.Invoke();
    }

    /// <summary>
    /// 协程加时计时器
    /// </summary>
    /// <returns></returns>
    private IEnumerator ExtenderCoroutine(float time, float frequency)
    {
        while (remainingTime < time)
        {
            yield return new WaitForSeconds(frequency);
            remainingTime += frequency;
            
            //提醒外部UI更新
            TimeUpdateEvent?.Invoke((time - remainingTime).ToString());
        }
        
        isRunning = false;
        OnTimerFinished?.Invoke();
    }
}
