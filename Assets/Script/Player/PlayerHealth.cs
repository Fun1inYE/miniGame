using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家有关生命值的逻辑
/// </summary>
public class PlayerHealth : MonoBehaviour
{   
    /// <summary>
    /// 玩家的生命
    /// </summary>
    public int health;

    public SpriteRenderer spriteRenderer;
    public Material originalMaterial; // 原始材质
    public Material flashMaterial; //闪烁材质
    private Coroutine flashCoroutine;  // 用于控制协程

    [SerializeField]
    private float flashDuration = 0.1f; // 闪白持续时间

    public void Awake()
    {
        if(spriteRenderer != null)
        {
            originalMaterial = spriteRenderer.material;
        }
    }

    /// <summary>
    /// 触发闪白效果
    /// </summary>
    public void TriggerFlash()
    {
        if (flashCoroutine != null)
        {
            StopCoroutine(flashCoroutine);
        }
        flashCoroutine = StartCoroutine(FlashCoroutine());
    }

    /// <summary>
    /// 闪白协程
    /// </summary>
    private IEnumerator FlashCoroutine()
    {
        // 设置材质为闪红材质
        spriteRenderer.material = flashMaterial;

        // 等待一段时间
        yield return new WaitForSeconds(flashDuration);

        // 恢复原来的材质
        spriteRenderer.material = originalMaterial;

        flashCoroutine = null;
    }

    private void Update()
    {
        if(health <= 0)
        {
            //TODO: 玩家死亡的逻辑
        }
    }

    /// <summary>
    /// 玩家被攻击的脚本
    /// </summary>
    public void Attacked(int attack)
    {   
        //触发闪红的协程
        TriggerFlash();
        health = health - attack;
    }
}
