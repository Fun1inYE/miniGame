using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敌人结构体
/// </summary>
[System.Serializable]
public class EnemyStruct
{
    /// <summary>
    /// 敌人类型
    /// </summary>
    public EnemyType enmeyType;

    /// <summary>
    /// 敌人生成权重占比
    /// </summary>
    public float weight;

    /// <summary>
    /// 开始刷新需要的时间
    /// </summary>
    public float startTime;

    /// <summary>
    /// 权重占比(0 ~ 1)
    /// </summary>
    public Vector2 range;
}

/// <summary>
/// 敌人生成管理器类
/// </summary>
public class EnemyGenerateManager : MonoBehaviour
{
    /// <summary>
    /// 参考中心点
    /// </summary>
    private Transform referencePoint;

    /// <summary>
    /// 敌人结构体
    /// </summary>
    private List<EnemyStruct> currentEnemyStructList;

    /// <summary>
    /// 带生成的波次
    /// </summary>
    private List<WaveData> waveList; 

    /// <summary>
    /// 当前场上的敌人数量
    /// </summary>
    private int currentEnemyCount = 0;

    /// <summary>
    /// 计算下一波敌人的计时器
    /// </summary>
    private Timer nextWaveTimer;
    /// <summary>
    /// 当前游戏时间
    /// </summary>
    private Timer currentTimer;

    /// <summary>
    /// 波次索引(默认为 -1)
    /// </summary>
    private int currentWaveIndex = -1;

    /// <summary>
    /// 生成敌人的协程
    /// </summary>
    private Coroutine generateEnemyCorotine;

    private void Awake()
    {
        //绑定摄像机参考中心点
        referencePoint = Camera.main.transform;
        nextWaveTimer = gameObject.AddComponent<Timer>();
        //给计时器绑定方法
        nextWaveTimer.OnTimerFinished += WaveStart;

        currentTimer = gameObject.AddComponent<Timer>();
        //注册计时结束的方法
        currentTimer.OnTimerFinished += WaveEnd;

        //初始化待生成的敌人列表
        currentEnemyStructList = new List<EnemyStruct>();

        MessageManager.Instance.AddFunctionInAction<int>(MessageDefine.ENEMY_COUNTDOWN, CurrentEnemyCountDown);
        MessageManager.Instance.AddFunctionInAction<List<WaveData>>(MessageDefine.GET_WAVE_INFO, GetWaveInfo);
        MessageManager.Instance.AddFunctionInAction(MessageDefine.GAMESTART, GameStart);
    }

    public void GameStart()
    {
        if(waveList == null)
        {
            Debug.LogError("EnemyGenerateManager没有获取到波次列表, 无法开始游戏");
            return;
        }

        //先判断当前波次数是不是 -1
        if(currentWaveIndex == -1)
        {
            Debug.Log("游戏开始！开始冷却时间");
            WaveColdStart();
        }
    }

    private void LateUpdate()
    {
        //绑定摄像机参考中心点
        referencePoint = Camera.main.transform;
    }

    #region 波次状态相关

    /// <summary>
    /// 获取波次的方法
    /// </summary>
    /// <param name="waveDatas"></param>
    public void GetWaveInfo(List<WaveData> waveDatas)
    {
        Debug.Log("被传递了列表");
        waveList = waveDatas;
    }

    /// <summary>
    /// 开始下一个波次的倒计时
    /// </summary>
    private void WaveColdStart()
    {
        currentWaveIndex ++;
        Debug.Log(waveList.Count);
        //先判断是否已经超出了最大波次的索引
        if(currentWaveIndex > waveList.Count - 1)
        {
            Debug.Log("已经坚持完所有的波次了");
            //TODO: 游戏完成的逻辑
            return;
        }

        Debug.Log($"进入到了下一个波次的倒计时，时间为：{waveList[currentWaveIndex].nextWaveCold}");
        if(waveList != null)
        {
            nextWaveTimer.StartCountdownTimer(waveList[currentWaveIndex].nextWaveCold, 0.2f);
        }
    }

    /// <summary>
    /// 开启波次的方法
    /// </summary>
    private void WaveStart()
    {
        //本波次开始前先将当前游戏时间清除
        currentTimer.remainingTime = 0;
        //将当前敌人数量变为0
        currentEnemyCount = 0;

        Debug.Log($"开始本波次的计时, 本波时间为 {waveList[currentWaveIndex].waveTime}");
        //开始本波次的计时
        currentTimer.StartExtendTimer(waveList[currentWaveIndex].waveTime, 0.2f);
        //按照这个波数进行生成敌人
        generateEnemyCorotine = StartCoroutine(GenerateEnemy(waveList[currentWaveIndex].generateFreqency));
    }

    /// <summary>
    /// 波次结束的方法
    /// </summary>
    private void WaveEnd()
    {
        //停止生成敌人的协程
        StopCoroutine(generateEnemyCorotine);
        //开启新的波数冷却计时
        WaveColdStart();
    }

    #endregion

    #region 敌人生成相关

