using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// ��ʱ�������ࣨͨ�ü�ʱ����
/// </summary>
public class Timer : MonoBehaviour
{
    /// <summary>
    /// ��ʱ��Э��
    /// </summary>
    private Coroutine timerCoroutine;

    /// <summary>
    /// ��ʱʱ��
    /// </summary>
    public float remainingTime;

    /// <summary>
    /// ��ʱ���Ƿ����ڼ�ʱ
    /// </summary>
    private bool isRunning = false;

    /// <summary>
    /// ί�к��¼���֪ͨ��ʱ������Ȼ��ִ��ί���е�����
    /// </summary>
    public delegate void TimerFinishedHandler();
    public event TimerFinishedHandler OnTimerFinished;

    /// <summary>
    /// ����ʱ����������ⲿ���µķ���
    /// </summary>
    public UnityAction<string> TimeUpdateEvent;

    /// <summary>
    /// ������ʱ��
    /// </summary>
    /// <param name="time"></param>
    public void StartCountdownTimer(float time, float frequency)
    {
        //�����ʱ��������ת�Ļ�����ֹͣ��ʱ��
        if (isRunning)
        {
            StopCountdownTimer();
        }

        remainingTime = time;
        isRunning = true;
        timerCoroutine = StartCoroutine(CountdownCoroutine(frequency));
    }

    /// <summary>
    /// ��ʼ��ʱ��
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
    /// ֹͣ��ʱ��
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
    /// ֹͣ��ʱ���ķ���
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
    /// Э�̼�ʱ��ʱ��
    /// </summary>
    /// <returns></returns>
    private IEnumerator CountdownCoroutine(float frequency)
    {
        while (remainingTime > 0)
        {
            yield return new WaitForSeconds(frequency);
            remainingTime -= frequency;

            //�����ⲿUI����
            TimeUpdateEvent?.Invoke(remainingTime.ToString());
        }

        isRunning = false;
        OnTimerFinished?.Invoke();
    }

    /// <summary>
    /// Э�̼�ʱ��ʱ��
    /// </summary>
    /// <returns></returns>
    private IEnumerator ExtenderCoroutine(float time, float frequency)
    {
        while (remainingTime < time)
        {
            yield return new WaitForSeconds(frequency);
            remainingTime += frequency;
            
            //�����ⲿUI����
            TimeUpdateEvent?.Invoke((time - remainingTime).ToString());
        }
        
        isRunning = false;
        OnTimerFinished?.Invoke();
    }
}
