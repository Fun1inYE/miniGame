using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test_BuildTower : MonoBehaviour
{
    public Button buildTower;

    public Button sureBuild;

    public TowerGenrateManager towerGenrateManager;

    private void Start()
    {
        towerGenrateManager = ComponentFinder.GetOrAddComponent<TowerGenrateManager>(FindAndMoveObject.FindFromFirstLayer("TowerGenrateManager"));

        buildTower.onClick.AddListener(() => {
            towerGenrateManager.SelectATower(TowerType.MagicTower);
        });

        sureBuild.onClick.AddListener(() => {
            towerGenrateManager.SwitchStateToBuilding();
        });

        
    }
}
