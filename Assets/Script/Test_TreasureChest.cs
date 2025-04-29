using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test_TreasureChest : MonoBehaviour
{
    public Button createAChest;

    public Transform player;

    private void Start()
    {
        player = FindAndMoveObject.FindFromFirstLayer("Player").transform;

        createAChest.onClick.AddListener(() => {
            GameObject chest = TreasureChestFactory.CreateAChest((Vector2)player.position + new Vector2(0, 8f), 500);
            Instantiate(chest, (Vector2)player.position + new Vector2(50f, 50f), Quaternion.identity);
        });
    }
}
