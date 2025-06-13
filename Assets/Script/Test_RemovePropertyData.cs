using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test_RemovePropertyData : MonoBehaviour
{
    public Button button;

    private void Start()
    {
        button.onClick.AddListener(() =>
        {
            BuffManager.Instance.DeletAllData();
            Debug.Log("删除完成");
        });
    }
}
