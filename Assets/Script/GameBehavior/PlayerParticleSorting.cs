using UnityEngine;

public class PlayerParticleSorting : MonoBehaviour
{
    [SerializeField] private ParticleSystemRenderer particleRenderer; // 粒子系统的Renderer
    [SerializeField] private SpriteRenderer playerSpriteRenderer;    // 玩家的SpriteRenderer

    private void LateUpdate()
    {
        // 确保粒子系统的sortingOrder比玩家小1（显示在下方）
        particleRenderer.sortingOrder = playerSpriteRenderer.sortingOrder - 1;
    }
}