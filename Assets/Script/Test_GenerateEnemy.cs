using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_GenerateEnemy : MonoBehaviour
{
    public void Start()
    {
        MessageManager.Instance.Send(MessageDefine.GAME_START);
    }
}