    /// <summary>
    /// 生成敌人的协程
    /// </summary>
    /// <returns></returns>
    private IEnumerator GenerateEnemy(float generateFreqency)
    {
        while(true)
        {
            //先检测有没有新敌人加入到待生成的列表中
            AddEnemyStructListFromTimer(currentTimer.remainingTime);

            //判断currentEnemyStructList中有没有敌人
            if(currentEnemyStructList.Count != 0)
            {
                //生成一个随机数(0, 1]
                float randomNum = Random.Range(0.000001f, 1.000001f);
                //生成一个随机点
                Vector2 point = GetRandomCoordinate();

                //判断点是否在屏幕内
                if(!JudgmentPoint.IsInScreen(point))
                {
                    if(currentEnemyCount >= waveList[currentWaveIndex].maxEnemyCount)
                    {
                        Debug.Log("敌人数量已经达到当前波数设定最大值");
                    }
                    else
                    {
                        //通过随机值判断应该生成什么敌人
                        GameObject initObj = EnemyFactory.CreateEnemyWithType(GetEnemyTypeWithRange(randomNum));
                        //敌人数量 +1
                        currentEnemyCount ++;
                        Instantiate(initObj, point, Quaternion.identity);
                        Debug.Log("点合法的");
                    }
                }
                else
                {
                    Debug.Log("点不合法");
                }
            }
            
            // 等待generateFreqency秒
            yield return new WaitForSeconds(generateFreqency); 
        }
    }

    /// <summary>
    /// 添加敌人的结构体
    /// </summary>
    /// <param name="currentTime">当前游戏时间</param>
    private void AddEnemyStructListFromTimer(float currentTime)
    {
        //减少遍历，减少性能浪费
        if(currentEnemyStructList.Count == waveList[currentWaveIndex].enemies.Count)
        {
            return;
        }

        //遍历当前波次的敌人
        foreach(EnemyStruct enmey in waveList[currentWaveIndex].enemies)
        {
            //如果当前场上时间大于敌人开始生成时间, 并且列表中不包含该敌人
            if(currentTime >= enmey.startTime && !currentEnemyStructList.Contains(enmey))
            {
                currentEnemyStructList.Add(enmey);
                CalculateRangeWithWeight();
            }
        }
    }

    /// <summary>
    /// 通过权重计算生成比例范围
    /// </summary>
    public void CalculateRangeWithWeight()
    {
        if(currentEnemyStructList.Count == 0)
        {
            Debug.Log("敌人列表中没有敌人");
            return;
        }

        //计算总权重
        float sum = 0;
        foreach(var enemyStruct in currentEnemyStructList)
        {
            sum = sum + enemyStruct.weight;
        }

        //判断总权重
        if(sum == 0)
        {
            Debug.Log("敌人列表中的敌人都不需要生成");
            return;
        }

        //计算范围
        float start = 0;
        float end = 0;
        foreach(var enemyStruct in currentEnemyStructList)
        {
            end = start + enemyStruct.weight / sum;
            enemyStruct.range = new Vector2(start, end);
            start = end;
            Debug.Log($"{enemyStruct.enmeyType}的生成比率范围为：{enemyStruct.range.x} , {enemyStruct.range.y}");
        }
    }

    #endregion

    /// <summary>
    /// 敌人数量减少方法
    /// </summary>
    public void CurrentEnemyCountDown(int count)
    {
        currentEnemyCount = currentEnemyCount - count;
    }

    /// <summary>
    /// 通过随机值判断敌人是否能够生成
    /// </summary>
    /// <param name="randomNum"></param>
    /// <returns></returns>
    private EnemyType GetEnemyTypeWithRange(float randomNum)
    {
        foreach(EnemyStruct enemy in currentEnemyStructList)
        {
            if(randomNum >= enemy.range.x && randomNum <= enemy.range.y)
            {
                return enemy.enmeyType;
            }
        }

        //报错代码
        Debug.LogError($"传进来的随机数值为 {randomNum}");
        return EnemyType.Null;
    }

    /// <summary>
    /// 获取摄像机外围的范围内随机坐标
    /// </summary>
    /// <returns></returns>
    private Vector2 GetRandomCoordinate()
    {
        //获取到摄像机的长度
        float height = Camera.main.orthographicSize * 2;
        //获取到摄像机的宽度
        float width = Camera.main.aspect * height;

        //先生成四个方向的随机数
        int randomDir = Random.Range(0, 4);

        float x;
        float y;

        //通过随机数判断位置
        switch(randomDir)
        {
            //左上
            case 0:
                x = Random.Range(referencePoint.position.x - width / 2, referencePoint.position.x - width);
                y = Random.Range(referencePoint.position.x + height / 2, referencePoint.position.y + height);
                return new Vector2(x, y);
            //右上
            case 1:
                x = Random.Range(referencePoint.position.x + width / 2, referencePoint.position.x + width);
                y = Random.Range(referencePoint.position.x + height / 2, referencePoint.position.y + height);
                return new Vector2(x, y);
            //右下
            case 2:
                x = Random.Range(referencePoint.position.x + width / 2, referencePoint.position.x + width);
                y = Random.Range(referencePoint.position.x - height / 2, referencePoint.position.y - height);
                return new Vector2(x, y);
            //左下
            case 3:
                x = Random.Range(referencePoint.position.x - width / 2, referencePoint.position.x - width);
                y = Random.Range(referencePoint.position.x - height / 2, referencePoint.position.y - height);
                return new Vector2(x, y);
            default:
                Debug.LogError("接收到了一个错误的方向数");
                return new Vector2(0, 0);
        }
    }

    private void OnDisable()
    {
        MessageManager.Instance.RemoveFunctionInAction<int>(MessageDefine.ENEMY_COUNTDOWN, CurrentEnemyCountDown);
        MessageManager.Instance.RemoveFunctionInAction<List<WaveData>>(MessageDefine.GET_WAVE_INFO, GetWaveInfo);
        MessageManager.Instance.RemoveFunctionInAction(MessageDefine.GAMESTART, GameStart);
    }
}
