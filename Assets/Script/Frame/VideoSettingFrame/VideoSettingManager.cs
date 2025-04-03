using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoSettingManager : MonoBehaviour
{
    void Start()
    {
        //限制帧数在60帧
        Application.targetFrameRate = 60;
    }

}
